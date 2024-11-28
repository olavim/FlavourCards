using VanillaFlavour.Extensions;
using HarmonyLib;
using UnityEngine;
using System;

namespace VanillaFlavour.Patches
{
    [HarmonyPatch(typeof(ObjectsToSpawn), "SpawnObject", new Type[] {
        typeof(Transform), 
        typeof(HitInfo), 
        typeof(ObjectsToSpawn), 
        typeof(HealthHandler), 
        typeof(PlayerSkin), 
        typeof(float), 
        typeof(SpawnedAttack), 
        typeof(bool)
    })]
    class ObjectsToSpawnPatch_SpawnObject
    {
        private static void Postfix(ObjectsToSpawn objectToSpawn, ref GameObject[] __result)
        {
            objectToSpawn.GetAdditionalData().SpawnedObjectAction?.Invoke(__result);
        }
    }
}
