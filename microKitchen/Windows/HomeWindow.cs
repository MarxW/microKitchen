using System;
using Microsoft.SPOT;
using GHIElectronics.NETMF.Glide.Display;
using GHIElectronics.NETMF.Glide;
using GHIElectronics.NETMF.Glide.UI;

namespace microKitchen.Windows
{

    class HomeWindow : Window
    {

        public Button ButtonNavigateToShopping { get; internal set; }

        #region "Initialise"

        public HomeWindow(string name, int width, int height)
            : base(name, width, height)
        {
            this.InitWindow();
        }

        public HomeWindow()
            : base("homeWindow", 320, 240)
        {
            this.InitWindow();
        }

        private void InitWindow()
        {
            this.BackColor = Colors.White;
            this.BackImage = Resources.GetBitmap(Resources.BitmapResources.screen_background);
            this.ButtonNavigateToShopping = new Button("buttonNavigateToShopping", 0, 6, 190, 110, 32);
            this.ButtonNavigateToShopping.Text = "Shopping";
            this.ButtonNavigateToShopping.TintColor = Colors.Black;
            this.ButtonNavigateToShopping.TintAmount = 10;
            this.ButtonNavigateToShopping.TapEvent += new OnTap(OnButtonShopping_Taped);
            this.AddChild(this.ButtonNavigateToShopping);
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
