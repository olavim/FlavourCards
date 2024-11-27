using CinnamonFlavour.Extensions;
using Sonigon;
using SoundImplementation;
using System.Linq;
using UnityEngine;

namespace CinnamonFlavour
{
	public class EmberInTheDarkAttachment : MonoBehaviour
	{
		[SerializeField] private SoundEvent _soundActivate = default;
		private bool _isActive;
		private float _activeDuration;
		private CharacterData _data;
		private Player _player;
		private LineEffect _lineEffect;

		private void Awake()
		{
			this._soundActivate.variables.audioMixerGroup = SoundVolumeManager.Instance.audioMixer.FindMatchingGroups("SFX")[0];
		}

		private void Start()
		{
			this._lineEffect = this.GetComponentInChildren<LineEffect>(true);
			this._data = this.GetComponentInParent<CharacterData>();
			this._player = this.GetComponentInParent<Player>();
		}

		private void Update()
		{
			if (this._data?.weaponHandler.gun.isReloading == true && !this._isActive) {
				var visibleOpponents = PlayerManager.instance.players
					.Where(p => p.teamID != this._player.teamID)
					.Where(p => PlayerManager.instance.CanSeePlayer(this.transform.position, p).canSee)
					.ToList();
				
				if (visibleOpponents.Count > 0) {
					var randomTarget = visibleOpponents[UnityEngine.Random.Range(0, visibleOpponents.Count)];
					randomTarget.transform.GetComponent<BrandHandler>().Brand(this._player);

					this._isActive = true;
					this._activeDuration = 0.2f;
					this._lineEffect.Play(this.transform, randomTarget.transform, 0f);
					SoundManager.Instance.PlayAtPosition(this._soundActivate, SoundManager.Instance.GetTransform(), randomTarget.transform);
				}
			}

			if (this._isActive) {
				this._activeDuration -= Time.deltaTime;

				if (this._activeDuration <= 0) {
					this._lineEffect.Stop();
					this._lineEffect.gameObject.SetActive(false);
				}

				if (this._data?.weaponHandler.gun.isReloading == false) {
					this._isActive = false;
				}
			}
		}
	}
}
