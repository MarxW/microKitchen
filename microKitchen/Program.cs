using System;
using System.Collections;
using System.Threading;


using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;
using GHIElectronics.NETMF.Glide.Display;
using GHIElectronics.NETMF.Glide;
using GHIElectronics.NETMF.Glide.UI;
using Microsoft.SPOT;

namespace microKitchen
{
    public partial class Program
    {
        static Window[] windows = new Window[4];
        static int curentWindow = 0;
        static ShoppingList shoppingList = new ShoppingList();
        static List shoppingTypes;

        void ProgramStarted()
        {
            Glide.FitToScreen = true;
            Glide.Keyboard = Helper.InitKeyboard();
            
            windows[0] = GlideLoader.LoadWindow(Resources.GetString(Resources.StringResources.windowHome));
            windows[1] = GlideLoader.LoadWindow(Resources.GetString(Resources.StringResources.windowMusicPlayer));
            windows[2] = GlideLoader.LoadWindow(Resources.GetString(Resources.StringResources.windowShopping));
            windows[3] = GlideLoader.LoadWindow(Resources.GetString(Resources.StringResources.windowShoppingList));

            GlideTouch.Initialize();

            SetupWindows();

            Glide.MainWindow = windows[0];

            Debug.Print("Program Started");
            //Thread.Sleep(-1);
        }

        static void SetupWindows()
        {
            InitMainWindow();
            InitMusicWindow();
            InitShoppingWindow();
            InitShoppingList();
        }



        #region "main Window"

        static void InitMainWindow()
        {
            Button buttonMusic = (Button)(windows[0]).GetChildByName("buttonMusic");
            buttonMusic.TapEvent += new OnTap(buttonNavidateToMusic_click);
            Button buttonShopping = (Button)(windows[0]).GetChildByName("buttonShopping");
            buttonShopping.TapEvent += new OnTap(buttonNavidateToShopping_click);
        }

        static void buttonNavidateToMusic_click(object sender)
        {
            curentWindow = 1;
            Tween.SlideWindow(windows[0], windows[1], Direction.Up);
        }

        static void buttonNavidateToShopping_click(object sender)
        {
            curentWindow = 3;
            Tween.SlideWindow(windows[0], windows[3], Direction.Up);
        }

        #endregion

        
        #region "shopping List"

        static void UpdateShoppingListGrid()
        {
            DataGrid data = (DataGrid)windows[3].GetChildByName("gridShoppingList");
            data.Clear();
            for (int i = 0; i < shoppingList.Length(); i++)
            {
                ShoppingListItem item = shoppingList.GetItem(i);
                data.AddItem(new DataGridItem(new object[3] { item.Name, ((ShoppingItemTypes)item.Type).GetStringValue(), item.NumberOfItems.ToString() }));
            }
            data.Invalidate();
        }

        static void InitShoppingList()
        {
            DataGrid data = (DataGrid)windows[3].GetChildByName("gridShoppingList");
            data.AddColumn(new DataGridColumn("Name", 180));
            data.AddColumn(new DataGridColumn("Type", 90));
            data.AddColumn(new DataGridColumn("Number", 40));
            UpdateShoppingListGrid();
            Button buttonExit = (Button)windows[3].GetChildByName("buttonHome");
            buttonExit.TapEvent += new OnTap(buttonNavidateToHomeWindow_click);
            Button buttonNewItem = (Button)windows[3].GetChildByName("buttonAddItem");
            buttonNewItem.TapEvent += new OnTap(displayNewShopingListItem_click);
        }

        static void displayNewShopingListItem_click(object sender)
        {
            curentWindow = 2;
            Tween.SlideWindow(windows[3], windows[2], Direction.Up);
        }

        #endregion

        #region "shopping Window"

        static void InitShoppingWindow()
        {
            Button buttonExit = (Button)windows[2].GetChildByName("buttonCancel");
            buttonExit.TapEvent += new OnTap(cancelAddShoppingItem_click);
            TextBox textBox = (TextBox)(windows[2]).GetChildByName("textBoxItem");
            textBox.TapEvent += new OnTap(Glide.OpenKeyboard);
            textBox.ValueChangedEvent += new OnValueChanged(shoppingTextBoxItem_ValueChangedEvent);

            Dropdown dropDownCategories = (Dropdown)windows[2].GetChildByName("dropdownCategory");
            dropDownCategories.Options.Add(new object[] {ShoppingItemTypes.Dairy.GetStringValue(), ShoppingItemTypes.Dairy.GetStringValue()});
            dropDownCategories.Options.Add(new object[] {ShoppingItemTypes.Frozen.GetStringValue(), ShoppingItemTypes.Frozen.GetStringValue()});
            dropDownCategories.Options.Add(new object[] {ShoppingItemTypes.Meat_SeaFood.GetStringValue(), ShoppingItemTypes.Meat_SeaFood.GetStringValue()});
            dropDownCategories.Options.Add(new object[] {ShoppingItemTypes.UnCategorised.GetStringValue(), ShoppingItemTypes.UnCategorised.GetStringValue()});

            shoppingTypes = new List(dropDownCategories.Options, 200);
            shoppingTypes.CloseEvent += new OnClose(shoppingTypes_CloseEvent);

            dropDownCategories.TapEvent += new OnTap(dropDownCategories_TapEvent);
            dropDownCategories.Invalidate();
            Button buttonAddItem = (Button)(windows[2]).GetChildByName("buttonAdd");
            buttonAddItem.TapEvent += new OnTap(addShoppingListItem_click);
        }

        static void dropDownCategories_TapEvent(object sender)
        {
            Glide.OpenList(sender, shoppingTypes);
        }

        static void shoppingTypes_CloseEvent(object sender)
        {
            Glide.CloseList();
        }

        static void cancelAddShoppingItem_click(object sender)
        {
            TextBox textBox = (TextBox)(windows[2]).GetChildByName("textBoxItem");
            textBox.Text = string.Empty;
            textBox.Invalidate();
            curentWindow = 3;
            Tween.SlideWindow(windows[2], windows[3], Direction.Down);
        }

        static void addShoppingListItem_click(object sender)
        {
            TextBox textBox = (TextBox)(windows[2]).GetChildByName("textBoxItem");
            if (! "".Equals(textBox.Text.Trim()))
            {
                Dropdown dropDownCategories = (Dropdown)windows[2].GetChildByName("dropdownCategory");
                Debug.Print("adding " + textBox.Text);
                shoppingList.Add(textBox.Text, dropDownCategories.Value.ToString().ToShoppingItemTypes(), 1);
                textBox.Text = string.Empty;
                textBox.Invalidate();
                Debug.Print("cart has " + shoppingList.Length() + " items");
                UpdateShoppingListGrid();
                curentWindow = 3;
                Tween.SlideWindow(windows[2], windows[3], Direction.Down);
            }
        }

        static void shoppingTextBoxItem_ValueChangedEvent(object sender)
        {
            TextBox textBox = (TextBox)(windows[2]).GetChildByName("textBoxItem");
            Debug.Print(textBox.Text);
        }

        #endregion




        #region "music Window"

        static void InitMusicWindow()
        {
            Button buttonExit = (Button)(windows[1]).GetChildByName("buttonHome");
            buttonExit.TapEvent += new OnTap(buttonNavidateToHomeWindow_click);
        }

        static void buttonNavidateToHomeWindow_click(object sender)
        {
            Tween.SlideWindow(windows[curentWindow], windows[0], Direction.Down);
        }

        #endregion


    }
}
