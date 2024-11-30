using CinnamonFlavour.Extensions;
using Sonigon;
using SoundImplementation;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CinnamonFlavour
{
	public class MenaceAttachment : MonoBehaviour
	{
		[SerializeField] private SoundEvent _soundActivate = default;
		private float _cooldown = 0;
		private float _effectCooldown = 0;
		private LineEffect _circleLineEffect;
		private PlayLineAnimation _circlePlayLineAnimation;
		private GameObject _lineEffectTemplate;
		private List<GameObject> _lineEffects = new();

		private void Awake()
		{
			this._soundActivate.variables.audioMixerGroup = SoundVolumeManager.Instance.audioMixer.FindMatchingGroups("SFX")[0];
			this._lineEffectTemplate = this.transform.Find("LineEffect").gameObject;
			this._circleLineEffect = this.transform.Find("Circle").GetComponent<LineEffect>();
			this._circlePlayLineAnimation = this.transform.Find("Circle").GetComponent<PlayLineAnimation>();
		}

		public void Update()
		{
			this._effectCooldown -= Time.deltaTime;
			this._cooldown -= Time.deltaTime;

			if (this._cooldown > 0)
			{
				return;
			}

			foreach (var lineEffect in this._lineEffects)
			{
				GameObject.Destroy(lineEffect);
			}

			this._lineEffects.Clear();

			float range = 4.5f * this.transform.localScale.x;
			this._circleLineEffect.radius = range * 0.75f;
			var brander = this.transform.GetComponentInParent<Player>();

			var nearbyVisibleOpponents = PlayerManager.instance.players
				.Where(p => p.teamID != brander.teamID)
				.Where(p => Vector2.Distance(brander.transform.position, p.transform.position) <= range)
				.Where(p => PlayerManager.instance.CanSeePlayer(brander.transform.position, p).canSee);

			foreach (var player in nearbyVisibleOpponents)
			{
				player.transform.GetComponent<BrandHandler>().Brand(brander);
			}

			if (this._effectCooldown <= 0)
			{
				foreach (var player in nearbyVisibleOpponents)
				{
					var lineEffect = GameObject.Instantiate(this._lineEffectTemplate, this.transform);
					lineEffect.GetComponent<LineEffect>().Play(this.transform, player.transform);
					this._lineEffects.Add(lineEffect);
				}

				this._effectCooldown = 0.6f;
			}

			if (this._lineEffects.Count > 0)
			{
				SoundManager.Instance.PlayAtPosition(this._soundActivate, SoundManager.Instance.GetTransform(), this.transform);
			}

			if (nearbyVisibleOpponents.Any())
			{
				this._circlePlayLineAnimation.PlayWidth();
				this._circlePlayLineAnimation.PlayOffset();
			}

			this._cooldown = 0.2f;
		}
	}
}
