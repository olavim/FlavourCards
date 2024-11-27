using CinnamonFlavour.Extensions;
using HarmonyLib;
using Photon.Pun;

namespace CinnamonFlavour.Patches
{
    [HarmonyPatch(typeof(ProjectileHit), "RPCA_DoHit")]
    class ProjectileHitPatch_RPCADoHit
    {
        private static void Prefix(ProjectileHit __instance, int viewID)
        {
            if (viewID == -1)
            {
                return;
            }

            PhotonView photonView = PhotonNetwork.GetPhotonView(viewID);
            var brandHandler = photonView.transform.GetComponent<BrandHandler>();

            if (!brandHandler || !brandHandler.IsBrandedBy(__instance.ownPlayer)) {
                return;
            }
            
            var gun = __instance.ownWeapon.GetComponent<Gun>();
            __instance.damage *= gun.GetAdditionalData().DamageBranded;
        }
    }
}