using System.Runtime.CompilerServices;
using HarmonyLib;
using UnityEngine;

namespace CinnamonFlavour.Extensions
{
    public class GunAdditionalData
    {
        private float _damageToBranded = 1f;

        public int AmmoOnHitBranded { get; set; } = 0;
        public float DamageToBranded {
            get => Mathf.Max(0.25f, this._damageToBranded);
            set => this._damageToBranded = value;
        }
    }

    public static class GunExtension
    {
        public static readonly ConditionalWeakTable<Gun, GunAdditionalData> data = new();

        public static GunAdditionalData GetAdditionalData(this Gun gun)
        {
            return data.GetOrCreateValue(gun);
        }
    }
    
    [HarmonyPatch(typeof(Gun), "ResetStats")]
    class GunPatch_ResetStats
    {
        private static void Prefix(Gun __instance)
        {
            __instance.GetAdditionalData().AmmoOnHitBranded = 0;
            __instance.GetAdditionalData().DamageToBranded = 1f;
        }
    }
}