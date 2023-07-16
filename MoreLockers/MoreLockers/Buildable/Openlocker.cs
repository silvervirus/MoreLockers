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

namespace MoreLockers
{
    
    public class OpenLocker
    {
       public static TechType techType;
        public static void Patch()
        {
            BuildableopenLocker.Register();
        }
    }

    public static class BuildableopenLocker
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("OpenLocker", "Open Locker", "A locker without a door.")
            // set the icon to that of the vanilla locker:
            .WithIcon(Utilities.GetSprite("OpenLocker"));

        public static void Register()
        {
            // create prefab:
            CustomPrefab prefab = new CustomPrefab(Info);

            // copy the model of a vanilla wreck piece (which looks like a taller locker):
            CloneTemplate lockerClone = new CloneTemplate(Info, "bca9b19c-616d-4948-8742-9bb6f4296dc3");

            // modify the cloned model:
            lockerClone.ModifyPrefab += obj =>
            {
                // allow it to be placced inside bases and submarines on the ground, and can be rotated:
                ConstructableFlags constructableFlags = ConstructableFlags.Inside | ConstructableFlags.Rotatable | ConstructableFlags.Ground | ConstructableFlags.Submarine;
                // find the object that holds the model:
                GameObject model = obj.transform.Find("submarine_locker_04").gameObject;
                // this line is only necessary for the tall locker so that the door is also part of the model:
                
                // add all components necessary for it to be built:
                PrefabUtils.AddConstructable(obj, Info.TechType, constructableFlags,model);
                // allow it to be opened as a storage container:
                PrefabUtils.AddStorageContainer(obj, "StorageRoot", "TallLocker", 3, 8, true);
                
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
