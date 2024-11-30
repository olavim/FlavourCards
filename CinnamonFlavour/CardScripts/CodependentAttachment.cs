using CinnamonFlavour.Extensions;
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
			this.GetComponent<CharacterStatModifiers>().GetAdditionalData().DealtDamageToPlayerAction += this.OnDealtDamage;
		}

		private void OnDestroy()
		{
			this.GetComponent<CharacterStatModifiers>().GetAdditionalData().DealtDamageToPlayerAction -= this.OnDealtDamage;
		}

		private void OnDealtDamage(Vector2 damage, bool selfDamage, Player player)
		{
			if (!selfDamage && player.GetComponent<BrandHandler>().IsBrandedBy(this._player))
			{
				this.ReloadAmmo();
			}
		}

		private void ReloadAmmo()
		{
			int bonusAmmo = this._gun.GetAdditionalData().AmmoOnHitBranded;
			int currentAmmo = (int) this._gunAmmo.GetFieldValue("currentAmmo");

			this._gun.isReloading = false;
			this._gunAmmo.SetFieldValue("currentAmmo", currentAmmo + bonusAmmo);
			this._gunAmmo.InvokeMethod("SoundStopReloadInProgress");
			this._gunAmmo.InvokeMethod("SetActiveBullets", false);
		}
	}
}
