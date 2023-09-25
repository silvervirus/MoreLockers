using BepInEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreDeco
{
    [BepInPlugin(Main.PLUGIN_GUID, Main.PLUGIN_NAME, Main.PLUGIN_VERSION)]
    class Main : BaseUnityPlugin
    {
        public const String PLUGIN_GUID = "SN.MoreDeco";
        public const String PLUGIN_NAME = "MoreDeco.SN";
        public const String PLUGIN_VERSION = "1.0.0";
        public void Awake()
        {
            WallMonitor.Patch();
            WallMonitor3.Patch();
            Forklift.Patch();
            WorkDesk.Patch();
            DeskScreen.Patch();
            FoodTray.Patch();
            deskclock.Patch();
            Foodbowl.Patch();
            Bottle.Patch();
            Bottle2.Patch();
            Bottle3.Patch();
            Bottle4.Patch();
            BottleGroup.Patch();
            Cup.Patch();
            Cup2.Patch();
            CupGroup.Patch();
            Menu.Patch();
            Pen.Patch();
            PenHolder.Patch();
            LabClosed1.Patch();
            LabClosed2.Patch();
            Crateopen.Patch();
            Ptable.Patch();
            Pdroid.Patch();
            Pa7.Patch();
            Pa8.Patch();
            Pa9.Patch();
            Snack1.Patch();
            Snack2.Patch();
            Snack3.Patch();
            paper3.Patch();
            
        }
    }
}
