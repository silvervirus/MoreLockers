using BepInEx;
using Nautilus.Crafting;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Utility;
using Nautilus.Handlers;
using UnityEngine;
using Nautilus.Assets.PrefabTemplates;
using static CraftData;
#if SUBNAUTICA
using Ingredient = CraftData.Ingredient;
#endif

namespace MoreDeco
{
    
    public class Snack1
    {
       public static TechType techType;
        public static void Patch()
        {
            Buildablesnack1.Register();
        }
    }

    public static class Buildablesnack1
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("Snacks", " A Snack", "A Snack .")
            // set the icon to that of the vanilla locker:
            .WithIcon(SpriteManager.Get(TechType.Snack1));

        public static void Register()
        {
            // create prefab:
            CustomPrefab prefab = new CustomPrefab(Info);

            // copy the model of a vanilla wreck piece (which looks like a taller locker):
            CloneTemplate lockerClone = new CloneTemplate(Info, "890d44e1-e336-466b-89c8-cb7ea5ccbe83");

            // modify the cloned model:
            lockerClone.ModifyPrefab += obj =>
            {
                // allow it to be placced inside bases and submarines on the ground, and can be rotated:


                ConstructableFlags constructableFlags = ConstructableFlags.Inside | ConstructableFlags.Rotatable | ConstructableFlags.Ground | ConstructableFlags.Submarine | ConstructableFlags.Rotatable;
                GameObject model = obj.transform.Find("Snack_01").gameObject;
               GameObject.DestroyImmediate(model.AddComponent<Pickupable>());
                GameObject.DestroyImmediate(obj.AddComponent<Pickupable>());

                // this line is only necessary for the tall locker so that the door is also part of the model:
                ///obj.transform.Find("Starship_wall_monitor_01_01").parent = model.transform;
                // add all components necessary for it to be built:

                // allow it to be opened as a storage container:
                PrefabUtils.AddConstructable(obj, Info.TechType, constructableFlags, model);

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
