using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Handlers;
using RamuneLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static CraftData;

namespace MoreLockers.hlocker
{
   
    internal class HorizontalWallLockers 
    {
        public static Texture2D HorizontalWallLockersTexture = Utilities.GetTexture("submarine_locker_01");
        public static Texture2D HorizontalWallLockersspec = Utilities.GetTexture("submarine_locker_01_spec");
        public static Texture2D HorizontalWallLockersnorm = Utilities.GetTexture("submarine_locker_01_normal");
        public static PrefabInfo Info;
        public static TechType TechType;

        public static void Patch()

        {


            Info = Utilities.CreatePrefabInfo("horizontalwalllocker", "Horizontal Wall Locker", "Small, wall-mounted storage solution.", Utilities.GetSprite("locker"), 2, 3);
            var prefab = new CustomPrefab(Info);

            
        prefab.SetUnlock(TechType.Peeper);
            prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);
            prefab.SetRecipe(new RecipeData(new CraftData.Ingredient(TechType.Titanium, 1)));




            prefab.SetGameObject(new CloneTemplate(Info, TechType.SmallLocker)
            {
                ModifyPrefab = prefab1 =>
                {

                    var renderer = prefab1.FindChild("model").FindChild("submarine_locker_02").FindChild("submarine_locker_02_door").GetComponent<MeshRenderer>();
                    foreach (var m in renderer.materials)
                    {
                        m.mainTexture = HorizontalWallLockersTexture;
                        m.SetTexture("_SpecTex", HorizontalWallLockersTexture);
                        m.SetTexture("_Illum", HorizontalWallLockersTexture);
                        m.SetTexture("_BumpMap", HorizontalWallLockersnorm);
                        
                    }

                    prefab1.FindChild("model").transform.rotation = Quaternion.Euler(0, 0, 90);
                    prefab1.FindChild("Collider").transform.rotation = Quaternion.Euler(0, 0, 90);
                    prefab1.FindChild("Build Trigger").transform.rotation = Quaternion.Euler(0, 0, 90);
                    prefab1.FindChild("TriggerCull").transform.rotation = Quaternion.Euler(0, 0, 90);


                }
                
        });



            prefab.Register();

            TechType = Info.TechType;


        }
        
    }
}

