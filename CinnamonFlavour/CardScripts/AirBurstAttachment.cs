using System.Linq;
using UnityEngine;

namespace CinnamonFlavour
{
	public class AirBurstAttachment : MonoBehaviour
	{
		[SerializeField] private GameObject _objectToSpawn = default;
		private ProjectileHit _projectileHit;
		private SpawnedAttack _attack;
		private Player _spawner;
		private Player[] _opponents;

		private void Start()
		{
			this._projectileHit = this.GetComponentInParent<ProjectileHit>();
			this._attack = this._projectileHit.GetComponent<SpawnedAttack>();
			this._spawner = this._attack.spawner;
			this._opponents = PlayerManager.instance.players.Where(p => p.teamID != this._spawner.teamID).ToArray();
		}

		private void Update()
		{
			if (this._projectileHit == null)
			{
				return;
			}

			var brandHandler = this.transform.GetComponentInParent<BrandHandler>();
			var nearbyBrandedOpponents = this._opponents
				.Where(p => !p.data.dead)
				.Where(p => p.GetComponent<BrandHandler>().IsBrandedBy(this._spawner))
				.Where(p => Vector2.Distance(this.transform.position, p.transform.position) <= 3f)
				.ToArray();

			if (nearbyBrandedOpponents.Length > 0)
			{
				var go = GameObject.Instantiate(this._objectToSpawn, this.transform.position, Quaternion.identity);
				this._attack.CopySpawnedAttackTo(go);

				this._projectileHit.Hit(
					new HitInfo
					{
						transform = nearbyBrandedOpponents[0].transform,
						point = this.transform.position,
						normal = (nearbyBrandedOpponents[0].transform.position - this.transform.position).normalized
					}
				);
			}
		}
	}
}
