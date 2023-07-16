using BepInEx;
using Nautilus.Crafting;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Utility;
using UnityEngine;
using Nautilus.Assets.PrefabTemplates;
using static CraftData;
using Nautilus.Handlers;
#if SUBNAUTICA
using Ingredient = CraftData.Ingredient;
#endif

namespace MoreDeco
{
    
    public class Snack3
    {
       public static TechType techType;
        public static void Patch()
        {
            BuildableSnack3.Register();
        }
    }

    public static class BuildableSnack3
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("Snacksss", " Snack", "A Snack .")
            // set the icon to that of the vanilla locker:
            .WithIcon(SpriteManager.Get(TechType.Snack3));

        public static void Register()
        {
            // create prefab:
            CustomPrefab prefab = new CustomPrefab(Info);

            // copy the model of a vanilla wreck piece (which looks like a taller locker):
            CloneTemplate lockerClone = new CloneTemplate(Info, "45a2c0fd-c56d-4946-8234-f48716a34718");

            // modify the cloned model:
            lockerClone.ModifyPrefab += obj =>
            {
                // allow it to be placced inside bases and submarines on the ground, and can be rotated:
                ConstructableFlags constructableFlags = ConstructableFlags.Inside | ConstructableFlags.Rotatable | ConstructableFlags.Ground | ConstructableFlags.Submarine| ConstructableFlags.Rotatable;
                // find the object that holds the model:
                GameObject model = obj.transform.Find("Snack_03").gameObject;
                GameObject.DestroyImmediate(model.AddComponent<Pickupable>());
                GameObject.DestroyImmediate(obj.AddComponent<Pickupable>());
                // this line is only necessary for the tall locker so that the door is also part of the model:
                ///obj.transform.Find("Starship_wall_monitor_01_01").parent = model.transform;
                // add all components necessary for it to be built:
                PrefabUtils.AddConstructable(obj, Info.TechType, constructableFlags,model);
                // allow it to be opened as a storage container:
                
                
            };

            // assign the created clone model to the prefab itself:
            prefab.SetGameObject(lockerClone);

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
