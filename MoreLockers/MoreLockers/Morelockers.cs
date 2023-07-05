using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLockers.hlocker;
using BepInEx;
namespace MoreLockers.main
{
    [BepInPlugin(Main.PLUGIN_GUID, Main.PLUGIN_NAME, Main.PLUGIN_VERSION)]
    class Main : BaseUnityPlugin
    {
        public const String PLUGIN_GUID = "SN.Morelockers";
        public const String PLUGIN_NAME = "MoreLockers.SN";
        public const String PLUGIN_VERSION = "1.0.1";
        public void Awake()
        {
            longLocker.Patch();
            TallLocker.Patch();
            SupplyCrate.Patch();
            SmallCrate.Patch();
            OpenLocker.Patch();
 
            HorizontalWallLockers.Patch();

        }
    }
}
