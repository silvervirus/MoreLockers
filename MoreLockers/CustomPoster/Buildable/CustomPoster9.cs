using BepInEx;
using Nautilus.Crafting;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Utility;
using UnityEngine;
using Nautilus.Assets.PrefabTemplates;
using static CraftData;
using RamuneLib;
#if SUBNAUTICA
using Ingredient = CraftData.Ingredient;
#endif

namespace CustomPoster
{
   
    public class CustomPoster9

    {
        public static Texture2D TestPhoto = Utilities.GetTexture("CustomPoster9");
        

        public static PrefabInfo Info;
        public static TechType TechType;

        public static void Patch()

        {


            Info = Utilities.CreatePrefabInfo("CustomPoster9", "CustomPoster9", "Wall Mounted Poster.", Utilities.GetSprite("CustomPoster9"), 2, 3);
            var prefab = new CustomPrefab(Info);


            prefab.SetUnlock(TechType.PosterKitty);
            prefab.SetEquipment(EquipmentType.Hand).WithQuickSlotType(QuickSlotType.Selectable);

            prefab.SetRecipe(new RecipeData(new CraftData.Ingredient(TechType.Titanium, 1)))
            .WithFabricatorType(CraftTree.Type.Workbench)
                .WithStepsToFabricatorTab("PT");



            prefab.SetGameObject(new CloneTemplate(Info, TechType.PosterKitty)
            {
                ModifyPrefab = prefab1 =>
                {

                    var material = prefab1.GetComponentInChildren<MeshRenderer>().materials[1]; ;
                   
                    
                        material.SetTexture("_MainTex", TestPhoto);
                        material.SetTexture("_SpecTex", TestPhoto);






                }

            });



            prefab.Register();

            TechType = Info.TechType;


        }

    }
}

