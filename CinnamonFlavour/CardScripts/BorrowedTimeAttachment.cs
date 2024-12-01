using System.Linq;
using UnboundLib;
using UnityEngine;

namespace CinnamonFlavour
{
	public class BorrowedTimeAttachment : MonoBehaviour
	{
		// 70% more reload speed = 1 / 1.7 reload time multiplier
		private readonly float _reloadTimeMultiplier = 1f / 1.7f;
		private readonly float _blockCooldownMultiplier = 0.5f;
		private float _updateCooldown = 0;
		private bool _isActive;
		private Player _player;
		private CharacterData _data;
		private GunAmmo _ammo;

		private void Start()
		{
			this._player = this.GetComponentInParent<Player>();
			this._data = this.GetComponentInParent<CharacterData>();
			this._data.healthHandler.reviveAction += this.Stop;
			this._ammo = this._data.weaponHandler.gun.GetComponentInChildren<GunAmmo>();
		}

		private void OnDestroy()
		{
			this._data.healthHandler.reviveAction -= this.Stop;
		}

		private void PlayerBranded(Player player)
		{
			this.Activate();
		}

		private void Update()
		{
			this._updateCooldown -= Time.deltaTime;

			if (this._updateCooldown > 0)
			{
				return;
			}

			bool hasBrandedOpponent = PlayerManager.instance.players
				.Where(p => !p.data.dead)
				.Where(p => p.teamID != this._player.teamID)
				.Any(p => p.GetComponent<BrandHandler>().IsBrandedBy(this._player));

			if (!this._isActive && hasBrandedOpponent)
			{
				this.Activate();
			}

			if (this._isActive && !hasBrandedOpponent)
			{
				this.Stop();
			}

			this._updateCooldown = 0.1f;
		}

		private void Activate()
		{
			if (this._isActive)
			{
				return;
			}

			this._isActive = true;

			this._ammo.reloadTimeMultiplier *= this._reloadTimeMultiplier;
			float counter = (float) this._ammo.GetFieldValue("reloadCounter");
			this._ammo.SetFieldValue("reloadCounter", counter * this._reloadTimeMultiplier);

			this._data.block.cdMultiplier *= this._blockCooldownMultiplier;
			this._data.block.counter *= this._blockCooldownMultiplier;
		}

		private void Stop()
		{
			if (!this._isActive)
			{
				return;
			}

			this._isActive = false;

			this._ammo.reloadTimeMultiplier /= this._reloadTimeMultiplier;
			float counter = (float) this._ammo.GetFieldValue("reloadCounter");
			this._ammo.SetFieldValue("reloadCounter", counter / this._reloadTimeMultiplier);

			this._data.block.cdMultiplier /= this._blockCooldownMultiplier;
			this._data.block.counter /= this._blockCooldownMultiplier;
		}
	}
}
