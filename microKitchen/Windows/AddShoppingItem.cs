using System;
using Microsoft.SPOT;
using GHIElectronics.NETMF.Glide.UI;
using microKitchen.Model;
using GHIElectronics.NETMF.Glide;
using GHIElectronics.NETMF.Glide.Display;
using Gadgeteer;

namespace microKitchen.Windows
{
    class AddShoppingItemDialog : Window
    {
        private Dropdown dropdownCategories;
        private Button buttonAddItem, buttonClose, buttonCoutnUp, buttonCountDown;
        private TextBox textboxItemName, textboxNumberOfItems;
        private List shoppingTypes;

        public int NumberOfItems
        {
            get
            {
                try { return int.Parse(this.textboxNumberOfItems.Text); }
                catch { return 1; }
            }
            set
            {
                this.textboxNumberOfItems.Text = value.ToString();
            }
        }

        public string ItemName
        {
            get { return this.textboxItemName.Text; }
            set { this.textboxItemName.Text = value; }
        }

        public Dropdown DropdownCategories
        {
            get { return this.dropdownCategories; }
        }

        public Button CloseButton
        {
            get { return this.buttonClose; }
        }

        public Button AddButton
        {
            get { return this.buttonAddItem; }
        }

        #region "Initialise"

        public AddShoppingItemDialog()
            : base("addShoppingItemModal", 320, 240)
        {
            this.Initialise();
        }

        public AddShoppingItemDialog(string name, int width, int height)
            : base(name, width, height)
        {
            this.Initialise();
        }

        private void Initialise()
        {
            this.BackColor = Colors.White;

            this.dropdownCategories = new Dropdown("dropdownCategory", 0, 10, 57, 300, 32);
            this.dropdownCategories.Options.Add(new object[] { ShoppingItemTypes.Dairy.GetStringValue(), ShoppingItemTypes.Dairy.GetStringValue() });
            this.dropdownCategories.Options.Add(new object[] { ShoppingItemTypes.Frozen.GetStringValue(), ShoppingItemTypes.Frozen.GetStringValue() });
            this.dropdownCategories.Options.Add(new object[] { ShoppingItemTypes.Meat_SeaFood.GetStringValue(), ShoppingItemTypes.Meat_SeaFood.GetStringValue() });
            this.dropdownCategories.Options.Add(new object[] { ShoppingItemTypes.UnCategorised.GetStringValue(), ShoppingItemTypes.UnCategorised.GetStringValue() });
            this.dropdownCategories.Text = "Select:";
            this.dropdownCategories.Alpha = 255;
            this.dropdownCategories.TapEvent += new OnTap(dropDownCategories_TapEvent);
            this.AddChild(this.dropdownCategories);

            this.shoppingTypes = new List(dropdownCategories.Options, 200);
            this.shoppingTypes.CloseEvent += new OnClose(shoppingTypes_CloseEvent);

            this.textboxNumberOfItems = new TextBox("textboxNumberOfItems", 0, 75, 100, 80, 32);
            this.textboxNumberOfItems.Text = "1";
            this.textboxNumberOfItems.Alpha = 255;
            this.AddChild(this.textboxNumberOfItems);

            this.buttonCoutnUp = new Button("buttonCountUp", 0, 160, 100, 32, 32);
            this.buttonCoutnUp.Text = "+";
            this.buttonCoutnUp.TintColor = Colors.Black;
            this.buttonCoutnUp.TintAmount = 10;
            this.buttonCoutnUp.TapEvent += new OnTap(buttonCoutnUp_TapEvent);
            this.AddChild(this.buttonCoutnUp);

            this.buttonCountDown = new Button("buttonCountDown", 0, 195, 100, 32, 32);
            this.buttonCountDown.Text = "-";
            this.buttonCountDown.TintColor = Colors.Black;
            this.buttonCountDown.TintAmount = 10;
            this.buttonCountDown.TapEvent += new OnTap(buttonCountDown_TapEvent);
            this.AddChild(this.buttonCountDown);

            this.buttonClose = new Button("buttonCancel", 0, 75, 180, 150, 32);
            this.buttonClose.Text = "Cancel";
            this.buttonClose.TintColor = Colors.Black;
            this.buttonClose.TintAmount = 10;
            this.buttonClose.TapEvent += new OnTap(buttonClose_TapEvent);
            this.AddChild(this.buttonClose);

            this.textboxItemName = new TextBox("textboxItemName", 0, 10, 15, 300, 32);
            this.textboxItemName.Alpha = 255;
            this.textboxItemName.TapEvent += new OnTap(Glide.OpenKeyboard);
            this.AddChild(this.textboxItemName);

            this.buttonAddItem = new Button("buttonAddItem", 0, 75, 140, 150, 32);
            this.buttonAddItem.Text = "Add";
            this.buttonAddItem.TintColor = Colors.Black;
            this.buttonAddItem.TintAmount = 10;
            this.buttonAddItem.TapEvent += new OnTap(buttonAddItem_TapEvent);
            this.AddChild(this.buttonAddItem);
        }

        #endregion

        #region "Events"

        void buttonCountDown_TapEvent(object sender)
        {
            if (this.NumberOfItems > 1)
            {
                this.textboxNumberOfItems.Text = (this.NumberOfItems - 1).ToString();
                this.textboxNumberOfItems.Invalidate();
            }
        }

        void buttonCoutnUp_TapEvent(object sender)
        {
            this.textboxNumberOfItems.Text = (this.NumberOfItems + 1).ToString();
            this.textboxNumberOfItems.Invalidate();
        }

        private void buttonAddItem_TapEvent(object sender)
        {
            // Empty
        }

        private void buttonClose_TapEvent(object sender)
        {
            // Empty.
        }

        private void dropDownCategories_TapEvent(object sender)
        {
            Glide.OpenList(sender, this.shoppingTypes);
        }

        private void shoppingTypes_CloseEvent(object sender)
        {
            Glide.CloseList();
        }

        #endregion

    }
}
