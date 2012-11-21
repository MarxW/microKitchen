using System;
using Microsoft.SPOT;
using GHIElectronics.NETMF.Glide.Display;
using GHIElectronics.NETMF.Glide;

namespace microKitchen.Windows
{
    class SplashScreen : Window
    {

        public SplashScreen()
            : base("splashScreen", 320, 240)
        {
            this.BackColor = Colors.White;
            this.BackImage = Resources.GetBitmap(Resources.BitmapResources.screen_background);
        }
    }
}
