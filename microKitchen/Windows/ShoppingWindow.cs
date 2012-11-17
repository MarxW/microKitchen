using System;
using Microsoft.SPOT;
using GHIElectronics.NETMF.Glide.Display;
using GHIElectronics.NETMF.Glide.UI;
using GHIElectronics.NETMF.Glide;
using microKitchen.Shopping;

namespace microKitchen.Windows
{
    class ShoppingWindow : Window
    {
        private Button bNavigateToHome, bAddShoppingItem;
        private DataGrid dgShoppingList;
        private ShoppingList shoppingList;
        private AddShoppingItemDialog addShoppingItemDialog;

        public ShoppingList ShoppingList
        {
            get { return this.shoppingList; }
            set { this.shoppingList = value; }
        }

        public Button ButtonNavigateToHome
        {
            get { return this.bNavigateToHome; }
        }

        public Button ButtonAddShoppingItem
        {
            get { return this.bAddShoppingItem; }
        }

        public DataGrid DataGridShoppingList
        {
            get { return this.dgShoppingList; }
        }

        #region "Initialise"

        public ShoppingWindow()
            : base("wondowShopping", 320, 240)
        {
            this.BackColor = Colors.White;
            this.shoppingList = new ShoppingList();
            this.InitWindow();
            this.UpdateShoppingListDataGrid();
        }

        public ShoppingWindow(ShoppingList shoppingList)
            : base("wondowShopping", 320, 240)
        {
            this.BackColor = Colors.White;
            this.shoppingList = shoppingList;
            this.InitWindow();
            this.UpdateShoppingListDataGrid();
        }

        private void InitWindow()
        {
            // Cancel button
            this.bNavigateToHome = new Button("buttonNavigateToHome", 0, 93, 205, 80, 32);
            this.bNavigateToHome.Text = "Cancel";
            this.bNavigateToHome.TintColor = Colors.Black;
            this.bNavigateToHome.TintAmount = 10;
            this.bNavigateToHome.TapEvent += new OnTap(OnButtonNavigateToHome_Taped); 
            this.AddChild(this.bNavigateToHome);
            // Add Item Button
            this.bAddShoppingItem = new Button("buttonNavigateToHome", 0, 5, 205, 80, 32);
            this.bAddShoppingItem.Text = "Add Item";
            this.bAddShoppingItem.TintColor = Colors.Black;
            this.bAddShoppingItem.TintAmount = 10;
            this.bAddShoppingItem.TapEvent += new OnTap(OnButtonAddShoppingItem_Taped); 
            this.AddChild(this.bAddShoppingItem);
            // Add ShoppingList DataGrid
            this.dgShoppingList = new DataGrid("datagridShoppingList", 0, 0, 0, 320, 20, 8);
            this.dgShoppingList.Draggable = false;
            this.dgShoppingList.SortableHeaders = true;
            this.dgShoppingList.TappableCells = true;
            this.dgShoppingList.ShowHeaders = true;
            this.dgShoppingList.ShowScrollbar = true;
            this.dgShoppingList.ScrollbarWidth = 10;
            this.dgShoppingList.AddColumn(new DataGridColumn("Name", 170));
            this.dgShoppingList.AddColumn(new DataGridColumn("Type", 90));
            this.dgShoppingList.AddColumn(new DataGridColumn("Number", 50));
            this.dgShoppingList.TapCellEvent += new OnTapCell(dgShoppingList_TapCellEvent);
            this.AddChild(this.dgShoppingList);

            this.addShoppingItemDialog = new AddShoppingItemDialog();
            this.addShoppingItemDialog.CloseButton.TapEvent += new OnTap(AddShoppingItemDialog_CloseButton_TapEvent);
            this.addShoppingItemDialog.AddButton.TapEvent += new OnTap(AddShoppingItemDialog_AddButton_TapEvent);
            this.shoppingList = Helpers.Configuration.LoadShoppingList();
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
