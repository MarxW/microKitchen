using System;
using Microsoft.SPOT;
using Gadgeteer.Modules.GHIElectronics;
using System.Threading;
using GHIElectronics.NETMF.Glide.Display;
using GHIElectronics.NETMF.Glide.UI;
using GHIElectronics.NETMF.Glide;
using microKitchen.Model;

namespace microKitchen
{

    static class Helper
    {

        public static string GetStringValue(this ShoppingItemTypes type) {
            switch (type)
            {
                case ShoppingItemTypes.Dairy:
                    return "Dairy";
                case ShoppingItemTypes.Frozen:
                    return "Frozen";
                case ShoppingItemTypes.Meat_SeaFood:
                    return "Meat_SeaFood";
                case ShoppingItemTypes.UnCategorised:
                    return "UnCategorised";
            }
            return string.Empty;
        }

        public static ShoppingItemTypes ToShoppingItemTypes(this string value)
        {
            switch (value)
            {
                case "Dairy": return ShoppingItemTypes.Dairy;
                case "Frozen": return ShoppingItemTypes.Frozen;
                case "Meat_SeaFood": return ShoppingItemTypes.Meat_SeaFood;
                case "UnCategorised": return ShoppingItemTypes.UnCategorised;
            }
            return ShoppingItemTypes.UnCategorised;
        }

        #region "Keyboard"

        public static Keyboard InitKeyboard()
        {
            Keyboard keyboard = new Keyboard(320, 128, 3, 32, 0);

            // Each view with keys in a up position.
            keyboard.BitmapUp = new Bitmap[4]
            {
                Resources.GetBitmap(Resources.BitmapResources.Keyboard_320x128_Uppercase),
                Resources.GetBitmap(Resources.BitmapResources.Keyboard_320x128_Lowercase),
                Resources.GetBitmap(Resources.BitmapResources.Keyboard_320x128_Numbers),
                Resources.GetBitmap(Resources.BitmapResources.Keyboard_320x128_Symbols)
            };

            // We must set the default key content.

            string[][] keyContent = new string[4][];

            // Letters
            keyContent[0] = new string[10] { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p" };
            keyContent[1] = new string[9] { "a", "s", "d", "f", "g", "h", "j", "k", "l" };
            keyContent[2] = new string[9] { Keyboard.ActionKey.ToggleCase, "z", "x", "c", "v", "b", "n", "m", Keyboard.ActionKey.Backspace };
            keyContent[3] = new string[5] { Keyboard.ActionKey.ToNumbers, ",", Keyboard.ActionKey.Space, ".", Keyboard.ActionKey.Return };
            keyboard.SetViewKeyContent(Keyboard.View.Letters, keyContent);

            // Numbers
            keyContent[0] = new string[10] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            keyContent[1] = new string[10] { "@", "#", "$", "%", "&", "*", "-", "+", "(", ")" };
            keyContent[2] = new string[9] { Keyboard.ActionKey.ToSymbols, "!", "\"", "'", ":", ";", "/", "?", Keyboard.ActionKey.Backspace };
            keyContent[3] = new string[5] { Keyboard.ActionKey.ToLetters, ",", Keyboard.ActionKey.Space, ".", Keyboard.ActionKey.Return };
            keyboard.SetViewKeyContent(Keyboard.View.Numbers, keyContent);

            // Symbols
            keyContent[0] = new string[10] { "~", "`", "|", "•", "√", "π", "÷", "×", "{", "}" };
            keyContent[1] = new string[10] { Keyboard.ActionKey.Tab, "£", "¢", "€", "º", "^", "_", "=", "[", "]" };
            keyContent[2] = new string[9] { Keyboard.ActionKey.ToNumbers, "™", "®", "©", "¶", "\\", "<", ">", Keyboard.ActionKey.Backspace };
            keyContent[3] = new string[5] { Keyboard.ActionKey.ToLetters, ",", Keyboard.ActionKey.Space, ".", Keyboard.ActionKey.Return };
            keyboard.SetViewKeyContent(Keyboard.View.Symbols, keyContent);

            int[][] keyWidth = new int[4][];

            // Letters
            keyWidth[0] = new int[10] { 32, 32, 32, 32, 32, 32, 32, 32, 32, 32 };
            keyWidth[1] = new int[9] { 32, 32, 32, 32, 32, 32, 32, 32, 32 };
            keyWidth[2] = new int[9] { 48, 32, 32, 32, 32, 32, 32, 32, 48 };
            keyWidth[3] = new int[5] { 48, 32, 160, 32, 48 };

            keyboard.SetViewKeyWidth(Keyboard.View.Letters, keyWidth);

            // Numbers
            keyWidth[0] = new int[10] { 32, 32, 32, 32, 32, 32, 32, 32, 32, 32 };
            keyWidth[1] = new int[10] { 32, 32, 32, 32, 32, 32, 32, 32, 32, 32 };
            keyWidth[2] = new int[9] { 48, 32, 32, 32, 32, 32, 32, 32, 48 };
            keyWidth[3] = new int[5] { 48, 32, 160, 32, 48 };

            keyboard.SetViewKeyWidth(Keyboard.View.Numbers, keyWidth);

            // Symbols
            keyWidth[0] = new int[10] { 32, 32, 32, 32, 32, 32, 32, 32, 32, 32 };
            keyWidth[1] = new int[10] { 32, 32, 32, 32, 32, 32, 32, 32, 32, 32 };
            keyWidth[2] = new int[9] { 48, 32, 32, 32, 32, 32, 32, 32, 48 };
            keyWidth[3] = new int[5] { 48, 32, 160, 32, 48 };

            keyboard.SetViewKeyWidth(Keyboard.View.Symbols, keyWidth);

            keyboard.CalculateKeys();
            return keyboard;
        }

        #endregion


    }

}
