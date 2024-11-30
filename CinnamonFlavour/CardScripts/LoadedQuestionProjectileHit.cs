using System.Linq;
using UnityEngine;

namespace CinnamonFlavour
{
	public class LoadedQuestionProjectileHit : RayHitEffect
	{
		[SerializeField] private GameObject _hitEffect = default;

		private readonly float _range = 6f;
		private bool _done;

		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (this._done || !hit.transform)
			{
				return HasToReturn.canContinue;
			}

			GameObject.Instantiate(this._hitEffect, hit.point, Quaternion.identity);
			var point = hit.point + (hit.normal * 0.1f);

			var brander = this.GetComponentInParent<SpawnedAttack>().spawner;
			var targets = PlayerManager.instance.players
				.Where(p => p.teamID != brander.teamID)
				.Where(p => !p.data.dead)
				.Where(p => Vector2.Distance(hit.point, p.transform.position) <= this._range)
				.Where(p => !p.data.block.IsBlocking())
				.Where(p => PlayerManager.instance.CanSeePlayer(point, p).canSee)
				.ToList();

			foreach (var target in targets)
			{
				target.transform.GetComponent<BrandHandler>().Brand(brander);
			}

			this._done = true;
			return HasToReturn.canContinue;
		}
	}
}