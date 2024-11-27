using CinnamonFlavour.Extensions;
using System.Collections;
using System.Linq;
using UnboundLib;
using UnityEngine;

namespace CinnamonFlavour
{
	public class HighNoonAttachment : MonoBehaviour
	{
		private WeaponHandler _wh;
		private Player _player;
		private CharacterStatModifiers _stats;

		private void Start()
		{
			this._wh = this.GetComponentInParent<WeaponHandler>();
			this._player = this.GetComponentInParent<Player>();
			this._stats = this.GetComponentInParent<CharacterStatModifiers>();

			this._stats.OnReloadDoneAction += this.OnReloadDone;
		}

		private void OnDestroy()
		{
			this._stats.OnReloadDoneAction -= this.OnReloadDone;
		}

		private void OnReloadDone(int bullets) {
			int maxShots = Mathf.Min(this._stats.GetAdditionalData().ShotsAfterReload, Mathf.CeilToInt((float) bullets / this._wh.gun.numberOfProjectiles));
			this.StartCoroutine(this.Shoot(maxShots));
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
	}
}
