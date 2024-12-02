using CinnamonFlavour.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace CinnamonFlavour
{
	public class Brand : MonoBehaviour
	{
		private static readonly float s_baseDamagePercentage = 0.015f;
		private static readonly float s_damageRate = 0.2f;

		private float _radius;
		private Color _color;
		private List<GameObject> _lineTargets;
		private List<float> _sparkCooldowns;
		private float _damageCooldown;
		private Player _player;

		public Player Brander { get; set; }

		public Color Color
		{
			get => this._color;
			set
			{
				this._color = value;
				var lrColor = this._color * 0.25f;
				lrColor.a = 1f;

				foreach (var lr in this.GetComponentsInChildren<LineRenderer>(true))
				{
					lr.startColor = lrColor;
					lr.endColor = lrColor;
				}
			}
		}

		private void Start()
		{
			this._radius = this.transform.Find("Circle").GetComponent<LineEffect>().radius;
			this._lineTargets = new();
			this._sparkCooldowns = new();
			this._player = this.GetComponentInParent<Player>();

			foreach (var le in this.transform.Find("Lines").GetComponentsInChildren<LineEffect>())
			{
				var go = new GameObject("LineTarget");
				go.transform.SetParent(this.transform);
				this.SetRandomPositionOnRadius(go);
				this._sparkCooldowns.Add(GetRandomSparkDuration());
				this._lineTargets.Add(go);
				le.Play(this.transform, go.transform, 0f);
			}
		}

		private void Update()
		{
			for (int i = 0; i < this._sparkCooldowns.Count; i++)
			{
				this._sparkCooldowns[i] -= Time.deltaTime;

				if (this._sparkCooldowns[i] <= 0)
				{
					this.SetRandomPositionOnRadius(this._lineTargets[i]);
					this._sparkCooldowns[i] = GetRandomSparkDuration();
				}
			}

			this._damageCooldown -= Time.deltaTime;

			if (this._damageCooldown <= 0)
			{
				float damage = this._player.data.maxHealth * this.Brander.data.stats.GetAdditionalData().BrandDamageMultiplier * Brand.s_baseDamagePercentage;
				this._player.data.healthHandler.TakeDamage(Vector2.up * damage, this.transform.position);

				if (this.Brander.data.stats.GetAdditionalData().EnableBrandLifeSteal)
				{
					this.Brander.data.healthHandler.Heal(damage * this.Brander.data.stats.lifeSteal);
				}

				this._damageCooldown = s_damageRate;
			}
		}

		private void SetRandomPositionOnRadius(GameObject target)
		{
			int degrees = UnityEngine.Random.Range(0, 360);
			target.transform.localPosition = Quaternion.Euler(0, 0, degrees) * (Vector2.up * this._radius * 1.2f);
		}

		private static float GetRandomSparkDuration()
		{
			return UnityEngine.Random.Range(0.1f, 0.5f);
		}
	}
}
