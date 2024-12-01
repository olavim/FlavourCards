using CinnamonFlavour.Extensions;
using UnityEngine;

namespace CinnamonFlavour
{
    public class BrandObjectsToSpawn
    {
        public enum SpawnTrigger { Expire }

        public GameObject Effect { get; set; } = default;
        public SpawnTrigger Trigger { get; set; } = SpawnTrigger.Expire;
        public int Stacks { get; set; } = 1;
        public bool ScaleStacks { get; set; } = false;
        public float ScaleStackMultiplier { get; set; } = 0;
        public float ScaleFromBrandDamage { get; set; } = 0;
        public float ScaleFromDamageToBranded { get; set; } = 0;

        public static GameObject SpawnObject(BrandObjectsToSpawn objectToSpawn, Vector2 position, Player spawner, SpawnedAttack spawnedAttack)
        {
            var go = GameObject.Instantiate(objectToSpawn.Effect, position, Quaternion.identity);
            spawnedAttack.CopySpawnedAttackTo(go);

            if (objectToSpawn.ScaleStacks && objectToSpawn.ScaleStackMultiplier > 0)
            {
                go.transform.localScale *= 1 + objectToSpawn.ScaleStackMultiplier * (objectToSpawn.Stacks - 1);
            }

            if (objectToSpawn.ScaleFromBrandDamage > 0)
            {
                float brandDamage = spawner.data.stats.GetAdditionalData().BrandDamageMultiplier;
                go.transform.localScale *= brandDamage * objectToSpawn.ScaleFromBrandDamage;
            }

            if (objectToSpawn.ScaleFromDamageToBranded > 0)
            {
                float damageToBranded = spawner.data.weaponHandler.gun.GetAdditionalData().DamageToBranded;
                go.transform.localScale *= damageToBranded * objectToSpawn.ScaleFromDamageToBranded;
            }

            return go;
        }
    }
}
