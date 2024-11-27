namespace CinnamonFlavour
{
	public class BrandProjectileHit : RayHitEffect
	{
		private bool _done;

		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (this._done || !hit.transform)
			{
				return HasToReturn.canContinue;
			}

			var brandHandler = hit.transform.GetComponent<BrandHandler>();
			if (brandHandler) {
				var brander = this.GetComponentInParent<SpawnedAttack>().spawner;
				var target = hit.transform.GetComponentInParent<Player>();
				if (target && brander.teamID != target.teamID) {
					brandHandler.Brand(brander);
				}
			}

			this._done = true;
			return HasToReturn.canContinue;
		}
	}
}