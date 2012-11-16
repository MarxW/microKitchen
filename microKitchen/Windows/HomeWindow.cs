using System;
using Microsoft.SPOT;
using GHIElectronics.NETMF.Glide.Display;
using GHIElectronics.NETMF.Glide;
using GHIElectronics.NETMF.Glide.UI;

namespace microKitchen.Windows
{

    class HomeWindow : Window
    {
        private Button bNavigateToShopping;

        public Button ButtonNavigateToShopping
        {
            get { return this.bNavigateToShopping; }
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
            this.bNavigateToShopping = new Button("buttonNavigateToShopping", 0, 6, 190, 110, 32);
            this.bNavigateToShopping.Text = "Shopping";
            this.bNavigateToShopping.TintColor = Colors.Black;
            this.bNavigateToShopping.TintAmount = 10;
            this.bNavigateToShopping.TapEvent += new OnTap(OnButtonShopping_Taped); 
            this.AddChild(this.bNavigateToShopping);
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
