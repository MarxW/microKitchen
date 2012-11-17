using System;
using Microsoft.SPOT;
using Microsoft.SPOT.IO;
using System.IO;
using System.Text;
using GHI.OSHW.Hardware;
using microKitchen.Model;
using Gadgeteer.Modules.GHIElectronics;

namespace microKitchen.Helpers
{
    static class Configuration
    {
        private const string SHOPPINGLIST_FILE = @"\SD\CONFIG\ShoppingList";

        public static void SaveShoppingList(ShoppingList shoppingList)
        {
            byte[] data = Reflection.Serialize(shoppingList, typeof(ShoppingList));
            if (File.Exists(SHOPPINGLIST_FILE))
            {
                File.Delete(SHOPPINGLIST_FILE);
            }
            FileStream fStream = new FileStream(SHOPPINGLIST_FILE, FileMode.Create, FileAccess.Write);
            fStream.Write(data, 0, data.Length);
            fStream.Close();
        }

        public static ShoppingList LoadShoppingList()
        {
            FileStream fStream;
            byte[] data;
            if (File.Exists(SHOPPINGLIST_FILE))
            {
                fStream = new FileStream(SHOPPINGLIST_FILE, FileMode.Open, FileAccess.Read);
                data = new byte[fStream.Length];
                fStream.Read(data, 0, data.Length);
                fStream.Close();
                try
                {
                    return (ShoppingList)Reflection.Deserialize(data, typeof(ShoppingList));
                }
                catch
                {
                    Debug.Print("invalid shoppinglist file");
                }
            }
            ShoppingList sList = new ShoppingList();
            data = Reflection.Serialize(sList, typeof(ShoppingList));
            fStream = new FileStream(SHOPPINGLIST_FILE, FileMode.OpenOrCreate, FileAccess.Write);
            fStream.Write(data, 0, data.Length);
            fStream.Close();
            return sList;
        }

        public static string GetSDRootDirectory()
        {
            return VolumeInfo.GetVolumes()[0].RootDirectory;
        }

        public static string bytesToString(byte[] bytes)
        {
            string s = string.Empty;
            for (int i = 0; i < bytes.Length; ++i)
            {
                s += (char)bytes[i];
            }
            return s;
        }
    }
}
