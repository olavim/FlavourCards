using CinnamonFlavour.Extensions;
using Photon.Pun;
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
		private Dictionary<int, GameObject> _brandVisuals;
		private GameObject _brandPrefab;
		private GameObject _brandContainer;

		private void Awake()
		{
			this._brandPrefab = (GameObject) CinnamonFlavour.CustomResources["Brand"];
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
			this._brandVisuals = new Dictionary<int, GameObject>();
		}

		private void OnDestroy()
		{
			this._data.healthHandler.reviveAction -= this.Reset;
		}

		public void Brand(Player brander)
		{
			var stats = brander.data.stats;
			float duration = stats.GetAdditionalData().BrandDuration * stats.GetAdditionalData().BrandDurationMultiplier;
			this.Brand(brander.playerID, duration);
		}

		internal void Brand(int branderID, float duration)
		{
			if (!this._player.data.view.IsMine)
			{
				return;
			}

			if (PhotonNetwork.OfflineMode)
			{
				this.RPCA_Brand(branderID, duration);
			}
			else
			{
				this._player.data.view.RPC("RPCA_Brand", RpcTarget.All, branderID, duration);
			}
		}

		[PunRPC]
		private void RPCA_Brand(int branderID, float duration)
		{
			var brander = PlayerManager.instance.players.FirstOrDefault(p => p.playerID == branderID);

			if (!this._brands.ContainsKey(branderID))
			{
				this._brands.Add(branderID, 0);

				var go = GameObject.Instantiate(this._brandPrefab, this._brandContainer.transform);
				var brand = go.GetComponent<Brand>();
				brand.Brander = brander;
				brand.Color = PlayerSkinBank.GetPlayerSkinColors(branderID).color;
				this._brandVisuals.Add(branderID, go);
			}

			this._brands[branderID] = duration;

			if (brander)
			{
				brander.data.stats.GetAdditionalData().PlayerBrandedAction?.Invoke(this._player);
			}
		}

		public bool IsBrandedBy(Player player)
		{
			return this._brands.ContainsKey(player.playerID);
		}

		private void Update()
		{
			foreach (var branderID in this._brands.Keys.ToList())
			{
				this._brands[branderID] -= Time.deltaTime;

				if (this._brands[branderID] <= 0)
				{
					GameObject.Destroy(this._brandVisuals[branderID]);

					this._brands.Remove(branderID);
					this._brandVisuals.Remove(branderID);
				}
			}
		}

		public void Reset()
		{
			this._brands = new Dictionary<int, float>();

			foreach (var go in this._brandVisuals.Values.ToList())
			{
				GameObject.Destroy(go);
			}

			this._brandVisuals = new Dictionary<int, GameObject>();
		}
	}
}
