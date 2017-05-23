using System.Collections.Generic;

namespace PoeItemInfo
{
    public class ItemEntity
    {
        public string Player { get; set; }
        public string Host { get; set; }
        public int Level { get; set; }
        public string Type { get; set; }
        public string Rarity { get; set; }
        public List<string> Affixes { get; set; }
    }
}
