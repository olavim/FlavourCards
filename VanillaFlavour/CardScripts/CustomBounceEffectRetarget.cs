using System.Collections;
using System.Linq;
using Photon.Pun;
using Sonigon;
using UnityEngine;

namespace VanillaFlavour
{
	public class CustomBounceEffectRetarget : BounceEffect
	{
		private const float Range = 8f;

		[SerializeField] private SoundEvent _soundTargetBounceTargetPlayer = default;

		private MoveTransform _move;
		private PhotonView _view;

		private void Start()
		{
			this._view = this.GetComponentInParent<PhotonView>();
			this._move = this.GetComponentInParent<MoveTransform>();
			this.GetComponentInParent<ChildRPC>().childRPCsVector2.Add("TargetBounce", this.SetNewVel);
			this.GetComponentInParent<ChildRPC>().childRPCsInt.Add("TargetBounceLine", this.DrawLineTo);
		}

		public override void DoBounce(HitInfo hit)
		{
			this.StartCoroutine(this.DelayMove(hit));
		}

		private void SetNewVel(Vector2 newVel)
		{
			this._move.enabled = true;
			this._move.velocity = newVel;
		}

		// Returns the closest visible player withing range
		private Player FindTarget(HitInfo hit)
		{
			var refPos = (Vector2) this.transform.position + hit.normal * 0.1f;

			Player result = null;
			float shortestDistance = float.PositiveInfinity;

			foreach (var player in PlayerManager.instance.players)
			{
				if (!player.data.dead)
				{
					float distance = Vector2.Distance(refPos, player.data.playerVel.position);
					if (PlayerManager.instance.CanSeePlayer(refPos, player).canSee && distance < shortestDistance && distance <= Range)
					{
						shortestDistance = distance;
						result = player;
					}
				}
			}

			return result;
		}

		private IEnumerator DelayMove(HitInfo hit)
		{
			var targetPlayer = this.FindTarget(hit);

			if (targetPlayer && this._view.IsMine)
			{
				this.GetComponentInParent<ChildRPC>().CallFunction("TargetBounceLine", targetPlayer.playerID);
			}

			this._move.enabled = false;

			if (hit.rigidbody)
			{
				this._move.GetComponent<RayCastTrail>().IgnoreRigFor(hit.rigidbody, 0.5f);
			}

			yield return new WaitForSeconds(0.1f);

			if (this._view.IsMine)
			{
				this.ActuallyDoBounce(targetPlayer?.playerID ?? -1);
			}
		}

		private void ActuallyDoBounce(int playerId)
		{
			var playerWithId = PlayerManager.instance.players.FirstOrDefault(p => p.playerID == playerId);

			if (!playerWithId)
			{
				// Normal bounce
				this.GetComponentInParent<ChildRPC>().CallFunction("TargetBounce", this._move.velocity);
				return;
			}

			// Targeted bounce
			var playerPos = playerWithId.data.playerVel.position;
			float compensation = this._move.GetUpwardsCompensation(this.transform.position, playerPos);
			var newVelocity = (playerPos + Vector2.up * compensation - (Vector2) this.transform.position).normalized * this._move.velocity.magnitude;
			this.GetComponentInParent<ChildRPC>().CallFunction("TargetBounce", newVelocity);
			SoundManager.Instance.PlayAtPosition(this._soundTargetBounceTargetPlayer, SoundManager.Instance.GetTransform(), this.transform);
		}

		private void DrawLineTo(int playerId)
		{
			var playerWithId = PlayerManager.instance.players.FirstOrDefault(p => p.playerID == playerId);
			if (playerWithId)
			{
				this.StartCoroutine(this.DrawLine(playerWithId.transform.position));
			}
		}

		private IEnumerator DrawLine(Vector3 position)
		{
			var line = this.GetComponentInChildren<LineEffect>(true);
			line.StartDraw();
			while (line)
			{
				line.DrawLine(this.transform.position, position);
				yield return null;
			}
		}
	}
}