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

namespace MoreDeco
{
    
    public class Pa9
    {
       public static TechType techType;
        public static void Patch()
        {
            BuildablePa9.Register();
        }
    }

    public static class BuildablePa9
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("Partifact10", " Precursor Artifact 10", "A Precursor Crystal.")
            // set the icon to that of the vanilla locker:
            .WithIcon(Utilities.GetSprite("Artifact10"));

        public static void Register()
        {
            // create prefab:
            CustomPrefab prefab = new CustomPrefab(Info);

            // copy the model of a vanilla wreck piece (which looks like a taller locker):
            CloneTemplate lockerClone = new CloneTemplate(Info, "f111c882-4ef6-4ad0-aeba-d123568ad3fc");

            // modify the cloned model:
            lockerClone.ModifyPrefab += obj =>
            {
                // allow it to be placced inside bases and submarines on the ground, and can be rotated:
                ConstructableFlags constructableFlags = ConstructableFlags.Inside | ConstructableFlags.Rotatable | ConstructableFlags.Ground | ConstructableFlags.Submarine| ConstructableFlags.Rotatable;
                // find the object that holds the model:
                GameObject model = obj.transform.Find("alien_relic_10").gameObject;
               
                obj.GetComponentInChildren<Animator>().enabled = false; 
                var collider = obj.GetComponent<CapsuleCollider>();
                collider.radius = 0.15f;
                collider.height = 0.15f;
                collider.contactOffset = 0.15f;
                collider.isTrigger = true;
                foreach (CapsuleCollider c in obj.GetComponentsInChildren<CapsuleCollider>())
                {
                    c.radius *= 0.15f;
                    c.height *= 0.15f;
                    c.isTrigger = true;
                }


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
