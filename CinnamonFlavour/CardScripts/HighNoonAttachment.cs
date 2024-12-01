using CinnamonFlavour.Extensions;
using Sonigon;
using SoundImplementation;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnboundLib;
using UnityEngine;

namespace CinnamonFlavour
{
	public class HighNoonAttachment : MonoBehaviour
	{
		[SerializeField] private SoundEvent _soundActivate = default;
		private bool _isActive;
		private float _activeDuration;
		private WeaponHandler _wh;
		private Player _player;
		private CharacterStatModifiers _stats;
		private GameObject _lineEffectPrefab;
		private List<LineEffect> _lineEffects = new();

		private void Start()
		{
			this._soundActivate.variables.audioMixerGroup = SoundVolumeManager.Instance.audioMixer.FindMatchingGroups("SFX")[0];
			this._wh = this.GetComponentInParent<WeaponHandler>();
			this._player = this.GetComponentInParent<Player>();
			this._stats = this.GetComponentInParent<CharacterStatModifiers>();
			this._lineEffectPrefab = this.transform.Find("LineEffect").gameObject;

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
				.Where(p => !p.data.dead)
				.Where(p => p.teamID != this._player.teamID)
				.Where(p => !p.data.block.IsBlocking())
				.Where(p => PlayerManager.instance.CanSeePlayer(this.transform.position, p).canSee)
				.ToList();

			foreach (var opponent in visibleOpponents)
			{
				opponent.GetComponent<BrandHandler>().Brand(this._player);

				var go = GameObject.Instantiate(this._lineEffectPrefab, this.transform);
				var lineEffect = go.GetComponent<LineEffect>();
				this._lineEffects.Add(lineEffect);
				lineEffect.Play(this.transform, opponent.transform, 0f);

				SoundManager.Instance.PlayAtPosition(this._soundActivate, SoundManager.Instance.GetTransform(), opponent.transform);
			}

			if (visibleOpponents.Count > 0)
			{
				this._isActive = true;
				this._activeDuration = 0.2f;
			}
		}

		private void Update()
		{
			if (this._isActive)
			{
				this._activeDuration -= Time.deltaTime;

				if (this._activeDuration <= 0)
				{
					foreach (var lineEffect in this._lineEffects)
					{
						lineEffect.Stop();
						GameObject.Destroy(lineEffect.gameObject);
					}

					this._lineEffects.Clear();
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
				var targetPos = target.transform.position;
				var myPos = this.transform.position;

				var compensation = (float) this._wh.gun.InvokeMethod("GetRangeCompensation", Vector3.Distance(targetPos, myPos)) * Vector3.up;
				this._wh.gun.SetFieldValue("forceShootDir", compensation + targetPos - myPos);
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
