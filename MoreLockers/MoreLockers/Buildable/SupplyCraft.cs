using BepInEx;
using Nautilus.Crafting;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Utility;
using Nautilus.Extensions;
using UnityEngine;
using Nautilus.Assets.PrefabTemplates;
using static CraftData;
#if SUBNAUTICA
using Ingredient = CraftData.Ingredient;
#endif

namespace MoreLockers
{
    
    public class SupplyCrate
    {
       public static TechType techType;
        public static void Patch()
        {
            BuildableSupplyCrate.Register();
        }
    }

    public static class BuildableSupplyCrate
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("SupplyCrate", "SupplyCrate", "a crate.")
            // set the icon to that of the vanilla locker:
            .WithIcon(SpriteManager.Get(TechType.Locker));

        public static void Register()
        {
            // create prefab:
            CustomPrefab prefab = new CustomPrefab(Info);

            // copy the model of a vanilla wreck piece (which looks like a taller locker):
            CloneTemplate scrateClone = new CloneTemplate(Info, "e837f37f-ed22-499d-b6ce-51f01b9602d8");

            // modify the cloned model:
            scrateClone.ModifyPrefab += obj =>
            {
                // allow it to be placced inside bases and submarines on the ground, and can be rotated:
                ConstructableFlags constructableFlags = ConstructableFlags.Inside | ConstructableFlags.Rotatable | ConstructableFlags.Ground | ConstructableFlags.Submarine;
                // find the object that holds the model:
                GameObject model = obj.transform.Find("Crate_treasure_chest").gameObject;
                // this line is only necessary for the tall locker so that the door is also part of the model:
                
                // add all components necessary for it to be built:
                PrefabUtils.AddConstructable(obj, Info.TechType, constructableFlags,model);
                // allow it to be opened as a storage container:
                PrefabUtils.AddStorageContainer(obj, "StorageRoot", "SupplyCrate", 3, 8, true);


            };

            // assign the created clone model to the prefab itself:
            prefab.SetGameObject(scrateClone);

            // assign it to the correct tab in the builder tool:
            prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);

            // set recipe:
            prefab.SetRecipe(new RecipeData(new Ingredient(TechType.Titanium, 3)));

            // finally, register it into the game:
            prefab.Register();

            TechType techType = Info.TechType;
            
        }
    }
}
