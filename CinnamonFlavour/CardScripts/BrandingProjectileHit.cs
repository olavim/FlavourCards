using System.Linq;
using UnityEngine;

namespace CinnamonFlavour
{
	public class BrandingProjectileHit : RayHitEffect
	{
		private readonly float _range = 6f;
		private Player _spawner;
		private bool _done;

		private void Start()
		{
			this._spawner = this.GetComponent<SpawnedAttack>().spawner;
		}

		private void OnDestroy()
		{
			// Projectile was destroyed before hitting anything, e.g. buckshot
			if (!this._done)
			{
				this.BrandNearby(this.transform.position);
			}
		}

		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (this._done || !hit.transform)
			{
				return HasToReturn.canContinue;
			}

			this.BrandNearby(hit.point + (hit.normal * 0.1f));

			this._done = true;
			return HasToReturn.canContinue;
		}

		private void BrandNearby(Vector2 point)
		{
			GameObject.Instantiate((GameObject) CinnamonFlavour.CustomResources["VE_BrandingShot"], point, Quaternion.identity);

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