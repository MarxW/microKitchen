using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;
using GHIElectronics.NETMF.Glide.Display;
using GHIElectronics.NETMF.Glide;

namespace microKitchen
{
    public partial class Program
    {
        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            Glide.FitToScreen = true;
            Glide.Keyboard = Helper.InitKeyboard();

            GlideTouch.Initialize();
            if (!GlideTouch.Calibrated)
            {
                CalibrationWindow calWindow = new CalibrationWindow(false, true);
                calWindow.CloseEvent += new OnClose(calWindow_CloseEvent);
                Glide.MainWindow = calWindow;
            }
            else
            {
                Glide.MainWindow = new Windows.HomeWindow();
            }

            try
            {
                sdCard.MountSDCard();
            }
            catch
            {
                Glide.MessageBoxManager.Show("There was a problem loading the SD Card.");
            }
            Debug.Print("Program Started");
        }

        void calWindow_CloseEvent(object sender)
        {
            Glide.MainWindow = new Windows.HomeWindow();
        }
    }
}
