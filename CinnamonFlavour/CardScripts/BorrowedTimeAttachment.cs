using CinnamonFlavour.Extensions;
using UnboundLib;
using UnityEngine;

namespace CinnamonFlavour
{
	public class BorrowedTimeAttachment : MonoBehaviour
	{
		private float _remainingDuration = 0;

		// 70% more reload speed = 1 / 1.7 reload time multiplier
		private float _reloadTimeMultiplier = 1f / 1.7f;
		private float _blockCooldownMultiplier = 0.5f;
		private bool _isActive;
		private CharacterData _data;
		private GunAmmo _ammo;

		private void Start()
		{
			this._data = this.GetComponentInParent<CharacterData>();
			this._data.stats.GetAdditionalData().PlayerBrandedAction += this.PlayerBranded;
			this._data.healthHandler.reviveAction += this.Stop;
			this._ammo = this._data.weaponHandler.gun.GetComponentInChildren<GunAmmo>();
		}

		private void OnDestroy()
		{
			this._data.stats.GetAdditionalData().PlayerBrandedAction -= this.PlayerBranded;
			this._data.healthHandler.reviveAction -= this.Stop;
		}

		private void PlayerBranded(Player player)
		{
			this.Activate();
		}

		private void Update()
		{
			if (this._remainingDuration > 0)
			{
				this._remainingDuration -= Time.deltaTime;
			}

			if (this._isActive && this._remainingDuration <= 0)
			{
				this.Stop();
			}
		}

		private void Activate()
		{
			if (!this._isActive)
			{
				this._ammo.reloadTimeMultiplier *= this._reloadTimeMultiplier;
				float counter = (float) this._ammo.GetFieldValue("reloadCounter");
				this._ammo.SetFieldValue("reloadCounter", counter * this._reloadTimeMultiplier);

				this._data.block.cdMultiplier *= this._blockCooldownMultiplier;
				this._data.block.counter *= this._blockCooldownMultiplier;
			}

			this._isActive = true;
			this._remainingDuration = 3f;
		}

		private void Stop()
		{
			if (this._isActive)
			{
				this._isActive = false;

				this._ammo.reloadTimeMultiplier /= this._reloadTimeMultiplier;
				float counter = (float) this._ammo.GetFieldValue("reloadCounter");
				this._ammo.SetFieldValue("reloadCounter", counter / this._reloadTimeMultiplier);

				this._data.block.cdMultiplier /= this._blockCooldownMultiplier;
				this._data.block.counter /= this._blockCooldownMultiplier;
			}
		}
	}
}
