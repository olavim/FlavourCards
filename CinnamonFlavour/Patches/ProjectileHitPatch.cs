using CinnamonFlavour.Extensions;
using HarmonyLib;
using Photon.Pun;
using System.Linq;
using UnboundLib;
using UnityEngine;

namespace CinnamonFlavour.Patches
{
    [HarmonyPatch(typeof(ProjectileHit), "Start")]
    class ProjectileHitPatch_Start
    {
        private static void Prefix(ProjectileHit __instance)
        {
            if (__instance.GetAdditionalData().WillBrand)
            {
                __instance.GetComponent<SpawnedAttack>().SetColor(new Color32(100, 0, 0, 255));
                __instance.gameObject.GetOrAddComponent<BrandingProjectileHit>();

                var objectsToSpawn = __instance.objectsToSpawn.ToList();
                objectsToSpawn.Add(
                    new()
                    {
                        effect = (GameObject) CinnamonFlavour.CustomResources["A_BrandingProjectile"],
                        direction = ObjectsToSpawn.Direction.identity,
                        normalOffset = 0.05f,
                        stickToAllTargets = true,
                        scaleStacks = false
                    }
                );
                __instance.objectsToSpawn = objectsToSpawn.ToArray();
            }
        }
    }

    [HarmonyPatch(typeof(ProjectileHit), "RPCA_DoHit")]
    class ProjectileHitPatch_RPCADoHit
    {
        private static void Prefix(ProjectileHit __instance, int viewID, bool wasBlocked)
        {
            if (viewID == -1)
            {
                return;
            }

            PhotonView photonView = PhotonNetwork.GetPhotonView(viewID);
            var brandHandler = photonView.transform.GetComponent<BrandHandler>();

            if (brandHandler)
            {
                if (brandHandler.IsBrandedBy(__instance.ownPlayer))
                {
                    var gun = __instance.ownWeapon.GetComponent<Gun>();
                    __instance.damage *= gun.GetAdditionalData().DamageToBranded;
                }
            }
        }
    }
}
