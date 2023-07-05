using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static CraftData;

namespace MoreLockers
{
    
    
        public  class longLocker
        {
            public static void Patch()
            {
                BuildableLongLocker.Register();
            }
        }

        public static class BuildableLongLocker
        {
            public static PrefabInfo Info { get; } = PrefabInfo.WithTechType("LongLocker", "Long Locker", "A long locker.")
                // set the icon to that of the vanilla locker:
                .WithIcon(SpriteManager.Get(TechType.Locker));

            public static void Register()
            {
                // create prefab:
                CustomPrefab prefab = new CustomPrefab(Info);

                // copy the model of a vanilla wreck piece (which looks like a taller locker):
                CloneTemplate longrClone = new CloneTemplate(Info, "367656d6-87d9-42a1-926c-3cf959ea1c85");

                // modify the cloned model:
                longrClone.ModifyPrefab += obj =>
                {
                    // allow it to be placced inside bases and submarines on the ground, and can be rotated:
                    ConstructableFlags constructableFlags = ConstructableFlags.Inside | ConstructableFlags.Rotatable | ConstructableFlags.Ground | ConstructableFlags.Submarine;
                    // find the object that holds the model:
                    GameObject model = obj.transform.Find("submarine_locker_03").gameObject;
                    // this line is only necessary for the tall locker so that the door is also part of the model:
                    obj.transform.Find("submarine_locker_03_door_01").parent = model.transform;
                    obj.transform.Find("submarine_locker_03_door_02").parent = model.transform;
                    
                    // add all components necessary for it to be built:
                    PrefabUtils.AddConstructable(obj, Info.TechType, constructableFlags, model);
                    // allow it to be opened as a storage container:
                    PrefabUtils.AddStorageContainer(obj , "StorageRoot", "LongLocker", 3, 8, true);
                };

                // assign the created clone model to the prefab itself:
                prefab.SetGameObject(longrClone);

                // assign it to the correct tab in the builder tool:
                prefab.SetPdaGroupCategory(TechGroup.InteriorModules, TechCategory.InteriorModule);

                // set recipe:
                prefab.SetRecipe(new RecipeData(new Ingredient(TechType.Titanium, 3)));

                // finally, register it into the game:
                prefab.Register();
            }
        }
    
}

