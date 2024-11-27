using CinnamonFlavour.Extensions;
using UnboundLib;
using UnityEngine;

namespace CinnamonFlavour
{
	public class CodependentAttachment : MonoBehaviour
	{
		private Player _player;

		private void Start()
		{
			this._player = this.GetComponent<Player>();
			this.GetComponent<CharacterStatModifiers>().GetAdditionalData().DealtDamageToPlayerAction += this.OnDealtDamage;
		}

		private void OnDestroy()
		{
			this.GetComponent<CharacterStatModifiers>().GetAdditionalData().DealtDamageToPlayerAction -= this.OnDealtDamage;
		}

		private void OnDealtDamage(Vector2 damage, bool selfDamage, Player player) {
			if (!selfDamage && player.GetComponent<BrandHandler>().IsBrandedBy(this._player)) {
				var gun = this._player.GetComponent<WeaponHandler>().gun;
				int bonusAmmo = gun.GetAdditionalData().AmmoOnHitBranded;
				var gunAmmo = gun.GetComponentInChildren<GunAmmo>();
				int currentAmmo = (int) gunAmmo.GetFieldValue("currentAmmo");

				gunAmmo.SetFieldValue("currentAmmo", currentAmmo + bonusAmmo);
				gunAmmo.InvokeMethod("SetActiveBullets", new object[] { false });
			}
		}
	}
}
