using BepInEx;
using HarmonyLib;
using SoundImplementation;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnboundLib.Cards;
using UnityEngine;

namespace VanillaFlavour
{
	[BepInPlugin(ModId, ModName, ModVersion)]
	public sealed class VanillaFlavour : BaseUnityPlugin
	{
		public const string ModId = "io.olavim.rounds.vanillaflavour";
		public const string ModName = "VanillaFlavour";
		public const string ModVersion = ThisAssembly.Project.Version;

		internal static Dictionary<string, object> RoundsResources { get; } = new();

		private void Start()
		{
			foreach (var res in Resources.FindObjectsOfTypeAll<GameObject>())
			{
				if (res.name.StartsWith("A_") || res.name.StartsWith("C_") || res.name.StartsWith("E_"))
				{
					RoundsResources.TryAdd(res.name, res);
				}
			}

			foreach (var res in Resources.FindObjectsOfTypeAll<SoundShotModifier>())
			{
				RoundsResources.TryAdd(res.name, res);
			}

			foreach (var res in Resources.FindObjectsOfTypeAll<SoundImpactModifier>())
			{
				RoundsResources.TryAdd(res.name, res);
			}

			foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(type => type.GetCustomAttribute<CardAttribute>() != null))
			{
				AccessTools.Method(typeof(CustomCard), nameof(CustomCard.BuildCard)).MakeGenericMethod(type).Invoke(null, null);
			}
		}
	}
}