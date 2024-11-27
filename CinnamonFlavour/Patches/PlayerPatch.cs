using HarmonyLib;

namespace CinnamonFlavour.Patches
{
    [HarmonyPatch(typeof(Player), "FullReset")]
    class PlayerPatch_FullReset
    {
        private static void Prefix(Player __instance)
        {
            __instance.transform.GetComponent<BrandHandler>()?.Reset();
        }
    }

    [HarmonyPatch(typeof(Player), "Awake")]
    class PlayerPatch_Awake
    {
        private static void Prefix(Player __instance)
        {
            __instance.gameObject.AddComponent<BrandHandler>();
        }
    }
}