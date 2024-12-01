using CinnamonFlavour.Extensions;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnboundLib;
using UnityEngine;

namespace CinnamonFlavour
{
	public class BrandHandler : MonoBehaviour
	{
		private Player _player;
		private CharacterData _data;
		private Dictionary<int, float> _brandDurations;
		private Dictionary<int, GameObject> _brands;
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

			this._brandDurations = new Dictionary<int, float>();
			this._brands = new Dictionary<int, GameObject>();
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

			if (!this._brandDurations.ContainsKey(branderID))
			{
				this._brandDurations.Add(branderID, 0);

				var go = GameObject.Instantiate(this._brandPrefab, this._brandContainer.transform);

				var spawnedAttack = go.GetOrAddComponent<SpawnedAttack>();
				spawnedAttack.spawner = brander;

				var brand = go.GetComponent<Brand>();
				brand.Brander = brander;
				brand.Color = PlayerSkinBank.GetPlayerSkinColors(branderID).color;
				this._brands.Add(branderID, go);
			}

			this._brandDurations[branderID] = duration;

			if (brander)
			{
				brander.data.stats.GetAdditionalData().PlayerBrandedAction?.Invoke(this._player);
			}
		}

		public bool IsBrandedBy(Player player)
		{
			return this._brandDurations.ContainsKey(player.playerID);
		}

		private void Update()
		{
			foreach (var branderID in this._brandDurations.Keys.ToList())
			{
				this._brandDurations[branderID] -= Time.deltaTime;

				if (this._brandDurations[branderID] <= 0)
				{
					this.ExpireBrand(branderID);
				}
			}
		}

		private void ExpireBrand(int branderID)
		{
			var brander = PlayerManager.instance.players.FirstOrDefault(p => p.playerID == branderID);

			if (brander)
			{
				var expireEffects = brander.data.stats.GetAdditionalData().BrandObjectsToSpawn
					.Where(x => x.Trigger == BrandObjectsToSpawn.SpawnTrigger.Expire)
					.ToList();

				var spawnedAttack = this._brands[branderID].GetComponent<SpawnedAttack>();

				foreach (var effect in expireEffects)
				{
					BrandObjectsToSpawn.SpawnObject(effect, this._player.transform.position, brander, spawnedAttack);
				}
			}

			GameObject.Destroy(this._brands[branderID]);

			this._brandDurations.Remove(branderID);
			this._brands.Remove(branderID);
		}

		public void Reset()
		{
			this._brandDurations = new Dictionary<int, float>();

			foreach (var go in this._brands.Values.ToList())
			{
				GameObject.Destroy(go);
			}

			this._brands = new Dictionary<int, GameObject>();
		}
	}
}
