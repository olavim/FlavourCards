using CinnamonFlavour.Extensions;
using System.Linq;
using UnityEngine;

namespace CinnamonFlavour
{
	public class AmbushAttachment : MonoBehaviour
	{
		public void Trigger()
		{
			float range = 8f * this.transform.localScale.x;
			var brander = this.transform.GetComponentInParent<Player>();

			var nearbyVisibleOpponents = PlayerManager.instance.players
				.Where(p => p.teamID != brander.teamID)
				.Where(p => Vector2.Distance(brander.transform.position, p.transform.position) <= range)
				.Where(p => PlayerManager.instance.CanSeePlayer(brander.transform.position, p).canSee);
			
			foreach (var player in nearbyVisibleOpponents) {
				player.transform.GetComponent<BrandHandler>().Brand(brander);
			}
		}
	}
}
