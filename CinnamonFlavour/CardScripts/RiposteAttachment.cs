using CinnamonFlavour.Extensions;
using System.Collections;
using System.Linq;
using UnboundLib;
using UnityEngine;

namespace CinnamonFlavour
{
	public class RiposteAttachment : MonoBehaviour
	{
		private WeaponHandler _wh;
		private Player _player;
		private CharacterStatModifiers _stats;

		private void Start()
		{
			this._wh = this.GetComponentInParent<WeaponHandler>();
			this._player = this.GetComponentInParent<Player>();
			this._stats = this.GetComponentInParent<CharacterStatModifiers>();

			this._player.data.block.BlockRechargeAction += this.OnBlockRefreshed;
		}

		private void OnDestroy()
		{
			this._player.data.block.BlockRechargeAction -= this.OnBlockRefreshed;
		}

		private void OnBlockRefreshed() {
			this.StartCoroutine(this.Shoot(this._stats.GetAdditionalData().ShotsAfterBlockRefresh));
		}

		private IEnumerator Shoot(int shots)
		{
			var opponents = PlayerManager.instance.players.Where(p => p.teamID != this._player.teamID);

			for (int i = 0; i < shots; i++)
			{
				yield return new WaitForSeconds(0.1f);

				if (this._wh.gun.isReloading) {
					break;
				}
				
				var visibleBrandedOpponents = opponents
					.Where(p => !p.data.dead)
					.Where(p => p.transform.GetComponent<BrandHandler>().IsBrandedBy(this._player))
					.Where(p => PlayerManager.instance.CanSeePlayer(this.transform.position, p).canSee)
					.ToList();
				
				if (visibleBrandedOpponents.Count == 0) {
					break;
				}

				var target = visibleBrandedOpponents[UnityEngine.Random.Range(0, visibleBrandedOpponents.Count)];

				this._wh.gun.SetFieldValue("forceShootDir", target.transform.position - this._player.transform.position);
				bool didShoot = this._wh.gun.Attack(0f, true, 1f, 1f, true);
				this._wh.gun.SetFieldValue("forceShootDir", Vector3.zero);

				if (!didShoot) {
					break;
				}
			}
		}

		public void BlockTrigger()
		{
			float range = 8f * this.transform.localScale.x;
			var brander = this.transform.GetComponentInParent<Player>();

			var nearbyVisibleOpponents = PlayerManager.instance.players
				.Where(p => p.teamID != brander.teamID)
				.Where(p => Vector2.Distance(brander.transform.position, p.transform.position) <= range)
				.Where(p => PlayerManager.instance.CanSeePlayer(brander.transform.position, p).canSee);
			
			foreach (var player in nearbyVisibleOpponents) {
				player.transform.GetComponent<BrandHandler>().Brand(brander);
			}
		}
	}
}
