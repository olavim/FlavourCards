using System.Linq;
using TMPro;
using UnboundLib.Cards;
using UnityEngine;

namespace CinnamonFlavour
{
	public abstract class CinnamonFlavourCard : CustomCard
	{
		public override string GetModName()
		{
			return "Cinnamon Flavour";
		}

		private void Start()
        {
            var modNameObj = new GameObject("ModNameText");
			
            RectTransform[] allChildrenRecursive = this.GetComponentsInChildren<RectTransform>();
            var edgeTransform = allChildrenRecursive.FirstOrDefault(obj => obj.gameObject.name == "EdgePart (2)");

            if (edgeTransform != null)
            {
                GameObject bottomLeftCorner = edgeTransform.gameObject;
                modNameObj.gameObject.transform.SetParent(bottomLeftCorner.transform);

                var modText = modNameObj.gameObject.AddComponent<TextMeshProUGUI>();
                modText.text = this.GetModName().Replace(" ", "\n");
				modText.autoSizeTextContainer = true;
                modNameObj.transform.localEulerAngles = new Vector3(0f, 0f, 135f);

                modNameObj.transform.localScale = Vector3.one;
            	modNameObj.transform.localPosition = new Vector3(-75f, -75f, 0f);
                modText.alignment = TextAlignmentOptions.Bottom;
                modText.alpha = 0.1f;
                modText.fontSize = 54;
            }
        }
	}
}
