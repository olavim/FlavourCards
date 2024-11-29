using CinnamonFlavour.Extensions;
using UnboundLib;
using UnityEngine;
using UnityEngine.Events;

namespace CinnamonFlavour
{
	public class PressureAttachment : MonoBehaviour
	{
		private float _remainingDuration = 0;
		private float _hpMultiplier = 3f;
		private float _speedMultiplier = 1.5f;
		private float _jumpMultiplier = 1.25f;
		private bool _isActive;
		private CharacterData _data;

		[SerializeField] private UnityEvent _startEvent = default;
		[SerializeField] private UnityEvent _endEvent = default;

		private void Start()
		{
			this._data = this.GetComponentInParent<CharacterData>();
			this._data.stats.GetAdditionalData().PlayerBrandedAction += this.PlayerBranded;
		}

		private void OnDestroy()
		{
			this._data.stats.GetAdditionalData().PlayerBrandedAction -= this.PlayerBranded;
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
				this._data.stats.movementSpeed *= this._speedMultiplier;
				this._data.stats.jump *= this._jumpMultiplier;
				this._data.health *= this._hpMultiplier;
				this._data.maxHealth *= this._hpMultiplier;
				this._data.stats.InvokeMethod("ConfigureMassAndSize");
				this._startEvent.Invoke();
			}
			
			this._isActive = true;
			this._remainingDuration = 3f;
		}

		public void Stop()
		{
			if (this._isActive)
			{
				this._isActive = false;
				this._data.stats.movementSpeed /= this._speedMultiplier;
				this._data.stats.jump /= this._jumpMultiplier;
				this._data.health /= this._hpMultiplier;
				this._data.maxHealth /= this._hpMultiplier;
				this._data.stats.InvokeMethod("ConfigureMassAndSize");
				this._endEvent.Invoke();
			}
		}
	}
}
