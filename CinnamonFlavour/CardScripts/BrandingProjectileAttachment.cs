using System.Linq;
using UnityEngine;

namespace CinnamonFlavour
{
	public class BrandingProjectileAttachment : MonoBehaviour
	{
		private readonly float _range = 6f;
		private Player _spawner;

		private void Start()
		{
			this._spawner = this.GetComponentInParent<SpawnedAttack>().spawner;
			this.BrandNearby();
		}

		private void BrandNearby()
		{
			var point = this.transform.position;

			// this.GetComponentInChildren<ParticleSystem>().Play();

			var targets = PlayerManager.instance.players
				.Where(p => p.teamID != this._spawner.teamID)
				.Where(p => !p.data.dead)
				.Where(p => !p.data.block.IsBlocking())
				.Where(p => PlayerManager.instance.CanSeePlayer(point, p).canSee)
				.Where(p => Vector2.Distance(point, p.transform.position) <= this._range)
				.ToList();

			foreach (var target in targets)
			{
				target.transform.GetComponent<BrandHandler>().Brand(this._spawner);
			}
		}
	}
}