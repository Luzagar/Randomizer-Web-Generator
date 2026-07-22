namespace TPRandomizer.Hints
{
    using System.Collections.Generic;

    public enum HintCategory
    {
        Invalid = 0,
        Grotto = 1,
        Post_dungeon = 2,
        Mist = 3,
        Owl_Statue = 4,
        Llc_Lantern_Chests = 5,
        Underwater = 6,
        Southern_Desert = 7,
        Northern_Desert = 8,
        Goron_Mines_2nd_Part = 9,
        Temple_of_Time_2nd_Half = 10,
        City_in_the_Sky_East_Wing = 11,
        Dungeon = 12,
        Lake_Lantern_Cave_2nd_Half = 13,
        Arbiters_Grounds_2nd_Half = 14,
        Lakebed_Temple_2nd_Wing = 15,
        Forest_Temple_West_Wing = 16,
        Snowpeak_Ruins_2nd_Floor = 17,
        Snowpeak_Beyond_This_Point = 18,
        Golden_Wolf = 19,
        Palace_of_Twilight_Center_Wing = 20,
    }

    public class HintCategoryUtils
    {
        public static readonly byte NumBitsToEncode = 5;
        private static Dictionary<HintCategory, string> enumToStr;
        private static Dictionary<string, HintCategory> strToEnum;

        private static Dictionary<string, HashSet<HintCategory>> checkToCategories;

        public static readonly Dictionary<HintCategory, string[]> categoryToChecksMap =
            new()
            {
                {
                    HintCategory.Grotto,
                    new[]
                    {
                        "Ordon Ranch Grotto Lantern Chest",
                        "Sacred Grove Baba Serpent Grotto Chest",
                        "Faron Field Corner Grotto Left Chest",
                        "Faron Field Corner Grotto Rear Chest",
                        "Faron Field Corner Grotto Right Chest",
                        "Eldin Field Bomskit Grotto Lantern Chest",
                        "Eldin Field Bomskit Grotto Left Chest",
                        "Eldin Field Water Bomb Fish Grotto Chest",
                        "Eldin Field Stalfos Grotto Left Small Chest",
                        "Eldin Field Stalfos Grotto Right Small Chest",
                        "Eldin Field Stalfos Grotto Stalfos Chest",
                        "Lanayru Field Poe Grotto Left Poe",
                        "Lanayru Field Poe Grotto Right Poe",
                        "Lanayru Field Skulltula Grotto Chest",
                        "BCT Helmasaur Grotto Chest",
                        "OCT South Tektite Grotto Chest",
                        "Lake Hylia Bridge Bubble Grotto Chest",
                        "Lake Hylia Shell Blade Grotto Chest",
                        "Lake Hylia Water Toadpoli Grotto Chest",
                        "Desert Rock Grotto First Poe",
                        "Desert Rock Grotto Lantern Chest",
                        "Desert Rock Grotto Second Poe",
                        "Desert Skulltula Grotto Chest",
                        "Snowpeak Freezard Grotto Chest",
                    }
                },
                {
                    HintCategory.Post_dungeon,
                    new[]
                    {
                        "Talo Sharpshooting",
                        "Kak Village Malo Mart Hawkeye",
                        "Death Mountain Trail Poe",
                        "Snowboard Racing Prize",
                        "Doctors Office Balcony Chest",
                        "Renados Letter",
                        "Telma Invoice",
                        "Wooden Statue",
                        "Ilia Memory Reward",
                    }
                },
                {
                    HintCategory.Mist,
                    new[]
                    {
                        "Faron Mist Cave Lantern Chest",
                        "Faron Mist Cave Open Chest",
                        "Faron Mist North Chest",
                        "Faron Mist South Chest",
                        "Faron Mist Stump Chest",
                    }
                },
                {
                    HintCategory.Owl_Statue,
                    new[]
                    {
                        "Sacred Grove Past Owl Statue Chest",
                        "Sacred Grove ToT Owl Statue Poe",
                        "Faron Woods Owl Statue Chest",
                        "Faron Woods Owl Statue Sky Character",
                        "Kak Gorge Owl Statue Chest",
                        "Kak Gorge Owl Statue Sky Character",
                        "Bridge of Eldin Owl Statue Chest",
                        "Bridge of Eldin Owl Statue Sky Character",
                        "Hyrule Field Amphitheater Owl Statue Chest",
                        "Hyrule Field Amphitheater Owl Statue Sky Character",
                        "Lake Hylia Bridge Owl Statue Chest",
                        "Lake Hylia Bridge Owl Statue Sky Character",
                        "Desert Owl Statue Chest",
                        "Desert Owl Statue Sky Character",
                        "HC Graveyard Owl Statue Chest",
                    }
                },
                {
                    HintCategory.Llc_Lantern_Chests,
                    new[] { "LLC Sixth Chest", "LLC End Lantern Chest", }
                },
                {
                    HintCategory.Underwater,
                    new[]
                    {
                        "Eldin Spring Underwater Chest",
                        "Lanayru Field Behind Gate Underwater Chest",
                        "Lake Hylia Underwater Chest",
                        "Lanayru Spring Underwater Left Chest",
                        "Lanayru Spring Underwater Right Chest",
                        "ZD Extinguish All Torches Chest",
                        "ZD Light All Torches Chest",
                        "ZD Underwater Goron",
                        "GM Crystal Switch Room Underwater Chest",
                        "GM Outside Underwater Chest",
                        "LBT Before Deku Toad Underwater Left Chest",
                        "LBT Before Deku Toad Underwater Right Chest",
                        "LBT Central Room Spire Chest",
                        "LBT West Second Floor Southwest Underwater Chest",
                        "CitS Underwater East Chest",
                        "CitS Underwater West Chest",
                        // Rupees:
                        "Eldin Spring Underwater Boulder Rupee",
                        "Kak Graveyard Underwater Boulder Rupee",
                        "Lake Hylia Left Underwater Pillar Rupee",
                        "Lake Hylia Right Underwater Pillar Rupee",
                        "Lake Hylia Left Underwater Boulder Rupee",
                        "Lake Hylia Right Underwater Boulder Rupee",
                        "Lanayru Field North Underwater Boulder Rupee",
                        "Lanayru Field South Underwater Boulder Rupee",
                        "Lanayru Spring Upper Underwater Boulder Rupee",
                        "Lanayru Spring Lower Underwater Boulder Rupee",
                        "ZD Central Underwater Boulder Rupee",
                        "ZD North Underwater Boulder Rupee",
                        "ZD Throne East Gate Underwater Rupee",
                        "ZD Throne West Gate Underwater Rupee",
                        "ZD Throne East Underwater Rupee",
                        "ZD Throne Northwest Underwater Rupee",
                        "ZD Throne West Underwater Rupee",
                        "ZD Throne South Underwater Rupee",
                    }
                },
                {
                    HintCategory.Southern_Desert,
                    new[]
                    {
                        "Desert East Canyon Chest",
                        "Desert East Poe",
                        "Desert Female Dayfly",
                        "Desert Lone Small Chest",
                        "Desert Male Dayfly",
                        "Desert Owl Statue Chest",
                        "Desert Owl Statue Sky Character",
                        "Desert Peahat Ledge Chest",
                        "Desert Poe Above CoO",
                        "Desert Skulltula Grotto Chest",
                        "Desert South Chest Behind Wooden Gates",
                        "Desert West Canyon Chest"
                    }
                },
                {
                    HintCategory.Northern_Desert,
                    new[]
                    {
                        "Desert Campfire East Chest",
                        "Desert Campfire North Chest",
                        "Desert Campfire West Chest",
                        "Desert North Peahat Poe",
                        "Desert North Small Chest Before Bulblin Camp",
                        "Desert Northeast Chest Behind Gates",
                        "Desert Northwest Chest Behind Gates",
                        "Desert Rock Grotto First Poe",
                        "Desert Rock Grotto Lantern Chest",
                        "Desert Rock Grotto Second Poe",
                    }
                },
                {
                    // The entire side path from the open room with water at the
                    // bottom.
                    HintCategory.Goron_Mines_2nd_Part,
                    new[]
                    {
                        "GM Gor Ebizo Chest",
                        "GM Gor Ebizo Key Shard",
                        "GM Chest Before Dangoro",
                        "GM Dangoro Chest",
                        "GM Beamos Room Chest",
                        "GM Gor Liggs Chest",
                        "GM Gor Liggs Key Shard",
                        "GM Main Magnet Room Top Chest",
                    }
                },
                {
                    HintCategory.Temple_of_Time_2nd_Half,
                    new[]
                    {
                        "ToT Moving Wall Dinalfos Room Chest",
                        "ToT Scales Gohma Chest",
                        "ToT Poe Above Scales",
                        "ToT Scales Upper Chest",
                        "ToT Floor Switch Puzzle Room Upper Chest",
                        "ToT Big Key Chest",
                        "ToT Guillotine Chest",
                        "ToT Chest Before Darknut",
                        "ToT Darknut Chest",
                    }
                },
                {
                    HintCategory.City_in_the_Sky_East_Wing,
                    new[]
                    {
                        "CitS East First Wing Chest After Fans",
                        "CitS East Tile Worm Small Chest",
                        "CitS East Wing After Dinalfos Alcove Chest",
                        "CitS East Wing After Dinalfos Ledge Chest",
                        "CitS East Wing Lower Level Chest",
                        "CitS Aeralfos Chest",
                    }
                },
                // We don't put `Dungeon` here since it isn't used and it would
                // be massive.
                {
                    HintCategory.Lake_Lantern_Cave_2nd_Half,
                    new[]
                    {
                        "LLC Second Poe",
                        "LLC Final Poe",
                        "LLC Ninth Chest",
                        "LLC Tenth Chest",
                        "LLC Eleventh Chest",
                        "LLC Twelfth Chest",
                        "LLC Thirteenth Chest",
                        "LLC Fourteenth Chest",
                        "LLC End Lantern Chest",
                    }
                },
                {
                    HintCategory.Snowpeak_Beyond_This_Point,
                    new[]
                    {
                        // All but "Ashei Sketch" are technically beyond the sign. However, we only
                        // end up creating this hint when it would hint about "Snowpeak Cave Ice
                        // Lantern Chest" and "Snowpeak Freezard Grotto Chest" and the dungeons
                        // behind SPR are unrequiredBarren.
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
                        "Snowboarding Top Right Rupee",
                        // Include the golden wolf which depends on the howling stone.
                        "Kak Graveyard Golden Wolf",
                    }
                },
                {
                    HintCategory.Arbiters_Grounds_2nd_Half,
                    new[]
                    {
                        "AG North Turning Room Chest",
                        "AG Big Key Chest",
                        "AG Spinner Room First Small Chest",
                        "AG Spinner Room Lower Central Small Chest",
                        "AG Spinner Room Lower North Chest",
                        "AG Spinner Room Second Small Chest",
                        "AG Spinner Room Stalfos Alcove Chest",
                        "AG Death Sword Chest",
                        "AG Stallord Heart Container",
                        "AG Dungeon Reward",
                    }
                },
                {
                    HintCategory.Lakebed_Temple_2nd_Wing,
                    new[]
                    {
                        "LBT West Lower Small Chest",
                        "LBT West Second Floor Central Small Chest",
                        "LBT West Second Floor Northeast Chest",
                        "LBT West Second Floor Southeast Chest",
                        "LBT West Second Floor Southwest Underwater Chest",
                        "LBT West Water Supply Chest",
                        "LBT West Water Supply Small Chest",
                        "LBT Underwater Maze Small Chest",
                        "LBT Big Key Chest",
                    }
                },
                {
                    HintCategory.Forest_Temple_West_Wing,
                    new[]
                    {
                        "FT Big Baba Key",
                        "FT Totem Pole Chest",
                        "FT West Deku Like Chest",
                        "FT West Tile Worm Chest Behind Stairs",
                        "FT West Tile Worm Room Vines Chest",
                        "FT Gale Boomerang",
                    }
                },
                {
                    HintCategory.Snowpeak_Ruins_2nd_Floor,
                    new[]
                    {
                        "SPR Chapel Chest",
                        "SPR Ice Room Poe",
                        "SPR Lobby Chandelier Chest",
                        "SPR Northeast Chandelier Chest",
                        "SPR Wooden Beam Chandelier Chest",
                    }
                },
                {
                    HintCategory.Golden_Wolf,
                    new[]
                    {
                        "Faron Woods Golden Wolf",
                        "Desert Golden Wolf",
                        "Kak Graveyard Golden Wolf",
                        "North CT Golden Wolf",
                        "Ordon Spring Golden Wolf",
                        "OCT South Golden Wolf",
                        "BCT Golden Wolf"
                    }
                },
                {
                    HintCategory.Palace_of_Twilight_Center_Wing,
                    new[]
                    {
                        "PoT Big Key Chest",
                        "PoT Central First Room Chest",
                        "PoT Central Outdoor Chest",
                        "PoT Central Tower Chest",
                        "PoT Zant Heart Container",
                    }
                }
            };

        static HintCategoryUtils()
        {
            enumToStr = new()
            {
                { HintCategory.Grotto, "Grotto" },
                { HintCategory.Post_dungeon, "Post_dungeon" },
                { HintCategory.Mist, "Mist" },
                { HintCategory.Owl_Statue, "Owl_Statue" },
                { HintCategory.Llc_Lantern_Chests, "Llc_Lantern_Chests" },
                { HintCategory.Underwater, "Underwater" },
                { HintCategory.Southern_Desert, "Southern_Desert" },
                { HintCategory.Northern_Desert, "Northern_Desert" },
                { HintCategory.Goron_Mines_2nd_Part, "Goron_Mines_2nd_Part" },
                { HintCategory.Temple_of_Time_2nd_Half, "Temple_of_Time_2nd_Half" },
                { HintCategory.City_in_the_Sky_East_Wing, "City_in_the_Sky_East_Wing" },
                { HintCategory.Dungeon, "Dungeon" },
                { HintCategory.Lake_Lantern_Cave_2nd_Half, "Lake_Lantern_Cave_2nd_Half" },
                { HintCategory.Arbiters_Grounds_2nd_Half, "Arbiters_Grounds_2nd_Half" },
                { HintCategory.Lakebed_Temple_2nd_Wing, "Lakebed_Temple_2nd_Wing" },
                { HintCategory.Snowpeak_Ruins_2nd_Floor, "Snowpeak_Ruins_2nd_Floor" },
                { HintCategory.Snowpeak_Beyond_This_Point, "Snowpeak_Beyond_This_Point" },
                { HintCategory.Golden_Wolf, "Golden_Wolf" },
                { HintCategory.Palace_of_Twilight_Center_Wing, "Palace_of_Twilight_Center_Wing" },
            };

            strToEnum = new();
            foreach (KeyValuePair<HintCategory, string> pair in enumToStr)
            {
                strToEnum[pair.Value] = pair.Key;
            }

            checkToCategories = new();
            foreach (KeyValuePair<HintCategory, string[]> pair in categoryToChecksMap)
            {
                foreach (string checkName in pair.Value)
                {
                    if (
                        !checkToCategories.TryGetValue(
                            checkName,
                            out HashSet<HintCategory> categories
                        )
                    )
                    {
                        categories = new();
                        checkToCategories[checkName] = categories;
                    }
                    categories.Add(pair.Key);
                }
            }
        }

        public static HintCategory StringToId(string category)
        {
            if (strToEnum.ContainsKey(category))
                return strToEnum[category];
            return HintCategory.Invalid;
        }

        public static string IdToString(HintCategory category)
        {
            if (enumToStr.ContainsKey(category))
                return enumToStr[category];
            return null;
        }

        public static HashSet<HintCategory> checkNameToCategories(string checkName)
        {
            if (checkToCategories.TryGetValue(checkName, out HashSet<HintCategory> categories))
            {
                return categories;
            }
            return null;
        }
    }
}
