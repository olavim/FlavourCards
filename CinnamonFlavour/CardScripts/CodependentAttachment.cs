using System.Linq;
using UnboundLib;
using UnityEngine;

namespace CinnamonFlavour
{
	public class CodependentAttachment : MonoBehaviour
	{
		private Player _player;
		private Gun _gun;
		private GunAmmo _gunAmmo;

		private void Start()
		{
			this._player = this.GetComponent<Player>();
			this._gun = this._player.data.weaponHandler.gun;
			this._gunAmmo = this._gun.GetComponentInChildren<GunAmmo>();
			this._gun.ShootPojectileAction += this.OnShoot;
		}

		private void OnDestroy()
		{
			this._gun.ShootPojectileAction -= this.OnShoot;
		}

		private void OnShoot(GameObject projectile)
		{
			bool hasBrandedOpponent = PlayerManager.instance.players
				.Where(p => !p.data.dead)
				.Where(p => p.teamID != this._player.teamID)
				.Any(p => p.GetComponent<BrandHandler>().IsBrandedBy(this._player));

			if (hasBrandedOpponent)
			{
				this.ReloadAmmo();
			}
		}

		private void ReloadAmmo()
		{
			int currentAmmo = (int) this._gunAmmo.GetFieldValue("currentAmmo");

			this._gun.isReloading = false;
			this._gunAmmo.SetFieldValue("currentAmmo", currentAmmo + 1);
			this._gunAmmo.InvokeMethod("SoundStopReloadInProgress");
			this._gunAmmo.InvokeMethod("SetActiveBullets", false);
		}
	}
}
