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
            this.dgShoppingList = new DataGrid("datagridShoppingList", 0, 0, 0, 320, 20, 10);
            this.dgShoppingList.Draggable = false;
            this.dgShoppingList.SortableHeaders = true;
            this.dgShoppingList.TappableCells = true;
            this.dgShoppingList.ShowHeaders = true;
            this.dgShoppingList.ShowScrollbar = true;
            this.dgShoppingList.ScrollbarWidth = 10;
            this.dgShoppingList.AddColumn(new DataGridColumn("Name", 170));
            this.dgShoppingList.AddColumn(new DataGridColumn("Type", 90));
            this.dgShoppingList.AddColumn(new DataGridColumn("Number", 50));
            this.AddChild(this.dgShoppingList);
        }

        #endregion

        #region "Events"

        private void OnButtonNavigateToHome_Taped(object sender)
        {
            Tween.SlideWindow(this, new HomeWindow(), Direction.Up);
            this.Dispose();
        }

        private void OnButtonAddShoppingItem_Taped(object sender)
        {
            this.AddChild(new AddShoppingItemDialog());
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
