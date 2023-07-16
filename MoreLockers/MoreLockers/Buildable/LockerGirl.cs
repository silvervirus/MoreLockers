using BepInEx;
using Nautilus.Crafting;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Utility;
using UnityEngine;
using Nautilus.Assets.PrefabTemplates;
using RamuneLib;
using static CraftData;
#if SUBNAUTICA
using Ingredient = CraftData.Ingredient;
#endif

namespace MoreLockers
{
    
    public class girlLocker
    {
       public static TechType techType;
        public static void Patch()
        {
            BuildablegirlLocker.Register();
        }
    }

    public static class BuildablegirlLocker
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("GirlLocker", "Girl Locker", "A locker with a girl on it.")
            // set the icon to that of the vanilla locker:
            .WithIcon(Utilities.GetSprite("GirlLocker"));

        public static void Register()
        {
            // create prefab:
            CustomPrefab prefab = new CustomPrefab(Info);

            // copy the model of a vanilla wreck piece (which looks like a taller locker):
            CloneTemplate lockerClone = new CloneTemplate(Info, "078b41f8-968e-4ca3-8a7e-4e3d7d98422c");

            // modify the cloned model:
            lockerClone.ModifyPrefab += obj =>
            {
                // allow it to be placced inside bases and submarines on the ground, and can be rotated:
                ConstructableFlags constructableFlags = ConstructableFlags.Inside | ConstructableFlags.Rotatable | ConstructableFlags.Ground | ConstructableFlags.Submarine;
                // find the object that holds the model:
                GameObject model = obj.transform.Find("submarine_locker_05").gameObject;
                // this line is only necessary for the tall locker so that the door is also part of the model:
                
                
                // add all components necessary for it to be built:
                PrefabUtils.AddConstructable(obj, Info.TechType, constructableFlags,model);
                // allow it to be opened as a storage container:
                PrefabUtils.AddStorageContainer(obj, "StorageRoot", "GirlLocker", 3, 8, true);
                
            };

            // assign the created clone model to the prefab itself:
            prefab.SetGameObject(lockerClone);

            // assign it to the correct tab in the builder tool:
            prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);

            // set recipe:
            prefab.SetRecipe(new RecipeData(new Ingredient(TechType.Titanium, 3), new Ingredient(TechType.Glass, 1)));

            // finally, register it into the game:
            prefab.Register();

            TechType techType = Info.TechType;
            
        }
    }
}
