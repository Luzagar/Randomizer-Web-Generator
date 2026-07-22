namespace TPRandomizer.Hints
{
    using System;
    using System.Collections.Generic;

    public enum Zone : int
    {
        Invalid = 0,
        Ordon = 1,
        Sacred_Grove = 2,
        Faron_Field = 3,
        Faron_Woods = 4,
        Kakariko_Gorge = 5,
        Kakariko_Village = 6,
        Kakariko_Graveyard = 7,
        Eldin_Field = 8,
        North_Eldin = 9,
        Death_Mountain = 10,
        Hidden_Village = 11,
        Lanayru_Field = 12,
        Beside_Castle_Town = 13,
        South_of_Castle_Town = 14,
        Castle_Town = 15,
        Agithas_Castle = 16,
        Great_Bridge_of_Hylia = 17,
        Lake_Hylia = 18,
        Lake_Lantern_Cave = 19,
        Lanayru_Spring = 20,
        Zoras_Domain = 21,
        Upper_Zoras_River = 22,
        Gerudo_Desert = 23,
        Bulblin_Camp = 24,
        Snowpeak_Mountain = 25,
        Cave_of_Ordeals = 26,
        Forest_Temple = 27,
        Goron_Mines = 28,
        Lakebed_Temple = 29,
        Arbiters_Grounds = 30,
        Snowpeak_Ruins = 31,
        Temple_of_Time = 32,
        City_in_the_Sky = 33,
        Palace_of_Twilight = 34,
        Hyrule_Castle = 35,
    }

    public class ZoneUtils
    {
        public static readonly byte NumBitsToEncode = 6;
        private static Dictionary<Zone, string> enumToStr;
        private static Dictionary<string, Zone> strToEnum;
        private static Dictionary<Zone, SpotId> idToSpotId =
            new()
            {
                { Zone.Ordon, SpotId.Ordon_Sign },
                { Zone.Sacred_Grove, SpotId.Sacred_Grove_Sign },
                { Zone.Faron_Field, SpotId.Faron_Field_Sign },
                { Zone.Faron_Woods, SpotId.Faron_Woods_Sign },
                { Zone.Kakariko_Gorge, SpotId.Kakariko_Gorge_Sign },
                { Zone.Kakariko_Village, SpotId.Kakariko_Village_Sign },
                { Zone.Kakariko_Graveyard, SpotId.Kakariko_Graveyard_Sign },
                { Zone.Eldin_Field, SpotId.Eldin_Field_Sign },
                { Zone.North_Eldin, SpotId.North_Eldin_Sign },
                { Zone.Death_Mountain, SpotId.Death_Mountain_Sign },
                { Zone.Hidden_Village, SpotId.Hidden_Village_Sign },
                { Zone.Lanayru_Field, SpotId.Lanayru_Field_Sign },
                { Zone.Beside_Castle_Town, SpotId.Beside_Castle_Town_Sign },
                { Zone.South_of_Castle_Town, SpotId.South_of_Castle_Town_Sign },
                { Zone.Castle_Town, SpotId.Castle_Town_Sign },
                { Zone.Great_Bridge_of_Hylia, SpotId.Great_Bridge_of_Hylia_Sign },
                { Zone.Lake_Hylia, SpotId.Lake_Hylia_Sign },
                { Zone.Lake_Lantern_Cave, SpotId.Lake_Lantern_Cave_Sign },
                { Zone.Lanayru_Spring, SpotId.Lanayru_Spring_Sign },
                { Zone.Zoras_Domain, SpotId.Zoras_Domain_Sign },
                { Zone.Upper_Zoras_River, SpotId.Upper_Zoras_River_Sign },
                { Zone.Gerudo_Desert, SpotId.Gerudo_Desert_Sign },
                { Zone.Bulblin_Camp, SpotId.Bulblin_Camp_Sign },
                { Zone.Snowpeak_Mountain, SpotId.Snowpeak_Mountain_Sign },
                { Zone.Cave_of_Ordeals, SpotId.Cave_of_Ordeals_Sign },
                { Zone.Forest_Temple, SpotId.Forest_Temple_Sign },
                { Zone.Goron_Mines, SpotId.Goron_Mines_Sign },
                { Zone.Lakebed_Temple, SpotId.Lakebed_Temple_Sign },
                { Zone.Arbiters_Grounds, SpotId.Arbiters_Grounds_Sign },
                { Zone.Snowpeak_Ruins, SpotId.Snowpeak_Ruins_Sign },
                { Zone.Temple_of_Time, SpotId.Temple_of_Time_Sign },
                { Zone.City_in_the_Sky, SpotId.City_in_the_Sky_Sign },
                { Zone.Palace_of_Twilight, SpotId.Palace_of_Twilight_Sign },
                { Zone.Hyrule_Castle, SpotId.Hyrule_Castle_Sign },
            };

        private static Dictionary<SpotId, Zone> spotIdToId;

        public static readonly Dictionary<Zone, BeyondPointObj> idToBeyondPointData =
            new()
            {
                {
                    Zone.Lake_Lantern_Cave,
                    new BeyondPointObj(
                        Zone.Lake_Lantern_Cave,
                        SpotId.Lake_Lantern_Cave_Sign,
                        HintCategory.Lake_Lantern_Cave_2nd_Half,
                        BeyondPointObj.Validity.AlwaysPass
                    )
                },
                {
                    Zone.Snowpeak_Mountain,
                    new BeyondPointObj(
                        Zone.Snowpeak_Mountain,
                        SpotId.Snowpeak_Mountain_Sign,
                        HintCategory.Snowpeak_Beyond_This_Point,
                        BeyondPointObj.Validity.Snowpeak
                    )
                },
                {
                    Zone.Goron_Mines,
                    new BeyondPointObj(
                        Zone.Goron_Mines,
                        SpotId.Goron_Mines_Sign,
                        HintCategory.Goron_Mines_2nd_Part,
                        BeyondPointObj.Validity.Dungeon
                    )
                },
                {
                    Zone.Lakebed_Temple,
                    new BeyondPointObj(
                        Zone.Lakebed_Temple,
                        SpotId.Lakebed_Temple_Sign,
                        HintCategory.Lakebed_Temple_2nd_Wing,
                        BeyondPointObj.Validity.Dungeon
                    )
                },
                {
                    Zone.Arbiters_Grounds,
                    new BeyondPointObj(
                        Zone.Arbiters_Grounds,
                        SpotId.Arbiters_Grounds_Sign,
                        HintCategory.Arbiters_Grounds_2nd_Half,
                        BeyondPointObj.Validity.Dungeon
                    )
                },
                {
                    Zone.Temple_of_Time,
                    new BeyondPointObj(
                        Zone.Temple_of_Time,
                        SpotId.Temple_of_Time_Beyond_Point_Sign,
                        HintCategory.Temple_of_Time_2nd_Half,
                        BeyondPointObj.Validity.Dungeon
                    )
                },
                {
                    Zone.City_in_the_Sky,
                    new BeyondPointObj(
                        Zone.City_in_the_Sky,
                        SpotId.City_in_the_Sky_Sign,
                        HintCategory.City_in_the_Sky_East_Wing,
                        BeyondPointObj.Validity.Dungeon
                    )
                },
                {
                    Zone.Palace_of_Twilight,
                    new BeyondPointObj(
                        Zone.Palace_of_Twilight,
                        SpotId.Palace_of_Twilight_Sign,
                        HintCategory.Palace_of_Twilight_Center_Wing,
                        BeyondPointObj.Validity.Dungeon
                    )
                },
            };

        private static readonly HashSet<Zone> dungeonZones =
            new()
            {
                Zone.Forest_Temple,
                Zone.Goron_Mines,
                Zone.Lakebed_Temple,
                Zone.Arbiters_Grounds,
                Zone.Snowpeak_Ruins,
                Zone.Temple_of_Time,
                Zone.City_in_the_Sky,
                Zone.Palace_of_Twilight,
                Zone.Hyrule_Castle,
            };

        public static readonly Dictionary<string, string[]> zoneNameToChecks =
            new()
            {
                {
                    "Ordon",
                    new[]
                    {
                        "Herding Goats Reward",
                        "Links Basement Chest",
                        "Ordon Cat Rescue",
                        "Ordon Ranch Grotto Lantern Chest",
                        "Ordon Shield",
                        "Ordon Spring Golden Wolf",
                        "Ordon Sword",
                        "Ordon Bo Cliff Rupee",
                        "Ordon Bo Roof Rupee",
                        "Ordon Bo Window Rupee 1",
                        "Ordon Bo Window Rupee 2",
                        "Ordon Hidden Rusl House Rupee",
                        "Ordon Rupee In Grass By Bo",
                        "Ordon Rupee In River 1",
                        "Ordon Rupee In River 2",
                        "Ordon Rupee Under Bridge",
                        "Ordon Rupee Under Tall Tree 1",
                        "Ordon Rupee Under Tall Tree 2",
                        "Ordon Rusl House Roof Rupee 1",
                        "Ordon Rusl House Roof Rupee 2",
                        "Ordon Shield House Ledge Grass Rupee",
                        "Ordon Tree Long Branch Rupee",
                        "Ordon Tree Short Branch Rupee",
                        // "Catch A Greengill",
                        // "Catch An Ordon Catfish",
                        "Sera Shop Slingshot",
                        "Uli Cradle Delivery",
                        "Wooden Sword Chest",
                        "Wrestling With Bo",
                        "Talk To Frog Outside Rusl House",
                        "Talk To Ordon Hawk",
                    }
                },
                {
                    "Sacred Grove",
                    new[]
                    {
                        "Lost Woods Boulder Poe",
                        "Lost Woods Lantern Chest",
                        "Lost Woods Waterfall Poe",
                        "Sacred Grove Baba Serpent Grotto Chest",
                        "Sacred Grove Female Snail",
                        "Sacred Grove Male Snail",
                        "Sacred Grove Master Sword Poe",
                        "Sacred Grove Past Owl Statue Chest",
                        "Sacred Grove Pedestal Master Sword",
                        "Sacred Grove Pedestal Shadow Crystal",
                        "Sacred Grove Spinner Chest",
                        "Sacred Grove ToT Owl Statue Poe",
                        //"Catch A Greengill",
                        //"Catch An Ordon Catfish",
                        //"Catch A Hylian Loach"
                    }
                },
                {
                    "Faron Field",
                    new[]
                    {
                        "Faron Field Bridge Chest",
                        "Faron Field Corner Grotto Left Chest",
                        "Faron Field Corner Grotto Rear Chest",
                        "Faron Field Corner Grotto Right Chest",
                        "Faron Field Female Beetle",
                        "Faron Field Male Beetle",
                        "Faron Field Poe",
                        "Faron Field Tree Heart Piece",
                        //"Catch An Ordon Catfish",
                        //"Catch A Hylian Loach",
                    }
                },
                {
                    "Faron Woods",
                    new[]
                    {
                        "Coro Bottle",
                        "Coro Lantern",
                        "Coro Gate Key",
                        "Faron Mist Cave Lantern Chest",
                        "Faron Mist Cave Open Chest",
                        "Faron Mist North Chest",
                        "Faron Mist Poe",
                        "Faron Mist South Chest",
                        "Faron Mist Stump Chest",
                        "Faron Woods Golden Wolf",
                        "Faron Woods Owl Statue Chest",
                        "Faron Woods Owl Statue Sky Character",
                        "North Faron Woods Deku Baba Chest",
                        "South Faron Cave Chest",
                        "Faron Woods Coro Boulder Rupee 1",
                        "Faron Woods Coro Boulder Rupee 2",
                        "Faron Woods Coro Boulder Rupee 3",
                        "Faron Woods Coro Boulder Rupee 4",
                        "Talk To Trill As Wolf",
                        //"Catch A Greengill",
                        //"Catch An Ordon Catfish",
                    }
                },
                {
                    "Kakariko Gorge",
                    new[]
                    {
                        "Eldin Lantern Cave First Chest",
                        "Eldin Lantern Cave Lantern Chest",
                        "Eldin Lantern Cave Poe",
                        "Eldin Lantern Cave Second Chest",
                        "Kak Gorge Double Clawshot Chest",
                        "Kak Gorge Female Pill Bug",
                        "Kak Gorge Male Pill Bug",
                        "Kak Gorge Owl Statue Chest",
                        "Kak Gorge Owl Statue Sky Character",
                        "Kak Gorge Poe",
                        "Kak Gorge Spire Heart Piece",
                        "Kak Gorge Owl Statue Boulder Rupee",
                        "Kak Gorge Spire Boulder Rupee",
                    }
                },
                {
                    "Kakariko Village",
                    new[]
                    {
                        "Barnes Bomb Bag",
                        "Eldin Spring Underwater Chest",
                        "Ilia Memory Reward",
                        "Kak Inn Chest",
                        "Kak Village Bomb Rock Spire Heart Piece",
                        "Kak Village Bomb Shop Poe",
                        "Kak Village Female Ant",
                        "Kak Village Malo Mart Hawkeye",
                        "Kak Village Malo Mart Hylian Shield",
                        "Kak Village Malo Mart Red Potion",
                        "Kak Village Malo Mart Wooden Shield",
                        "Kak Village Watchtower Poe",
                        "Kak Watchtower Alcove Chest",
                        "Kak Watchtower Chest",
                        "Renados Letter",
                        "Talo Sharpshooting",
                        "Kak Village Bell Rupee",
                        "Shad Dominion Rod",
                        "Eldin Spring Underwater Boulder Rupee",
                        "Kak Village Spring Shortcut Box Rupee 1",
                        "Kak Village Spring Shortcut Box Rupee 2",
                        "Kak Village Ant House Ledge Box Rupee",
                        "Kak Village Hot Spring Ledge Box Rupee",
                        //"Catch A Greengill",
                        //"Catch A Hyrule Bass",
                    }
                },
                {
                    "Kakariko Graveyard",
                    new[]
                    {
                        "Gift From Ralis",
                        "Kak Graveyard Golden Wolf",
                        "Kak Graveyard Grave Poe",
                        "Kak Graveyard Lantern Chest",
                        "Kak Graveyard Male Ant",
                        "Kak Graveyard Open Poe",
                        "Rutelas Blessing",
                        "Kak Graveyard Underwater Boulder Rupee",
                        //"Catch A Greengill",
                        //"Catch A Hylian Loach",
                    }
                },
                {
                    "Eldin Field",
                    new[]
                    {
                        "Bridge of Eldin Male Phasmid",
                        "Bridge of Eldin Owl Statue Chest",
                        "Eldin Field Bomb Rock Chest",
                        "Eldin Field Bomskit Grotto Lantern Chest",
                        "Eldin Field Bomskit Grotto Left Chest",
                        "Eldin Field Female Grasshopper",
                        "Eldin Field Male Grasshopper",
                        "Eldin Field Water Bomb Fish Grotto Chest",
                        "Goron Springwater Rush"
                    }
                },
                {
                    "North Eldin",
                    new[]
                    {
                        "Bridge of Eldin Female Phasmid",
                        "Bridge of Eldin Owl Statue Sky Character",
                        "Eldin Field Stalfos Grotto Left Small Chest",
                        "Eldin Field Stalfos Grotto Right Small Chest",
                        "Eldin Field Stalfos Grotto Stalfos Chest",
                        "Eldin Stockcave Lantern Chest",
                        "Eldin Stockcave Lowest Chest",
                        "Eldin Stockcave Upper Chest",
                        "Bridge of Eldin Boulder Rupee"
                    }
                },
                {
                    "Death Mountain",
                    new[]
                    {
                        "Death Mountain Alcove Chest",
                        "Death Mountain Trail Poe",
                        "Death Mountain Volcano Ledge Rupee 1",
                        "Death Mountain Volcano Ledge Rupee 2",
                        "Death Mountain Volcano Ledge Rupee 3",
                        "Death Mountain Volcano Pipe Ledge Rock Rupee",
                    }
                },
                {
                    "Hidden Village",
                    new[]
                    {
                        "Cats Hide and Seek Minigame",
                        "HV Poe",
                        "Ilia Charm",
                        "Skybook From Impaz",
                        "Talk To HV Cucco",
                    }
                },
                {
                    "Lanayru Field",
                    new[]
                    {
                        "Lanayru Field Behind Gate Underwater Chest",
                        "Lanayru Field Bridge Poe",
                        "Lanayru Field Female Stag Beetle",
                        "Lanayru Field Male Stag Beetle",
                        "Lanayru Field Poe Grotto Left Poe",
                        "Lanayru Field Poe Grotto Right Poe",
                        "Lanayru Field Skulltula Grotto Chest",
                        "Lanayru Field Spinner Track Chest",
                        "Lanayru Ice Block Puzzle Cave Chest",
                        "Lanayru Field North Spinner Track Boulder Rupee",
                        "Lanayru Field South Spinner Track Boulder Rupee",
                        "Lanayru Field North Underwater Boulder Rupee",
                        "Lanayru Field South Underwater Boulder Rupee",
                        "Lanayru Field Tree Boulder Rupee",
                        //"Catch A Greengill",
                        //"Catch A Hylian Pike",
                    }
                },
                {
                    "Beside Castle Town",
                    new[]
                    {
                        "Hyrule Field Amphitheater Owl Statue Chest",
                        "Hyrule Field Amphitheater Owl Statue Sky Character",
                        "Hyrule Field Amphitheater Poe",
                        "BCT Female Butterfly",
                        "BCT Golden Wolf",
                        "BCT Helmasaur Grotto Chest",
                        "BCT Male Butterfly",
                        "BCT Northern Boulder Rupee",
                        "BCT Southern Boulder Rupee"
                    }
                },
                {
                    "South of Castle Town",
                    new[]
                    {
                        "OCT South Double Clawshot Chasm Chest",
                        "OCT South Female Ladybug",
                        "OCT South Fountain Chest",
                        "OCT South Golden Wolf",
                        "OCT South Male Ladybug",
                        "OCT South Poe",
                        "OCT South Tektite Grotto Chest",
                        "OCT South Tightrope Chest",
                        "Wooden Statue",
                        "OCT South Boulder Rupee",
                        //"Catch A Hylian Pike",
                    }
                },
                {
                    "Castle Town",
                    new[]
                    {
                        "CT Malo Mart Magic Armor",
                        "Charlo Donation Blessing",
                        "Doctors Office Balcony Chest",
                        "East CT Bridge Poe",
                        "Jovani 20 Poe Soul Reward",
                        "Jovani 60 Poe Soul Reward",
                        "Jovani House Poe",
                        "North CT Golden Wolf",
                        "STAR Prize 1",
                        "STAR Prize 2",
                        "Telma Invoice",
                        "CT Goron Shop Hylian Shield",
                        "CT Goron Shop Red Potion",
                        "CT Goron Shop Lantern Oil",
                        "CT Goron Shop Arrow Refill",
                        "Talk To West CT Dog",
                        "Talk To East CT Dog",
                        "Talk To CT Black And White Cat After Jovani 60",
                        "Talk To CT Dark Cat After MDH",
                    }
                },
                {
                    "Agitha's Castle",
                    new[]
                    {
                        "Agitha Female Ant Reward",
                        "Agitha Female Beetle Reward",
                        "Agitha Female Butterfly Reward",
                        "Agitha Female Dayfly Reward",
                        "Agitha Female Dragonfly Reward",
                        "Agitha Female Grasshopper Reward",
                        "Agitha Female Ladybug Reward",
                        "Agitha Female Mantis Reward",
                        "Agitha Female Phasmid Reward",
                        "Agitha Female Pill Bug Reward",
                        "Agitha Female Snail Reward",
                        "Agitha Female Stag Beetle Reward",
                        "Agitha Male Ant Reward",
                        "Agitha Male Beetle Reward",
                        "Agitha Male Butterfly Reward",
                        "Agitha Male Dayfly Reward",
                        "Agitha Male Dragonfly Reward",
                        "Agitha Male Grasshopper Reward",
                        "Agitha Male Ladybug Reward",
                        "Agitha Male Mantis Reward",
                        "Agitha Male Phasmid Reward",
                        "Agitha Male Pill Bug Reward",
                        "Agitha Male Snail Reward",
                        "Agitha Male Stag Beetle Reward"
                    }
                },
                {
                    "Great Bridge of Hylia",
                    new[]
                    {
                        "Lake Hylia Bridge Bubble Grotto Chest",
                        "Lake Hylia Bridge Cliff Chest",
                        "Lake Hylia Bridge Cliff Poe",
                        "Lake Hylia Bridge Female Mantis",
                        "Lake Hylia Bridge Male Mantis",
                        "Lake Hylia Bridge Owl Statue Chest",
                        "Lake Hylia Bridge Owl Statue Sky Character",
                        "Lake Hylia Bridge Vines Chest",
                        "Lake Hylia Bridge Faron Boulder Rupee",
                        "Lake Hylia Bridge Owl Statue Boulder Rupee",
                    }
                },
                {
                    "Lake Hylia",
                    new[]
                    {
                        "Auru Gift To Fyer",
                        "Flight By Fowl Fifth Platform Chest",
                        "Flight By Fowl Fourth Platform Chest",
                        "Flight By Fowl Ledge Poe",
                        "Flight By Fowl Second Platform Chest",
                        "Flight By Fowl Third Platform Chest",
                        "Flight By Fowl Top Platform Reward",
                        "Isle of Riches Poe",
                        "Lake Hylia Alcove Poe",
                        "Lake Hylia Dock Poe",
                        "Lake Hylia Shell Blade Grotto Chest",
                        "Lake Hylia Tower Poe",
                        "Lake Hylia Underwater Chest",
                        "Lake Hylia Water Toadpoli Grotto Chest",
                        "Outside Lanayru Spring Left Statue Chest",
                        "Outside Lanayru Spring Right Statue Chest",
                        "Plumm Fruit Balloon Minigame",
                        "Lake Hylia Left Underwater Pillar Rupee",
                        "Lake Hylia Right Underwater Pillar Rupee",
                        "Lake Hylia Right Underwater Boulder Rupee",
                        "Lake Hylia Left Underwater Boulder Rupee",
                        "Talk To Plumm As Wolf",
                        "Talk To Plumm As Link",
                    }
                },
                {
                    "Lake Lantern Cave",
                    new[]
                    {
                        "LLC Eighth Chest",
                        "LLC Eleventh Chest",
                        "LLC End Lantern Chest",
                        "LLC Fifth Chest",
                        "LLC Final Poe",
                        "LLC First Chest",
                        "LLC First Poe",
                        "LLC Fourteenth Chest",
                        "LLC Fourth Chest",
                        "LLC Ninth Chest",
                        "LLC Second Chest",
                        "LLC Second Poe",
                        "LLC Seventh Chest",
                        "LLC Sixth Chest",
                        "LLC Tenth Chest",
                        "LLC Third Chest",
                        "LLC Thirteenth Chest",
                        "LLC Twelfth Chest"
                    }
                },
                {
                    "Lanayru Spring",
                    new[]
                    {
                        "Lanayru Spring Back Room Lantern Chest",
                        "Lanayru Spring Back Room Left Chest",
                        "Lanayru Spring Back Room Right Chest",
                        "Lanayru Spring East Double Clawshot Chest",
                        "Lanayru Spring Underwater Left Chest",
                        "Lanayru Spring Underwater Right Chest",
                        "Lanayru Spring West Double Clawshot Chest",
                        "Lanayru Spring Upper Underwater Boulder Rupee",
                        "Lanayru Spring Lower Underwater Boulder Rupee",
                        //"Catch A Hylian Loach",
                        //"Catch A Greengill"
                    }
                },
                {
                    "Zora's Domain",
                    new[]
                    {
                        "ZD Chest Behind Waterfall",
                        "ZD Chest By Mother and Child Isles",
                        "ZD Extinguish All Torches Chest",
                        "ZD Light All Torches Chest",
                        "ZD Male Dragonfly",
                        "ZD Mother and Child Isle Poe",
                        "ZD Underwater Goron",
                        "ZD Waterfall Poe",
                        "ZD Behind Waterfall Rupee",
                        "ZD Central Underwater Boulder Rupee",
                        "ZD North Underwater Boulder Rupee",
                        "ZD Shortcut Ledge Rupee",
                        "ZD Shortcut Lower Boulder Rupee",
                        "ZD Shortcut Upper Boulder Rupee",
                        "ZD Throne East Gate Underwater Rupee",
                        "ZD Throne East Underwater Rupee",
                        "ZD Throne Northwest Underwater Rupee",
                        "ZD Throne South Underwater Rupee",
                        "ZD Throne West Gate Underwater Rupee",
                        "ZD Throne West Underwater Rupee",
                        "ZD Top Ledge Rupee",
                        "ZD Vine Ledge Rupee",
                        "ZD Waterfall Ledge Rupee",
                        "Catch A Reekfish",
                    }
                },
                {
                    "Upper Zora's River",
                    new[]
                    {
                        "Fishing Hole Bottle",
                        "Fishing Hole Heart Piece",
                        "Iza Helping Hand",
                        "Iza Raging Rapids Minigame",
                        "UZR Female Dragonfly",
                        "UZR Poe",
                        "UZR East Underwater Boulder Rupee",
                        "UZR Central Underwater Boulder Rupee",
                        "UZR West Underwater Boulder Rupee",
                        "UZR Ledge Boulder Rupee",
                        "Catch The Legendary Hylian Loach",
                        "Catch A Greengill",
                        "Catch An Ordon Catfish",
                        "Catch A Hyrule Bass",
                        "Catch A Hylian Pike",
                        "Catch A Hylian Loach",
                        "Rollgoal 1-1",
                        "Rollgoal 1-8",
                        "Rollgoal 8-8",
                    }
                },
                {
                    "Gerudo Desert",
                    new[]
                    {
                        "Desert Campfire East Chest",
                        "Desert Campfire North Chest",
                        "Desert Campfire West Chest",
                        "Desert East Canyon Chest",
                        "Desert East Poe",
                        "Desert Female Dayfly",
                        "Desert Golden Wolf",
                        "Desert Lone Small Chest",
                        "Desert Male Dayfly",
                        "Desert North Peahat Poe",
                        "Desert North Small Chest Before Bulblin Camp",
                        "Desert Northeast Chest Behind Gates",
                        "Desert Northwest Chest Behind Gates",
                        "Desert Owl Statue Chest",
                        "Desert Owl Statue Sky Character",
                        "Desert Peahat Ledge Chest",
                        "Desert Poe Above CoO",
                        "Desert Rock Grotto First Poe",
                        "Desert Rock Grotto Lantern Chest",
                        "Desert Rock Grotto Second Poe",
                        "Desert Skulltula Grotto Chest",
                        "Desert South Chest Behind Wooden Gates",
                        "Desert West Canyon Chest",
                        "Outside Bulblin Camp Poe"
                    }
                },
                {
                    "Bulblin Camp",
                    new[]
                    {
                        "Bulblin Camp First Chest Under Tower At Entrance",
                        "Bulblin Camp Poe",
                        "Bulblin Camp Roasted Boar",
                        "Bulblin Camp Small Chest in Back of Camp",
                        "Bulblin Guard Key",
                        "Outside AG Lantern Chest",
                        "Outside AG Poe"
                    }
                },
                {
                    "Snowpeak Mountain",
                    new[]
                    {
                        "Ashei Sketch",
                        "Snowboard Racing Prize",
                        "Snowpeak Above Freezard Grotto Poe",
                        "Snowpeak Blizzard Poe",
                        "Snowpeak Cave Ice Lantern Chest",
                        "Snowpeak Cave Ice Poe",
                        "Snowpeak Freezard Grotto Chest",
                        "Snowpeak Icy Summit Poe",
                        "Snowpeak Poe Among Trees",
                        "Snowboarding Bridge Ledge Bottom Rupee",
                        "Snowboarding Bridge Ledge Middle Rupee",
                        "Snowboarding Bridge Ledge Upper Rupee",
                        "Snowboarding Shortcut Rupee 1",
                        "Snowboarding Shortcut Rupee 2",
                        "Snowboarding Shortcut Rupee 3",
                        "Snowboarding Shortcut Rupee 4",
                        "Snowboarding Shortcut Rupee 5",
                        "Snowboarding Shortcut Rupee 6",
                        "Snowboarding Shortcut Rupee 7",
                        "Snowboarding Shortcut Rupee 8",
                        "Snowboarding Shortcut Rupee 9",
                        "Snowboarding Shortcut Rupee 10",
                        "Snowboarding Shortcut Rupee 11",
                        "Snowboarding Snowy Tree Top Rupee 1",
                        "Snowboarding Snowy Tree Top Rupee 2",
                        "Snowboarding Snowy Tree Top Rupee 3",
                        "Snowboarding Top Left Rupee",
                        "Snowboarding Top Right Rupee"
                    }
                },
                {
                    "Cave of Ordeals",
                    new[]
                    {
                        "CoO Floor 17 Poe",
                        "CoO Floor 33 Poe",
                        "CoO Floor 44 Poe",
                        "CoO Great Fairy Reward"
                    }
                },
                {
                    "Forest Temple",
                    new[]
                    {
                        "FT Big Baba Key",
                        "FT Big Key Chest",
                        "FT Central Chest Behind Stairs",
                        "FT Central Chest Hanging From Web",
                        "FT Central North Chest",
                        "FT Diababa Heart Container",
                        "FT Dungeon Reward",
                        "FT East Tile Worm Chest",
                        "FT East Water Cave Chest",
                        "FT Entrance Vines Chest",
                        "FT Gale Boomerang",
                        "FT North Deku Like Chest",
                        "FT Second Monkey Under Bridge Chest",
                        "FT Totem Pole Chest",
                        "FT West Deku Like Chest",
                        "FT West Tile Worm Chest Behind Stairs",
                        "FT West Tile Worm Room Vines Chest",
                        "FT Windless Bridge Chest"
                    }
                },
                {
                    "Goron Mines",
                    new[]
                    {
                        "GM After Crystal Switch Room Magnet Wall Chest",
                        "GM Beamos Room Chest",
                        "GM Chest Before Dangoro",
                        "GM Crystal Switch Room Small Chest",
                        "GM Crystal Switch Room Underwater Chest",
                        "GM Dangoro Chest",
                        "GM Dungeon Reward",
                        "GM Entrance Chest",
                        "GM Fyrus Heart Container",
                        "GM Gor Amato Chest",
                        "GM Gor Amato Key Shard",
                        "GM Gor Amato Small Chest",
                        "GM Gor Ebizo Chest",
                        "GM Gor Ebizo Key Shard",
                        "GM Gor Liggs Chest",
                        "GM Gor Liggs Key Shard",
                        "GM Magnet Maze Chest",
                        "GM Main Magnet Room Bottom Chest",
                        "GM Main Magnet Room Top Chest",
                        "GM Outside Beamos Chest",
                        "GM Outside Clawshot Chest",
                        "GM Outside Underwater Chest"
                    }
                },
                {
                    "Lakebed Temple",
                    new[]
                    {
                        "LBT Before Deku Toad Alcove Chest",
                        "LBT Before Deku Toad Underwater Left Chest",
                        "LBT Before Deku Toad Underwater Right Chest",
                        "LBT Big Key Chest",
                        "LBT Central Room Chest",
                        "LBT Central Room Small Chest",
                        "LBT Central Room Spire Chest",
                        "LBT Chandelier Chest",
                        "LBT Deku Toad Chest",
                        "LBT Dungeon Reward",
                        "LBT East Lower Waterwheel Bridge Chest",
                        "LBT East Lower Waterwheel Stalactite Chest",
                        "LBT East Second Floor Southeast Chest",
                        "LBT East Second Floor Southwest Chest",
                        "LBT East Water Supply Clawshot Chest",
                        "LBT East Water Supply Small Chest",
                        "LBT Lobby Left Chest",
                        "LBT Lobby Rear Chest",
                        "LBT Morpheel Heart Container",
                        "LBT Stalactite Room Chest",
                        "LBT Underwater Maze Small Chest",
                        "LBT West Lower Small Chest",
                        "LBT West Second Floor Central Small Chest",
                        "LBT West Second Floor Northeast Chest",
                        "LBT West Second Floor Southeast Chest",
                        "LBT West Second Floor Southwest Underwater Chest",
                        "LBT West Water Supply Chest",
                        "LBT West Water Supply Small Chest"
                    }
                },
                {
                    "Arbiter's Grounds",
                    new[]
                    {
                        "AG Big Key Chest",
                        "AG Death Sword Chest",
                        "AG Dungeon Reward",
                        "AG East Lower Turnable Redead Chest",
                        "AG East Turning Room Poe",
                        "AG East Upper Turnable Chest",
                        "AG East Upper Turnable Redead Chest",
                        "AG Entrance Chest",
                        "AG Ghoul Rat Room Chest",
                        "AG Hidden Wall Poe",
                        "AG North Turning Room Chest",
                        "AG Spinner Room First Small Chest",
                        "AG Spinner Room Lower Central Small Chest",
                        "AG Spinner Room Lower North Chest",
                        "AG Spinner Room Second Small Chest",
                        "AG Spinner Room Stalfos Alcove Chest",
                        "AG Stallord Heart Container",
                        "AG Torch Room East Chest",
                        "AG Torch Room Poe",
                        "AG Torch Room West Chest",
                        "AG West Chandelier Chest",
                        "AG West Poe",
                        "AG West Small Chest Behind Block",
                        "AG West Stalfos Northeast Chest",
                        "AG West Stalfos West Chest"
                    }
                },
                {
                    "Snowpeak Ruins",
                    new[]
                    {
                        "SPR Ball and Chain",
                        "SPR Blizzeta Heart Container",
                        "SPR Broken Floor Chest",
                        "SPR Chapel Chest",
                        "SPR Chest After Darkhammer",
                        "SPR Courtyard Central Chest",
                        "SPR Dungeon Reward",
                        "SPR East Courtyard Buried Chest",
                        "SPR East Courtyard Chest",
                        "SPR Ice Room Poe",
                        "SPR Lobby Armor Poe",
                        "SPR Lobby Chandelier Chest",
                        "SPR Lobby East Armor Chest",
                        "SPR Lobby Poe",
                        "SPR Lobby West Armor Chest",
                        "SPR Mansion Map",
                        "SPR Northeast Chandelier Chest",
                        "SPR Ordon Pumpkin Chest",
                        "SPR West Cannon Room Central Chest",
                        "SPR West Cannon Room Corner Chest",
                        "SPR West Courtyard Buried Chest",
                        "SPR Wooden Beam Central Chest",
                        "SPR Wooden Beam Chandelier Chest",
                        "SPR Wooden Beam Northwest Chest"
                    }
                },
                {
                    "Temple of Time",
                    new[]
                    {
                        "ToT Armogohma Heart Container",
                        "ToT Armos Antechamber East Chest",
                        "ToT Armos Antechamber North Chest",
                        "ToT Armos Antechamber Statue Chest",
                        "ToT Big Key Chest",
                        "ToT Chest Before Darknut",
                        "ToT Darknut Chest",
                        "ToT Dungeon Reward",
                        "ToT First Staircase Armos Chest",
                        "ToT First Staircase Gohma Gate Chest",
                        "ToT First Staircase Window Chest",
                        "ToT Floor Switch Puzzle Room Upper Chest",
                        "ToT Guillotine Chest",
                        "ToT Lobby Lantern Chest",
                        "ToT Moving Wall Beamos Room Chest",
                        "ToT Moving Wall Dinalfos Room Chest",
                        "ToT Poe Above Scales",
                        "ToT Poe Behind Gate",
                        "ToT Scales Gohma Chest",
                        "ToT Scales Upper Chest"
                    }
                },
                {
                    "City in the Sky",
                    new[]
                    {
                        "CitS Aeralfos Chest",
                        "CitS Argorok Heart Container",
                        "CitS Baba Tower Alcove Chest",
                        "CitS Baba Tower Narrow Ledge Chest",
                        "CitS Baba Tower Top Small Chest",
                        "CitS Big Key Chest",
                        "CitS Central Outside Ledge Chest",
                        "CitS Central Outside Poe Island Chest",
                        "CitS Chest Behind North Fan",
                        "CitS Chest Below Big Key Chest",
                        "CitS Dungeon Reward",
                        "CitS East First Wing Chest After Fans",
                        "CitS East Tile Worm Small Chest",
                        "CitS East Wing After Dinalfos Alcove Chest",
                        "CitS East Wing After Dinalfos Ledge Chest",
                        "CitS East Wing Lower Level Chest",
                        "CitS Garden Island Poe",
                        "CitS Poe Above Central Fan",
                        "CitS Underwater East Chest",
                        "CitS Underwater West Chest",
                        "CitS West Garden Corner Chest",
                        "CitS West Garden Ledge Chest",
                        "CitS West Garden Lone Island Chest",
                        "CitS West Garden Lower Chest",
                        "CitS West Wing Baba Balcony Chest",
                        "CitS West Wing First Chest",
                        "CitS West Wing Narrow Ledge Chest",
                        "CitS West Wing Tile Worm Chest"
                    }
                },
                {
                    "Palace of Twilight",
                    new[]
                    {
                        "PoT Big Key Chest",
                        "PoT Central First Room Chest",
                        "PoT Central Outdoor Chest",
                        "PoT Central Tower Chest",
                        "PoT Collect Both Sols",
                        "PoT East Wing First Room East Alcove Chest",
                        "PoT East Wing First Room North Small Chest",
                        "PoT East Wing First Room West Alcove Chest",
                        "PoT East Wing First Room Zant Head Chest",
                        "PoT East Wing Second Room Northeast Chest",
                        "PoT East Wing Second Room Northwest Chest",
                        "PoT East Wing Second Room Southeast Chest",
                        "PoT East Wing Second Room Southwest Chest",
                        "PoT West Wing Chest Behind Wall of Darkness",
                        "PoT West Wing First Room Central Chest",
                        "PoT West Wing Second Room Central Chest",
                        "PoT West Wing Second Room Lower South Chest",
                        "PoT West Wing Second Room Southeast Chest",
                        "PoT Zant Heart Container"
                    }
                },
                {
                    "Hyrule Castle",
                    new[]
                    {
                        "HC Big Key Chest",
                        "HC East Wing Balcony Chest",
                        "HC East Wing Boomerang Puzzle Chest",
                        "HC Graveyard Grave Switch Room Back Left Chest",
                        "HC Graveyard Grave Switch Room Front Left Chest",
                        "HC Graveyard Grave Switch Room Right Chest",
                        "HC Graveyard Owl Statue Chest",
                        "HC King Bulblin Key",
                        "HC Lantern Staircase Chest",
                        "HC Main Hall Northeast Chest",
                        "HC Main Hall Northwest Chest",
                        "HC Main Hall Southwest Chest",
                        "HC Southeast Balcony Tower Chest",
                        "HC Treasure Room Eighth Small Chest",
                        "HC Treasure Room Fifth Chest",
                        "HC Treasure Room Fifth Small Chest",
                        "HC Treasure Room First Chest",
                        "HC Treasure Room First Small Chest",
                        "HC Treasure Room Fourth Chest",
                        "HC Treasure Room Fourth Small Chest",
                        "HC Treasure Room Second Chest",
                        "HC Treasure Room Second Small Chest",
                        "HC Treasure Room Seventh Small Chest",
                        "HC Treasure Room Sixth Small Chest",
                        "HC Treasure Room Third Chest",
                        "HC Treasure Room Third Small Chest",
                        "HC West Courtyard Central Small Chest",
                        "HC West Courtyard North Small Chest"
                    }
                },
            };

        static ZoneUtils()
        {
            enumToStr = new()
            {
                { Zone.Ordon, "Ordon" },
                { Zone.Sacred_Grove, "Sacred Grove" },
                { Zone.Faron_Field, "Faron Field" },
                { Zone.Faron_Woods, "Faron Woods" },
                { Zone.Kakariko_Gorge, "Kakariko Gorge" },
                { Zone.Kakariko_Village, "Kakariko Village" },
                { Zone.Kakariko_Graveyard, "Kakariko Graveyard" },
                { Zone.Eldin_Field, "Eldin Field" },
                { Zone.North_Eldin, "North Eldin" },
                { Zone.Death_Mountain, "Death Mountain" },
                { Zone.Hidden_Village, "Hidden Village" },
                { Zone.Lanayru_Field, "Lanayru Field" },
                { Zone.Beside_Castle_Town, "Beside Castle Town" },
                { Zone.South_of_Castle_Town, "South of Castle Town" },
                { Zone.Castle_Town, "Castle Town" },
                { Zone.Agithas_Castle, "Agitha's Castle" },
                { Zone.Great_Bridge_of_Hylia, "Great Bridge of Hylia" },
                { Zone.Lake_Hylia, "Lake Hylia" },
                { Zone.Lake_Lantern_Cave, "Lake Lantern Cave" },
                { Zone.Lanayru_Spring, "Lanayru Spring" },
                { Zone.Zoras_Domain, "Zora's Domain" },
                { Zone.Upper_Zoras_River, "Upper Zora's River" },
                { Zone.Gerudo_Desert, "Gerudo Desert" },
                { Zone.Bulblin_Camp, "Bulblin Camp" },
                { Zone.Snowpeak_Mountain, "Snowpeak Mountain" },
                { Zone.Cave_of_Ordeals, "Cave of Ordeals" },
                { Zone.Forest_Temple, "Forest Temple" },
                { Zone.Goron_Mines, "Goron Mines" },
                { Zone.Lakebed_Temple, "Lakebed Temple" },
                { Zone.Arbiters_Grounds, "Arbiter's Grounds" },
                { Zone.Snowpeak_Ruins, "Snowpeak Ruins" },
                { Zone.Temple_of_Time, "Temple of Time" },
                { Zone.City_in_the_Sky, "City in the Sky" },
                { Zone.Palace_of_Twilight, "Palace of Twilight" },
                { Zone.Hyrule_Castle, "Hyrule Castle" },
            };

            strToEnum = new();
            foreach (KeyValuePair<Zone, string> pair in enumToStr)
            {
                strToEnum[pair.Value] = pair.Key;
            }

            spotIdToId = new();
            foreach (KeyValuePair<Zone, SpotId> pair in idToSpotId)
            {
                spotIdToId[pair.Value] = pair.Key;
            }
            // Manually add in ToT midpoint sign which is not mapped to from a
            // Zone.
            spotIdToId[SpotId.Temple_of_Time_Beyond_Point_Sign] = Zone.Temple_of_Time;
        }

        public static Zone StringToId(string zoneName)
        {
            if (strToEnum.ContainsKey(zoneName))
                return strToEnum[zoneName];
            return Zone.Invalid;
        }

        public static Zone StringToIdThrows(string zoneName)
        {
            Zone zone = StringToId(zoneName);
            if (zone == Zone.Invalid)
                throw new Exception($"Expected zoneName '{zoneName}' to resolve to a valid zone.");
            return zone;
        }

        public static string IdToString(Zone zoneId)
        {
            if (enumToStr.ContainsKey(zoneId))
                return enumToStr[zoneId];
            return null;
        }

        public static SpotId IdToSpotId(Zone zoneId)
        {
            if (idToSpotId.ContainsKey(zoneId))
                return idToSpotId[zoneId];
            return SpotId.Invalid;
        }

        public static SpotId IdToSpotIdThrows(Zone zoneId)
        {
            SpotId result = IdToSpotId(zoneId);
            if (result == SpotId.Invalid)
                throw new Exception($"Failed to convert Zone {zoneId} to valid spotId.");
            return result;
        }

        public static Zone SpotIdToZone(SpotId spotId)
        {
            if (spotIdToId.ContainsKey(spotId))
                return spotIdToId[spotId];
            return Zone.Invalid;
        }

        public static bool IsDungeonZone(Zone zoneId)
        {
            return dungeonZones.Contains(zoneId);
        }

        public static bool IsDungeonZone(string stringId)
        {
            Zone zoneId = StringToId(stringId);
            return dungeonZones.Contains(zoneId);
        }
    }
}
