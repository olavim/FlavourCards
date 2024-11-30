using CinnamonFlavour.Extensions;
using HarmonyLib;
using UnityEngine;

namespace CinnamonFlavour.Patches
{
    [HarmonyPatch(typeof(Gun), "ApplyProjectileStats")]
    class GunPatch_ApplyProjectileStats
    {
        private static void Prefix(Gun __instance, GameObject obj, float randomSeed)
        {
            obj.GetComponent<ProjectileHit>().GetAdditionalData().WillBrand = randomSeed < __instance.GetAdditionalData().BrandChance;
        }
    }
}