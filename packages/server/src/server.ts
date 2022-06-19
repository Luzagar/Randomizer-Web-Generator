import path from 'path';
import fs from 'fs-extra';
import searchUpFileTree from './util/searchUpFileTree';

if (process.env.NODE_ENV === 'production') {
  require('dotenv').config();
} else {
  const envFileDir = searchUpFileTree(__dirname, (currPath) =>
    fs.existsSync(path.join(currPath, '.env'))
  );

  require('dotenv').config({
    path: path.join(envFileDir, '.env.development'),
  });
}
console.log(process.env);

import { initConfig, resolveRootPath, resolveOutputPath } from './config';
initConfig();

const url = require('url');
const cors = require('cors');
import express from 'express';
import {
  callGenerator,
  callGeneratorMatchOutput,
  callGeneratorBuf,
} from './util';
import apiSeedProgress from './api/seed/apiSeedProgress';
import apiSeedGenerate from './api/seed/apiSeedGenerate';
import { genUserJwt } from './util/jwt';
const { normalizeStringToMax128Bytes } = require('./util/string');
import jwt from 'jsonwebtoken';
import { getGenerationProgress } from './generationQueues';

declare global {
  namespace Express {
    interface Request {
      newUserJwt?: string;
      userId?: string;
    }
  }
}

const app = express(); // create express app
app.use(cors());

const bodyParser = require('body-parser');
app.use(bodyParser.json());

app.all(
  '/api/*',
  (req: express.Request, res: express.Response, next: express.NextFunction) => {
    if (
      !req.headers.authorization ||
      !req.headers.authorization.startsWith('Bearer ')
    ) {
      return res.status(403).send({ error: 'Forbidden' });
    }

    const token = req.headers.authorization.substring(7);

    jwt.verify(token, 'example_secret_key', (err, data: jwt.JwtPayload) => {
      if (err || !data.uid) {
        return res.status(403).send({ error: 'Forbidden' });
      }

      req.userId = data.uid;
      next();
    });
  }
);

app.get(
  '*',
  (req: express.Request, res: express.Response, next: express.NextFunction) => {
    if (req.path.indexOf('.') < 0 && !req.path.startsWith('/api/')) {
      const userAgent = req.headers['user-agent'];
      if (!userAgent) {
        return res.send('Unsupported User-Agent.');
      } else if (
        userAgent.indexOf('MSIE ') >= 0 ||
        userAgent.indexOf('Trident/') >= 0
      ) {
        return res.send(
          'Internet Explorer is not supported. Please use a different browser.'
        );
      }

      req.newUserJwt = genUserJwt();
    }
    next();
  }
);

let root: string;
let indexHtmlPath: string;

// add middlewares
if (process.env.NODE_ENV === 'production') {
  root = path.join(__dirname, '..', 'client', 'build');
  indexHtmlPath = path.join(__dirname, '..', 'client', 'build', 'index.html');
} else {
  // root = path.join(__dirname, 'build');
  // indexHtmlPath = path.join(__dirname, 'build', 'index.html');

  // root = path.join(__dirname, 'packages', 'client');
  // indexHtmlPath = path.join(__dirname, 'packages', 'client', 'index.html');

  const rootDir = resolveRootPath();

  root = path.join(rootDir, 'packages', 'client');
  indexHtmlPath = path.join(root, 'index.html');
}

app.post('/api/seed/generate', apiSeedGenerate);
app.get('/api/seed/progress/:id', apiSeedProgress);

app.post(
  '/api/generateseed',
  function (req: express.Request, res: express.Response) {
    const { settingsString, seed } = req.body;

    if (!settingsString || typeof settingsString !== 'string') {
      res.status(400).send({ error: 'Malformed request.' });
      return;
    }

    if (seed && typeof seed !== 'string') {
      res.status(400).send({ error: 'Malformed request.' });
      return;
    }

    const seedStr = seed ? normalizeStringToMax128Bytes(seed) : '';
    console.log(`seedStr: '${seedStr}'`);

    callGeneratorMatchOutput(
      ['generate2', settingsString, seedStr],
      (error, data) => {
        if (error) {
          res.status(500).send({ error });
        } else {
          res.send({ data: { id: data } });
        }
      }
    );
  }
);

interface Aaa {
  name: string;
  length: number;
  bytes: string;
}

interface OutputFileMeta {
  name: string;
  length: number;
}

app.post('/api/final', function (req: express.Request, res: express.Response) {
  const { referer } = req.headers;

  let id = null;

  try {
    const refUrl = new URL(referer);
    if (refUrl.pathname) {
      const match = refUrl.pathname.match(/^\/seed\/([a-zA-Z0-9-_]+)$/);
      if (match && match.length > 1) {
        id = match[1];
      }
    }
  } catch (e) {
    // do nothing
  }

  // const { query } = url.parse(referer, true);
  // const { id } = query;

  if (!id) {
    res.status(400).send({ error: 'Bad referer.' });
    return;
  }

  if (typeof id !== 'string' || !/^[0-9a-z-_]+$/i.test(id)) {
    res.status(400).send({ error: 'Invalid id format.' });
    return;
  }

  const { fileCreationSettings } = req.body;

  if (
    !fileCreationSettings ||
    typeof fileCreationSettings !== 'string' ||
    !/^[0-9a-z-_]+$/i.test(fileCreationSettings)
  ) {
    res.status(400).send({ error: 'Invalid fileCreationSettings format.' });
    return;
  }

  callGeneratorBuf(
    ['generate_final_output2', id, 'aBc', fileCreationSettings],
    (error, buffer) => {
      if (error) {
        res.status(500).send({ error });
        return;
      }

      try {
        const index = buffer.indexOf('BYTES:', 0);

        let currIndex = index;
        if (currIndex < 0) {
          res.status(500).send({ error: 'Failed to find BYTES:' });
          return;
        }

        currIndex += 'BYTES:'.length;
        const jsonLen = parseInt(
          buffer.toString('utf8', currIndex, currIndex + 8),
          16
        );
        currIndex += 8;

        const json: OutputFileMeta[] = JSON.parse(
          buffer.toString('utf8', currIndex, currIndex + jsonLen)
        );
        currIndex += jsonLen;

        const data: Aaa[] = [];
        // const data = [];
        json.forEach(({ name, length }) => {
          data.push({
            name,
            length,
            bytes: buffer
              .subarray(currIndex, currIndex + length)
              .toString('base64'),
          });
          currIndex += length;
        });

        res.send({ data });
      } catch (e) {
        res.status(500).send({ error: e.message });
      }
    }
  );
});

app.get(
  '/api/creategci',
  function (req: express.Request, res: express.Response) {
    const { referer } = req.headers;

    const { query } = url.parse(referer, true);

    const { id } = query;

    if (!id) {
      res.status(400).send({ error: 'Bad referer.' });
      return;
    }

    const filePath = resolveOutputPath(`seeds/${id}/input.json`);
    if (fs.existsSync(filePath)) {
      const ff = fs.readFileSync(filePath, { encoding: 'utf8' });
      const json = JSON.parse(ff);
      res.send({ data: json });
    } else {
      res.status(404).send({
        error: 'Did not find seed data for provided id.',
      });
    }
  }
);

app.get('/', (req: express.Request, res: express.Response) => {
  fs.readFile(indexHtmlPath, function read(err, data) {
    if (err) {
      console.log(err);
      res.status(500).send({ error: 'Internal server error.' });
    } else {
      let msg = data.toString();
      msg = msg.replace(
        '<!-- USER_ID -->',
        `<input id="userJwtInput" type="hidden" value="${req.newUserJwt}">`
      );

      const excludedChecksList = JSON.parse(callGenerator('print_check_ids'));
      const arr = Object.keys(excludedChecksList).map((key) => {
        return `<li><label><input type='checkbox' data-checkId='${excludedChecksList[key]}'>${key}</label></li>`;
      });

      msg = msg.replace('<!-- CHECK_IDS -->', arr.join('\n'));

      const startingItems = [
        [63, 'Progressive Sword'],
        [63, 'Progressive Sword'],
        [63, 'Progressive Sword'],
        [63, 'Progressive Sword'],
        [64, 'Boomerang'],
        [72, 'Lantern'],
        [75, 'Slingshot'],
        [74, 'Progressive Fishing Rod'],
        [74, 'Progressive Fishing Rod'],
        [69, 'Iron Boots'],
        [67, 'Progressive Bow'],
        [81, 'Bomb Bag and Bombs'],
        [49, 'Zora Armor'],
        [68, 'Progressive Clawshot'],
        [68, 'Progressive Clawshot'],
        [50, 'Shadow Crystal'],
        [144, 'Aurus Memo'],
        [145, 'Asheis Sketch'],
        [65, 'Spinner'],
        [66, 'Ball and Chain'],
        [70, 'Progressive Dominion Rod'],
        [70, 'Progressive Dominion Rod'],
        [233, 'Progressive Sky Book'],
        [233, 'Progressive Sky Book'],
        [233, 'Progressive Sky Book'],
        [233, 'Progressive Sky Book'],
        [233, 'Progressive Sky Book'],
        [233, 'Progressive Sky Book'],
        [233, 'Progressive Sky Book'],
        [132, 'Horse Call'],
        [243, 'Gate Keys'],
        [96, 'Empty Bottle'],
      ];

      const startingItemsEls = startingItems.map((item) => {
        return `<li><label><input type='checkbox' data-itemId='${item[0]}'>${item[1]}</label> </li>`;
      });

      msg = msg.replace('<!-- STARTING_ITEMS -->', startingItemsEls.join('\n'));

      res.send(msg);
    }
  });
});

const escapeHtml = (str: string) =>
  str.replace(
    /[&<>'"]/g,
    (tag: string) =>
      ({
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;',
        "'": '&#39;',
        '"': '&quot;',
      }[tag])
  );

app.get('/seed/:id', (req: express.Request, res: express.Response) => {
  fs.readFile(path.join(root, 'getseed.html'), function read(err, data) {
    if (err) {
      console.log(err);
      res.status(500).send({ error: 'Internal server error.' });
    } else {
      let msg = data.toString();

      // const { id } = <{ id: string }>req.query;
      // if (!id || typeof id )

      const { id } = req.params;

      if (!id) {
        res.status(400).send({ error: 'Malformed request.' });
        return;
      }

      // const filePath = path.join(__dirname, `seeds/${id}/input.json`);
      const filePath = resolveOutputPath(`seeds/${id}/input.json`);

      if (fs.existsSync(filePath)) {
        // Completely done generating
        const json = JSON.parse(
          fs.readFileSync(filePath, { encoding: 'utf8' })
        );
        json.seedHash = undefined;
        json.itemPlacement = undefined;
        const fileContents = escapeHtml(JSON.stringify(json));

        msg = msg.replace(
          '<!-- INPUT_JSON_DATA -->',
          `<input id='inputJsonData' type='hidden' value='${fileContents}'>`
        );
        msg = msg.replace('<!-- REQUESTER_HASH -->', '');
      } else {
        const { generationStatus } = getGenerationProgress(id);
        if (generationStatus) {
          // Generation requested but not completed and not 100% forgotten about
          // by the server
          msg = msg.replace('<!-- INPUT_JSON_DATA -->', '');
          msg = msg.replace(
            '<!-- REQUESTER_HASH -->',
            `<input id='requesterHash' type='hidden' value='${generationStatus.requesterHash}'>`
          );
        } else {
          // Have no idea what the client is talking about with that seedId
          msg = msg.replace('<!-- INPUT_JSON_DATA -->', '');
          msg = msg.replace('<!-- REQUESTER_HASH -->', '');
        }
      }

      res.send(msg);
    }
  });
});

app.use(express.static(root));

app.use(function (req: express.Request, res: express.Response, next) {
  res.status(404);

  const accepts = req.accepts(['text/html', 'application/json']);

  if (accepts === 'applicaton/json') {
    res.json({ error: 'Not found' });
  } else {
    res.type('txt').send('Not found');
  }
});

// start express server on port 5000
app.listen(process.env.PORT || 5000, () => {
  console.log('server started');
});
