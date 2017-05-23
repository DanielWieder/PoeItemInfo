using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoeItemInfo
{
    public class ItemParser
    {
        public ItemEntity Parse(string name, string host, string text)
        {
            ItemEntity e = new ItemEntity();
            e.Host = host;
            e.Player = name;

            var sections = text
                .Split(new[] { "--------" }, StringSplitOptions.None)
                .Where(x => !x.Contains("Corrupted")).ToList();

            var baseInfo = sections[0].Split(new[] { Environment.NewLine }, StringSplitOptions.None).Where(x => x != string.Empty).Where(x => !x.Contains("You cannot use this item"));
            e.Rarity = baseInfo.First(x => x.Contains("Rarity")).Replace("Rarity: ", "");
            e.Type = baseInfo.Last();

            var ilvlSection = sections.First(x => x.Contains("Item Level")).Split(new[] {Environment.NewLine}, StringSplitOptions.None).Where(x => x != string.Empty);
            var ilvlString = ilvlSection.First(x => x.Contains("Item Level")).Replace("Item Level: ", "");

            e.Level = int.Parse(ilvlString);

            int offset = 1;
            if (e.Rarity == "Unique")
            {
                offset = 2;
            }

            e.Affixes = sections[sections.Count - offset].Split(new[] {Environment.NewLine}, StringSplitOptions.None).Where(x => x != string.Empty).ToList();
            return e;
        }
    }
}
