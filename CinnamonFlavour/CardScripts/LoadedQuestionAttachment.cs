using Sonigon;
using SoundImplementation;
using UnityEngine;

namespace CinnamonFlavour
{
	public class LoadedQuestionAttachment : MonoBehaviour
	{
		private CharacterData _data;
		private int _activeDuration;
		private bool _isParticleSystemActive;
		private GameObject _effectPrefab;
		private Transform _particleTransform;
		private ParticleSystem[] _particleSystems;

		[SerializeField] private SoundEvent _soundActivate = default;

		private void Awake() {
			this._effectPrefab = (GameObject) CinnamonFlavour.CustomResources["E_LoadedQuestion"];
			this._soundActivate.variables.audioMixerGroup = SoundVolumeManager.Instance.audioMixer.FindMatchingGroups("SFX")[0];
		}

		private void Start()
		{
			this._particleTransform = this.transform.GetChild(0);
			this._particleSystems = this.GetComponentsInChildren<ParticleSystem>();
			this._data = this.GetComponentInParent<CharacterData>();
			this._data.healthHandler.reviveAction += this.Reset;
			this._data.weaponHandler.gun.ShootPojectileAction += this.Attack;
		}

		private void OnDestroy()
		{
			this._data.healthHandler.reviveAction -= this.Reset;
			this._data.weaponHandler.gun.ShootPojectileAction -= this.Attack;
		}

		private void Reset()
		{
			this._activeDuration = 0;
		}

		private void Update()
		{
			if (this._data?.weaponHandler.gun.isReloading == true && this._activeDuration == 0) {
				this._activeDuration = this._data.weaponHandler.gun.numberOfProjectiles;
			}

			if (this._activeDuration > 0)
			{
				this._particleTransform.position = this._data.weaponHandler.gun.transform.position;
				this._particleTransform.rotation = this._data.weaponHandler.gun.transform.rotation;
			}

			if (this._activeDuration > 0 && !this._isParticleSystemActive)
			{
				SoundManager.Instance.PlayAtPosition(this._soundActivate, SoundManager.Instance.GetTransform(), this.transform);
				foreach (var particleSystem in this._particleSystems)
				{
					particleSystem.Play();
				}
				this._isParticleSystemActive = true;
			}
			
			if (this._activeDuration == 0 && this._isParticleSystemActive)
			{
				foreach (var particleSystem in this._particleSystems)
				{
					particleSystem.Stop();
				}
				this._isParticleSystemActive = false;
			}
		}

		private void Attack(GameObject projectile)
		{
			var attack = projectile.GetComponent<SpawnedAttack>();
			if (!attack)
			{
				return;
			}

			if (this._activeDuration > 0)
			{
				attack.SetColor(new Color32(100, 0, 0, 255));
				GameObject.Instantiate(this._effectPrefab, projectile.transform.position, projectile.transform.rotation, projectile.transform);
				this._activeDuration--;
			}
		}
	}
}
