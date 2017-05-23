using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Data.SqlClient;
using System.Dynamic;

namespace PoeItemInfo
{
    public class SubmitItem
    {
        public void Execute(ItemEntity item)
        {
            string database = "PoeSimCraftItems";

            var db = new PetaPoco.Database(database);

            dynamic dynamic = new ExpandoObject();
            dynamic.Player = item.Player;
            dynamic.Host = item.Host;
            dynamic.Level = item.Level;
            dynamic.Type = item.Type;
            dynamic.Rarity = item.Rarity;

            var itemId = db.Insert("Item", "ItemId", true, dynamic);

            foreach (var affix in item.Affixes)
            {
                var sql = "INSERT INTO ItemAffix (ItemId, Affix)";
                sql += "VALUES (";
                sql += itemId + ", ";
                sql += "'" + affix + "')";
                db.Execute(sql);
            }
        }
    }
}
