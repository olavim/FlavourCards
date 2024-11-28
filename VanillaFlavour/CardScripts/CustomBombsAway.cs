using UnityEngine;
using VanillaFlavour.Extensions;

namespace VanillaFlavour
{
    // Custom Bombs Away attachment to disable self damage
	public sealed class CustomBombsAway : MonoBehaviour {
        private Gun _bombSpawnerGun;

        private void Start() {
            var bombsAway = GameObject.Instantiate((GameObject) VanillaFlavour.RoundsResources["A_BombsAway"], this.transform.position, Quaternion.identity, this.transform);
            this._bombSpawnerGun = bombsAway.transform.Find("Rotator/Gun").GetComponent<Gun>();
            this._bombSpawnerGun.objectsToSpawn[0].GetAdditionalData().SpawnedObjectAction += this.HandleBombObjectSpawns;
        }

        private void OnDestroy() {
            this._bombSpawnerGun.objectsToSpawn[0].GetAdditionalData().SpawnedObjectAction -= this.HandleBombObjectSpawns;
        }

        private void HandleBombObjectSpawns(GameObject[] spawnedObjects) {
            foreach (var spawnedObject in spawnedObjects) {
                spawnedObject.GetComponent<SpawnObjects>().SpawnedAction += this.HandleBombExplosionSpawn;
            }
        }

        private void HandleBombExplosionSpawn(GameObject spawnedObject) {
            // It says "ignoreTeam" but it only applies to self
            spawnedObject.GetComponent<Explosion>().ignoreTeam = true;
        }
    }
}