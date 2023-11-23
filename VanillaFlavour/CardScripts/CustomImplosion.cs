using Photon.Pun;
using UnityEngine;

namespace VanillaFlavour
{
	public class CustomImplosion : MonoBehaviour
	{
		[SerializeField] private float _force = 0;
		[SerializeField] private float _drag = 0;
		[SerializeField] private float _time = 0;
		[SerializeField] private float _clampDist = 0;
		[SerializeField] private float _dmgPer100Hp = 0;

		private void Start()
		{
			this.GetComponent<Explosion>().HitTargetAction += this.HitTarget;
			this._clampDist *= this.transform.localScale.x;
			this._force *= this.transform.localScale.x;
		}

		public void HitTarget(Damagable damageable, float distance)
		{
			var attack = this.GetComponent<SpawnedAttack>();

			if (!attack.IsMine())
			{
				return;
			}

			var healthHandler = damageable.GetComponent<HealthHandler>();
			float dmg = attack.spawner.data.maxHealth * this._dmgPer100Hp * 0.01f * this.transform.localScale.x;
			healthHandler.CallTakeDamage(Vector2.up * dmg, this.transform.position, null, attack.spawner, true);

			healthHandler.GetComponent<PhotonView>().RPC("RPCA_SendForceTowardsPointOverTime", RpcTarget.All, new object[]
			{
				this._force,
				this._drag,
				this._clampDist,
				(Vector2) this.transform.position,
				this._time,
				0,
				false,
				false
			});
		}
	}
}