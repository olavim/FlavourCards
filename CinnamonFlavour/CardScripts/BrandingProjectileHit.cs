namespace CinnamonFlavour
{
	public class BrandingProjectileHit : RayHitEffect
	{
		private bool _done;

		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			var brander = this.GetComponentInParent<SpawnedAttack>().spawner;
			var player = hit.transform?.GetComponentInParent<Player>();

			if (this._done || !player || player.teamID == brander.teamID)
			{
				return HasToReturn.canContinue;
			}

			player.transform.GetComponent<BrandHandler>().Brand(brander);
			this._done = true;
			return HasToReturn.canContinue;
		}
	}
}