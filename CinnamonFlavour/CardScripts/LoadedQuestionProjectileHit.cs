using Sonigon;
using SoundImplementation;
using System.Linq;
using UnboundLib;
using UnityEngine;

namespace CinnamonFlavour
{
	public class LoadedQuestionProjectileHit : RayHitEffect
	{
		[SerializeField] private SoundEvent _hitSound = default;

		private bool _done;
		private GameObject _lineEffectPrefab;

		private void Start()
		{
			this._hitSound.variables.audioMixerGroup = SoundVolumeManager.Instance.audioMixer.FindMatchingGroups("SFX")[0];
			this._lineEffectPrefab = this.transform.Find("LineEffect").gameObject;
		}

		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (this._done || !hit.transform)
			{
				return HasToReturn.canContinue;
			}

			var point = hit.point + (hit.normal * 0.1f);

			var brander = this.GetComponentInParent<SpawnedAttack>().spawner;
			var targets = PlayerManager.instance.players
				.Where(p => p.teamID != brander.teamID)
				.Where(p => !p.data.dead)
				.Where(p => !p.data.block.IsBlocking())
				.Where(p => PlayerManager.instance.CanSeePlayer(point, p).canSee)
				.ToList();

			foreach (var target in targets)
			{
				target.transform.GetComponent<BrandHandler>().Brand(brander);
				SoundManager.Instance.PlayAtPosition(this._hitSound, SoundManager.Instance.GetTransform(), target.transform);

				var lineEffect = Instantiate(this._lineEffectPrefab, point, Quaternion.identity).GetComponent<LineEffect>();
				lineEffect.Play(target.transform, this.transform.position, 0f);
				Unbound.Instance.ExecuteAfterSeconds(0.2f, () => GameObject.Destroy(lineEffect.gameObject));
			}

			this._done = true;
			return HasToReturn.canContinue;
		}
	}
}