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

namespace MoreDeco
{
    
    public class Pdroid
    {
       public static TechType techType;
        public static void Patch()
        {
            BuildablePdroid.Register();
        }
    }

    public static class BuildablePdroid
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("Partifact11", " Precursor Artifact 11", "A Precursor lab droid.")
            // set the icon to that of the vanilla locker:
            .WithIcon(Utilities.GetSprite("artifact11"));

        public static void Register()
        {
            // create prefab:
            CustomPrefab prefab = new CustomPrefab(Info);

            // copy the model of a vanilla wreck piece (which looks like a taller locker):
            CloneTemplate lockerClone = new CloneTemplate(Info, "e6e7977e-3639-43b9-978f-9d0b40f17800");

            // modify the cloned model:
            lockerClone.ModifyPrefab += obj =>
            {
                // allow it to be placced inside bases and submarines on the ground, and can be rotated:
                ConstructableFlags constructableFlags = ConstructableFlags.Inside | ConstructableFlags.Rotatable | ConstructableFlags.Ground | ConstructableFlags.Submarine| ConstructableFlags.Rotatable;
                // find the object that holds the model:
                GameObject model = obj.transform.Find("alien_relic_11").gameObject;
                obj.GetComponentInChildren<Animator>().enabled = false;
               
                obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y + 0.1f, obj.transform.localPosition.z);
                // Scale
                foreach (Transform tr in obj.transform)
                    tr.transform.localScale *= 0.5f;
                var collider = obj.GetComponent<CapsuleCollider>();
                collider.radius = 0.5f;
                collider.height = 0.1f;
                collider.contactOffset = 0.1f;
                collider.isTrigger = true;
                foreach (BoxCollider c in obj.GetComponents<BoxCollider>())
                {
                    c.size *= 0.5f;
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
