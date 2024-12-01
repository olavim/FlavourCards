using CinnamonFlavour.Extensions;
using System.Linq;
using UnboundLib;
using UnityEngine;
using UnityEngine.Events;

namespace CinnamonFlavour
{
	public class PressureAttachment : MonoBehaviour
	{
		private readonly float _hpMultiplier = 3f;
		private readonly float _speedMultiplier = 1.5f;
		private readonly float _jumpMultiplier = 1.25f;
		private float _updateCooldown = 0;
		private bool _isActive;
		private Player _player;
		private CharacterData _data;

		[SerializeField] private UnityEvent _startEvent = default;
		[SerializeField] private UnityEvent _endEvent = default;

		private void Start()
		{
			this._player = this.GetComponentInParent<Player>();
			this._data = this.GetComponentInParent<CharacterData>();
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

			this._data.stats.movementSpeed *= this._speedMultiplier;
			this._data.stats.jump *= this._jumpMultiplier;
			this._data.health *= this._hpMultiplier;
			this._data.maxHealth *= this._hpMultiplier;
			this._data.stats.InvokeMethod("ConfigureMassAndSize");
			this._startEvent.Invoke();
		}

		public void Stop()
		{
			if (!this._isActive)
			{
				return;
			}

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
