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
    
    public class Pa7
    {
       public static TechType techType;
        public static void Patch()
        {
            BuildablePa7.Register();
        }
    }

    public static class BuildablePa7
    {
        public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("Partifact7", " Precursor Artifact 7", "A Precursor rifle.")
            // set the icon to that of the vanilla locker:
            .WithIcon(Utilities.GetSprite("artifact7"));

        public static void Register()
        {
            // create prefab:
            CustomPrefab prefab = new CustomPrefab(Info);

            // copy the model of a vanilla wreck piece (which looks like a taller locker):
            CloneTemplate lockerClone = new CloneTemplate(Info, "65e8aad0-b391-46cf-a062-dca72ee277d1");

            // modify the cloned model:
            lockerClone.ModifyPrefab += obj =>
            {
                // allow it to be placced inside bases and submarines on the ground, and can be rotated:
                ConstructableFlags constructableFlags = ConstructableFlags.Inside | ConstructableFlags.Rotatable | ConstructableFlags.Ground | ConstructableFlags.Submarine| ConstructableFlags.Rotatable;
                // find the object that holds the model:
                GameObject model = obj.transform.Find("alien_relic_07").gameObject;
                obj.transform.localPosition = new Vector3(0f, 1.85f, 0f);
                obj.GetComponentInChildren<Animator>().enabled = false;
                GameObject.DestroyImmediate(obj.GetComponent<EntityTag>());
                GameObject.DestroyImmediate(obj.GetComponent<ImmuneToPropulsioncannon>());
                GameObject.DestroyImmediate(obj.GetComponent<CapsuleCollider>());
                var collider = obj.AddComponent<BoxCollider>();
                collider.size = new Vector3(0.5f, 0.6f, 0.5f);
                collider.center = new Vector3(0f, 0.3f, 0f);
                collider.isTrigger = true;
                obj.AddComponent<SkyApplier>();
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
