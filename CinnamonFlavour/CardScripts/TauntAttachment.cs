using CinnamonFlavour.Extensions;
using UnityEngine;

namespace CinnamonFlavour
{
	public class TauntAttachment : MonoBehaviour
	{
		private float _remainingDuration = 0;
		private float _blockCooldownMultiplier = 0.5f;
		private bool _isActive;
		private CharacterData _data;

		private void Start()
		{
			this._data = this.GetComponentInParent<CharacterData>();
			this._data.stats.GetAdditionalData().PlayerBrandedAction += this.PlayerBranded;
			this._data.healthHandler.reviveAction += this.Stop;
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
			if (this._remainingDuration > 0) {
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
				this._data.block.cdMultiplier *= this._blockCooldownMultiplier;
			}
			
			this._isActive = true;
			this._remainingDuration = 3f;
		}

		private void Stop()
		{
			if (this._isActive)
			{
				this._isActive = false;
				this._data.block.cdMultiplier /= this._blockCooldownMultiplier;
				this._data.block.counter /= this._blockCooldownMultiplier;
			}
		}
	}
}
