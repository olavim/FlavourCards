using Sonigon;
using SoundImplementation;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CinnamonFlavour
{
	public class LifelineAttachment : MonoBehaviour
	{
		private GameObject _giveEffectTemplate;
		private List<GameObject> _giveEffects;
		private CharacterData _data;
		private float _cooldown = 0;
		private List<Player> _currentTargets = new();
		[SerializeField] private SoundEvent _soundHeal = default;

		private void Start()
		{
			this._data = this.GetComponentInParent<CharacterData>();
			this._soundHeal.variables.audioMixerGroup = SoundVolumeManager.Instance.audioMixer.FindMatchingGroups("SFX")[0];
			this._giveEffectTemplate = this.transform.Find("Give").gameObject;
			this._giveEffects = new List<GameObject>
			{
				this._giveEffectTemplate
			};
		}

		private void Update()
		{
			this._cooldown -= Time.deltaTime;

			if (this._cooldown > 0)
			{
				return;
			}

			if (this._data.stats.lifeSteal == 0)
			{
				return;
			}

			this._currentTargets = PlayerManager.instance.players
				.FindAll(p => !p.data.dead)
				.FindAll(p => p.teamID != this._data.player.teamID)
				.FindAll(p => p.GetComponent<BrandHandler>().IsBrandedBy(this._data.player));

			if (this._giveEffects.Count < this._currentTargets.Count)
			{
				this.AddGiveEffects(this._currentTargets.Count - this._giveEffects.Count);
			}
			else if (this._giveEffects.Count > this._currentTargets.Count)
			{
				this.RemoveGiveEffects(Math.Min(this._giveEffects.Count - 1, this._giveEffects.Count - this._currentTargets.Count));
			}

			for (var i = 0; i < this._currentTargets.Count; i++)
			{
				this._giveEffects[i].transform.position = this._currentTargets[i].transform.position;
			}

			if (this._currentTargets.Count > 0)
			{
				SoundManager.Instance.PlayAtPosition(this._soundHeal, SoundManager.Instance.GetTransform(), this.transform);

				foreach (var particleSystem in this.GetComponentsInChildren<ParticleSystem>())
				{
					particleSystem.Play();
				}
			}

			this._cooldown = 0.2f;
		}

		private void LateUpdate()
		{
			for (var i = 0; i < this._currentTargets.Count; i++)
			{
				this._giveEffects[i].transform.position = this._currentTargets[i].transform.position;
			}
		}

		private void AddGiveEffects(int count)
		{
			for (var i = 0; i < count; i++)
			{
				this._giveEffects.Add(Instantiate(this._giveEffectTemplate, this.transform));
			}
		}

		private void RemoveGiveEffects(int count)
		{
			for (var i = 0; i < count; i++)
			{
				GameObject.Destroy(this._giveEffects[this._giveEffects.Count - 1]);
				this._giveEffects.RemoveAt(this._giveEffects.Count - 1);
			}
		}
	}
}
