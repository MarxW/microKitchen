using System;
using Microsoft.SPOT;
using GHIElectronics.NETMF.Glide.Display;
using GHIElectronics.NETMF.Glide.UI;
using GHIElectronics.NETMF.Glide;
using microKitchen.Model;

namespace microKitchen.Windows
{
    class ShoppingWindow : Window
    {
        public Button ButtonNavigateToHome { get; internal set; }
        public Button ButtonAddShoppingItem { get; internal set; }
        public DataGrid DataGridShoppingList { get; internal set; }
        public ShoppingList ShoppingList { get; set; }
        private AddShoppingItemDialog addShoppingItemDialog;

        #region "Initialise"

        public ShoppingWindow()
            : base("wondowShopping", 320, 240)
        {
            this.InitWindow();
        }

        public ShoppingWindow(ShoppingList shoppingList)
            : base("wondowShopping", 320, 240)
        {
            this.InitWindow();
        }

        private void InitWindow()
        {
            this.BackColor = Colors.White;
            // Cancel button
            this.ButtonNavigateToHome = new Button("buttonNavigateToHome", 0, 93, 205, 80, 32);
            this.ButtonNavigateToHome.Text = "Cancel";
            this.ButtonNavigateToHome.TintColor = Colors.Black;
            this.ButtonNavigateToHome.TintAmount = 10;
            this.ButtonNavigateToHome.TapEvent += new OnTap(OnButtonNavigateToHome_Taped);
            this.AddChild(this.ButtonNavigateToHome);
            // Add Item Button
            this.ButtonAddShoppingItem = new Button("buttonNavigateToHome", 0, 5, 205, 80, 32);
            this.ButtonAddShoppingItem.Text = "Add Item";
            this.ButtonAddShoppingItem.TintColor = Colors.Black;
            this.ButtonAddShoppingItem.TintAmount = 10;
            this.ButtonAddShoppingItem.TapEvent += new OnTap(OnButtonAddShoppingItem_Taped);
            this.AddChild(this.ButtonAddShoppingItem);
            // Add ShoppingList DataGrid
            this.DataGridShoppingList = new DataGrid("datagridShoppingList", 0, 0, 0, 320, 20, 8);
            this.DataGridShoppingList.Draggable = false;
            this.DataGridShoppingList.SortableHeaders = true;
            this.DataGridShoppingList.TappableCells = true;
            this.DataGridShoppingList.ShowHeaders = true;
            this.DataGridShoppingList.ShowScrollbar = true;
            this.DataGridShoppingList.ScrollbarWidth = 10;
            this.DataGridShoppingList.AddColumn(new DataGridColumn("Name", 170));
            this.DataGridShoppingList.AddColumn(new DataGridColumn("Type", 90));
            this.DataGridShoppingList.AddColumn(new DataGridColumn("Number", 50));
            this.DataGridShoppingList.TapCellEvent += new OnTapCell(dgShoppingList_TapCellEvent);
            this.AddChild(this.DataGridShoppingList);

            this.addShoppingItemDialog = new AddShoppingItemDialog();
            this.addShoppingItemDialog.CloseButton.TapEvent += new OnTap(AddShoppingItemDialog_CloseButton_TapEvent);
            this.addShoppingItemDialog.AddButton.TapEvent += new OnTap(AddShoppingItemDialog_AddButton_TapEvent);
            this.ShoppingList = Helpers.Configuration.LoadShoppingList();
            this.UpdateShoppingListDataGrid();

            this.UpdateShoppingListDataGrid();
        }

        #endregion

        #region "Events"

        void dgShoppingList_TapCellEvent(object sender, TapCellEventArgs args)
        {
            Debug.Print(GlideTouch.Calibrated.ToString());
            ModalResult result = Glide.MessageBoxManager.Show("Delete this item?", "Delete", ModalButtons.YesNo);
            if (result == ModalResult.Yes)
            {
                object[] data = this.dgShoppingList.GetRowData(args.RowIndex);
                if (null != data)
                {
                    for (int i = 0; i < this.shoppingList.Length(); i++)
                    {
                        ShoppingListItem item = this.shoppingList.GetItem(i);
                        if (item.Name.Equals((string)data[0]) && item.Type == ((string)data[1]).ToShoppingItemTypes() && item.NumberOfItems == (int)data[2])
                        {
                            this.shoppingList.Remove(item);
                            Helpers.Configuration.SaveShoppingList(this.shoppingList);
                            this.UpdateShoppingListDataGrid();
                            break;
                        }
                    }
                }
            }
        }

        private void OnButtonNavigateToHome_Taped(object sender)
        {
            Tween.SlideWindow(this, new HomeWindow(), Direction.Up);
            this.Dispose();
        }

        private void OnButtonAddShoppingItem_Taped(object sender)
        {
            Tween.SlideWindow(this, this.addShoppingItemDialog, Direction.Up);
        }

        private void AddShoppingItemDialog_CloseButton_TapEvent(object sender)
        {
            Tween.SlideWindow(this.addShoppingItemDialog, this, Direction.Up);
            this.addShoppingItemDialog.DropdownCategories.Value = "";
            this.addShoppingItemDialog.DropdownCategories.Text = "Select:";
            this.addShoppingItemDialog.ItemName = string.Empty;
            this.addShoppingItemDialog.NumberOfItems = 1;
        }

        private void AddShoppingItemDialog_AddButton_TapEvent(object sender)
        {
            Tween.SlideWindow(this.addShoppingItemDialog, this, Direction.Up);
            this.shoppingList.Add(this.addShoppingItemDialog.ItemName,
                             this.addShoppingItemDialog.DropdownCategories.Value.ToString().ToShoppingItemTypes(),
                             this.addShoppingItemDialog.NumberOfItems);
            this.addShoppingItemDialog.DropdownCategories.Value = "";
            this.addShoppingItemDialog.DropdownCategories.Text = "Select:";
            this.addShoppingItemDialog.ItemName = string.Empty;
            this.addShoppingItemDialog.NumberOfItems = 1;
            Helpers.Configuration.SaveShoppingList(this.shoppingList);
            this.UpdateShoppingListDataGrid();
        }

        #endregion

        #region "Private Methods"

        private void UpdateShoppingListDataGrid()
        {
            this.dgShoppingList.Clear();
            for (int i = 0; i < this.shoppingList.Length(); i++)
            {
                ShoppingListItem item = this.shoppingList.GetItem(i);
                this.dgShoppingList.AddItem(new DataGridItem(new object[3] { item.Name, ((ShoppingItemTypes)item.Type).GetStringValue(), item.NumberOfItems.ToString() }));
            }
            this.dgShoppingList.Invalidate();
        }

        #endregion

    }
}
