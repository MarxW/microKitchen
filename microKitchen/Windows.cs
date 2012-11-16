using System;
using Microsoft.SPOT;
using GHIElectronics.NETMF.Glide.Display;
using GHIElectronics.NETMF.Glide;
using GHIElectronics.NETMF.Glide.UI;

namespace microKitchen
{
    class WindowOne : Window
    {
        public WindowOne()
            : base("wondowOne", 320, 240)
        {
            this.BackColor = Microsoft.SPOT.Presentation.Media.Color.White;
            TextBlock tb = new TextBlock("textBlock", 0, 126, 190, 110, 32);
            tb.Text = "You made it";
            this.AddChild(tb);
        }

    }

    class HomeWindow : Window
    {
        private Button bNavigate;

        public Button ButtonNavigateToPageOne
        {
            get { return this.bNavigate; }
        }

        #region "Initialise"

        public HomeWindow(string name, int width, int height)
            : base(name, width, height)
        {
            this.BackColor = Microsoft.SPOT.Presentation.Media.Color.White;
            this.SetUpButtons();
        }

        public HomeWindow()
            : base("homeWindow", 320, 240)
        {
            this.BackColor = Microsoft.SPOT.Presentation.Media.Color.White;
            this.SetUpButtons();
        }

        private void SetUpButtons()
        {
            this.bNavigate = new Button("buttonNavigate", 0, 126, 190, 110, 32);
            this.bNavigate.Text = "Go To Next";
            this.AddChild(this.bNavigate);
            this.TapEvent += new OnTap(bNavigate_TapEvent);
        }

        #endregion

        #region "Events"

        private void bNavigate_TapEvent(object sender)
        {
            Tween.SlideWindow(this, new WindowOne(), Direction.Up);
            this.Dispose();
        }

        #endregion

    }
}
