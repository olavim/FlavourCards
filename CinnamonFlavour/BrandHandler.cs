using CinnamonFlavour.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CinnamonFlavour
{
	public class BrandHandler : MonoBehaviour
	{
		private Player _player;
		private CharacterData _data;
		private Dictionary<int, float> _brands;
		private Dictionary<int, Color> _brandColors;
		private Dictionary<int, GameObject> _brandVisuals;
		private GameObject _brandVisualPrefab;
		private GameObject _brandContainer;

		private void Awake() {
			this._brandVisualPrefab = (GameObject) CinnamonFlavour.CustomResources["Brand"];
			this._brandContainer = new GameObject("Brands");
			this._brandContainer.transform.SetParent(this.transform);
			this._brandContainer.transform.localPosition = Vector3.zero;
		}

		private void Start()
		{
			this._player = this.GetComponent<Player>();
			this._data = this._player.data;
			this._data.healthHandler.reviveAction += this.Reset;

			this._brands = new Dictionary<int, float>();
			this._brandColors = new Dictionary<int, Color>();
			this._brandVisuals = new Dictionary<int, GameObject>();
		}

		private void OnDestroy()
		{
			this._data.healthHandler.reviveAction -= this.Reset;
		}

		public void Brand(Player brander) {
			var stats = brander.data.stats;
			float duration = stats.GetAdditionalData().BrandDuration * stats.GetAdditionalData().BrandDurationMultiplier;
			this.Brand(brander.playerID, duration);
		}

		public void Brand(int branderID, float duration) {
			if (!this._brands.ContainsKey(branderID)) {
				this._brands.Add(branderID, 0);

				var go = GameObject.Instantiate(this._brandVisualPrefab, this._brandContainer.transform);
				this._brandVisuals.Add(branderID, go);
				
				var playerSkinColors = PlayerSkinBank.GetPlayerSkinColors(branderID);
				this._brandColors.Add(branderID, playerSkinColors.color);

				this.UpdateBrandVisuals();
			}

			this._brands[branderID] = duration;

			var brander = PlayerManager.instance.players.FirstOrDefault(p => p.playerID == branderID);
			if (brander) {
				brander.data.stats.GetAdditionalData().PlayerBrandedAction?.Invoke(this._player);
			}
		}

		public bool IsBrandedBy(Player player) {
			return this._brands.ContainsKey(player.playerID);
		}

		private void Update() {
			bool removed = false;

			foreach (var branderID in this._brands.Keys.ToList()) {
				this._brands[branderID] -= Time.deltaTime;

				if (this._brands[branderID] <= 0) {
					GameObject.Destroy(this._brandVisuals[branderID]);

					this._brands.Remove(branderID);
					this._brandVisuals.Remove(branderID);
					this._brandColors.Remove(branderID);

					removed = true;
				}
			}

			if (removed) {
				this.UpdateBrandVisuals();
			}
		}

		private void LateUpdate() {
			if (this._brands.Count > 1) {
				this._brandContainer.transform.rotation *= Quaternion.AngleAxis(Time.deltaTime * 100, Vector3.forward);
			} else {
				this._brandContainer.transform.rotation = Quaternion.identity;
			}
		}

		private void UpdateBrandVisuals() {
			float degreesPerBrand = 360f / this._brands.Count;
			float currentDegrees = 0;

			foreach (var branderID in this._brandVisuals.Keys) {
				var go = this._brandVisuals[branderID];
				go.transform.localScale = Vector3.one * 0.5f;
				var rotation = Quaternion.AngleAxis(currentDegrees, Vector3.forward);
				go.transform.localPosition = rotation * (Vector3.up * 0.5f);
				go.transform.localRotation = rotation;
				go.GetComponentsInChildren<SpriteRenderer>().First(r => r.gameObject.name == "Foreground").color = this._brandColors[branderID];
				currentDegrees = (currentDegrees + degreesPerBrand) % 360;
			}
		}

		public void Reset() {
			this._brands = new Dictionary<int, float>();
			this._brandColors = new Dictionary<int, Color>();

			foreach (var go in this._brandVisuals.Values.ToList()) {
				GameObject.Destroy(go);
			}

			this._brandVisuals = new Dictionary<int, GameObject>();
		}
	}
}
