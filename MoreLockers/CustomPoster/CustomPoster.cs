using BepInEx;
using System;
using System.Collections.Generic;
using System.Linq;
using Nautilus.Handlers;
using System.Text;
using RamuneLib;
using System.Threading.Tasks;

namespace CustomPoster
{
    [BepInPlugin(Main.PLUGIN_GUID, Main.PLUGIN_NAME, Main.PLUGIN_VERSION)]
    class Main : BaseUnityPlugin
    {
        public const String PLUGIN_GUID = "SN.CustomPoster";
        public const String PLUGIN_NAME = "CustomPoster.SN";
        public const String PLUGIN_VERSION = "1.0.0";
        public void Awake()
        {
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "LithiumIonBattery");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "HeatBlade");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "PlasteelTank");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "HighCapacityTank");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "UltraGlideFins");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "SwimChargeFins");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "RepulsionCannon");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "CyclopsHullModule2");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "CyclopsHullModule3");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "SeamothHullModule2");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "SeamothHullModule3");
            CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, "ExoHullModule2");
            CraftTreeHandler.AddTabNode(CraftTree.Type.Workbench, "PT", "Posters", Utilities.GetSprite("CustomPoster1"));

           
            CustomPoster1.Patch();
            CustomPoster2.Patch();
            CustomPoster3.Patch();
            CustomPoster4.Patch();
            CustomPoster5.Patch();
            CustomPoster6.Patch();
            CustomPoster7.Patch();
            CustomPoster8.Patch();
            CustomPoster9.Patch();
            CustomPoster10.Patch();
            CustomPoster11.Patch();
            CustomPoster12.Patch();
            CustomPoster13.Patch();
            CustomPoster14.Patch();
            CustomPoster15.Patch();
            CustomPoster16.Patch();
            CustomPoster17.Patch();
            CustomPoster18.Patch();
            CustomPoster19.Patch();
            CustomPoster20.Patch();

        }
    }
}
