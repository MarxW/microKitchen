using System;
using Microsoft.SPOT;
using GHIElectronics.NETMF.Glide.UI;
using microKitchen.Shopping;
using GHIElectronics.NETMF.Glide;
using GHIElectronics.NETMF.Glide.Display;
using Gadgeteer;

namespace microKitchen.Windows
{
    class AddShoppingItemDialog : Window
    {
        public Dropdown dropDownCategories;
        private List shoppingTypes;

        #region "Initialise"

        public AddShoppingItemDialog()
            : base("addShoppingItemModal", 320, 240)
        {
            this.InitialiseModal();
        }

        public AddShoppingItemDialog(string name, int width, int height)
            : base(name, width, height)
        {
            this.InitialiseModal();
        }

        private void InitialiseModal()
        {
            this.dropDownCategories = new Dropdown("dropdownCategory", 0, 10, 57, 300, 32);
            this.dropDownCategories.Options.Add(new object[] { ShoppingItemTypes.Dairy.GetStringValue(), ShoppingItemTypes.Dairy.GetStringValue() });
            this.dropDownCategories.Options.Add(new object[] { ShoppingItemTypes.Frozen.GetStringValue(), ShoppingItemTypes.Frozen.GetStringValue() });
            this.dropDownCategories.Options.Add(new object[] { ShoppingItemTypes.Meat_SeaFood.GetStringValue(), ShoppingItemTypes.Meat_SeaFood.GetStringValue() });
            this.dropDownCategories.Options.Add(new object[] { ShoppingItemTypes.UnCategorised.GetStringValue(), ShoppingItemTypes.UnCategorised.GetStringValue() });
            this.dropDownCategories.TapEvent += new OnTap(dropDownCategories_TapEvent);
            this.AddChild(this.dropDownCategories);

            this.shoppingTypes = new List(dropDownCategories.Options, 200);
            this.shoppingTypes.CloseEvent += new OnClose(shoppingTypes_CloseEvent);
            this.AddChild(this.shoppingTypes);

        }

        #endregion

        #region "Events"

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
