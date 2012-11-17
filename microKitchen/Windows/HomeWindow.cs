using System;
using Microsoft.SPOT;
using GHIElectronics.NETMF.Glide.Display;
using GHIElectronics.NETMF.Glide;
using GHIElectronics.NETMF.Glide.UI;

namespace microKitchen.Windows
{

    class HomeWindow : Window
    {
        private Button buttonNavigateToShopping;

        public Button ButtonNavigateToShopping
        {
            get { return this.buttonNavigateToShopping; }
        }

        #region "Initialise"

        public HomeWindow(string name, int width, int height)
            : base(name, width, height)
        {
            this.BackColor = Colors.White;
            this.InitWindow();
        }

        public HomeWindow()
            : base("homeWindow", 320, 240)
        {
            this.BackColor = Colors.White;
            this.InitWindow();
        }

        private void InitWindow()
        {
            this.buttonNavigateToShopping = new Button("buttonNavigateToShopping", 0, 6, 190, 110, 32);
            this.buttonNavigateToShopping.Text = "Shopping";
            this.buttonNavigateToShopping.TintColor = Colors.Black;
            this.buttonNavigateToShopping.TintAmount = 10;
            this.buttonNavigateToShopping.TapEvent += new OnTap(OnButtonShopping_Taped); 
            this.AddChild(this.buttonNavigateToShopping);
        }

        #endregion

        #region "Events"

        private void OnButtonShopping_Taped(object sender)
        {
            Tween.SlideWindow(this, new ShoppingWindow(), Direction.Up);
            this.Dispose();
        }

        #endregion

    }
}
