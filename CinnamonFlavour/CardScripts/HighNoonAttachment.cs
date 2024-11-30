using CinnamonFlavour.Extensions;
using Sonigon;
using SoundImplementation;
using System.Collections;
using System.Linq;
using UnboundLib;
using UnityEngine;

namespace CinnamonFlavour
{
	public class HighNoonAttachment : MonoBehaviour
	{
		[SerializeField] private SoundEvent _soundActivate = default;
		private bool _isActive;
		private bool _startedReloading;
		private float _activeDuration;
		private CharacterData _data;
		private WeaponHandler _wh;
		private Player _player;
		private CharacterStatModifiers _stats;
		private LineEffect _lineEffect;

		private void Start()
		{
			this._soundActivate.variables.audioMixerGroup = SoundVolumeManager.Instance.audioMixer.FindMatchingGroups("SFX")[0];
			this._wh = this.GetComponentInParent<WeaponHandler>();
			this._data = this.GetComponentInParent<CharacterData>();
			this._player = this.GetComponentInParent<Player>();
			this._stats = this.GetComponentInParent<CharacterStatModifiers>();
			this._lineEffect = this.GetComponentInChildren<LineEffect>(true);

			this._stats.OnReloadDoneAction += this.OnReloadDone;
			this._stats.OutOfAmmpAction += this.OnReloadStarted;
		}

		private void OnDestroy()
		{
			this._stats.OnReloadDoneAction -= this.OnReloadDone;
		}

		private void OnReloadDone(int bullets)
		{
			int maxShots = Mathf.Min(this._stats.GetAdditionalData().ShotsAfterReload, Mathf.CeilToInt((float) bullets / this._wh.gun.numberOfProjectiles));
			this.StartCoroutine(this.Shoot(maxShots));
		}

		private void OnReloadStarted(int bullets)
		{
			this._isActive = true;

			var visibleOpponents = PlayerManager.instance.players
				.Where(p => p.teamID != this._player.teamID)
				.Where(p => PlayerManager.instance.CanSeePlayer(this.transform.position, p).canSee)
				.ToList();

			if (visibleOpponents.Count > 0)
			{
				var randomTarget = visibleOpponents[UnityEngine.Random.Range(0, visibleOpponents.Count)];
				randomTarget.transform.GetComponent<BrandHandler>().Brand(this._player);

				this._isActive = true;
				this._activeDuration = 0.2f;
				this._lineEffect.Play(this.transform, randomTarget.transform, 0f);
				SoundManager.Instance.PlayAtPosition(this._soundActivate, SoundManager.Instance.GetTransform(), randomTarget.transform);
			}
		}

		private void Update()
		{
			if (this._isActive)
			{
				this._activeDuration -= Time.deltaTime;

				if (this._activeDuration <= 0)
				{
					this._lineEffect.Stop();
					this._lineEffect.gameObject.SetActive(false);
					this._isActive = false;
				}
			}
		}

		private IEnumerator Shoot(int shots)
		{
			var opponents = PlayerManager.instance.players.Where(p => p.teamID != this._player.teamID);

			for (int i = 0; i < shots; i++)
			{
				yield return new WaitForSeconds(0.1f);

				if (this._wh.gun.isReloading)
				{
					break;
				}

				var visibleBrandedOpponents = opponents
					.Where(p => !p.data.dead)
					.Where(p => p.transform.GetComponent<BrandHandler>().IsBrandedBy(this._player))
					.Where(p => PlayerManager.instance.CanSeePlayer(this.transform.position, p).canSee)
					.ToList();

				if (visibleBrandedOpponents.Count == 0)
				{
					break;
				}

				var target = visibleBrandedOpponents[UnityEngine.Random.Range(0, visibleBrandedOpponents.Count)];

				this._wh.gun.SetFieldValue("forceShootDir", target.transform.position - this._player.transform.position);
				bool didShoot = this._wh.gun.Attack(0f, true, 1f, 1f, true);
				this._wh.gun.SetFieldValue("forceShootDir", Vector3.zero);

				if (!didShoot)
				{
					break;
				}
			}
		}
	}
}
