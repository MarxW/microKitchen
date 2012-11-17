﻿
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the Gadgeteer Designer.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Gadgeteer;
using GTM = Gadgeteer.Modules;

namespace microKitchen
{
    public partial class Program : Gadgeteer.Program
    {
        // GTM.Module definitions
        Gadgeteer.Modules.GHIElectronics.Display_T35 display_T35;
        Gadgeteer.Modules.GHIElectronics.UsbClientDP usbClientDP;
        Gadgeteer.Modules.GHIElectronics.SDCard sdCard;

        public static void Main()
        {
            //Important to initialize the Mainboard first
            Mainboard = new GHIElectronics.Gadgeteer.FEZHydra();			

            Program program = new Program();
            program.InitializeModules();
            program.ProgramStarted();
            program.Run(); // Starts Dispatcher
        }

        private void InitializeModules()
        {   
            // Initialize GTM.Modules and event handlers here.		
            usbClientDP = new GTM.GHIElectronics.UsbClientDP(2);
		
            sdCard = new GTM.GHIElectronics.SDCard(8);
		
            display_T35 = new GTM.GHIElectronics.Display_T35(10, 11, 12, 13);

        }
    }
}
