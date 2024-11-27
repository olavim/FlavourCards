using BepInEx;
using HarmonyLib;
using Sonigon;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnboundLib.Cards;
using UnityEngine;

namespace CinnamonFlavour
{
	[BepInPlugin(ModId, ModName, ModVersion)]
	public sealed class CinnamonFlavour : BaseUnityPlugin
	{
		public const string ModId = "io.olavim.rounds.cinnamonflavour";
		public const string ModName = "CinnamonFlavour";
		public const string ModVersion = ThisAssembly.Project.Version;

		internal static Dictionary<string, object> RoundsResources { get; } = new();
		internal static Dictionary<string, object> CustomResources { get; } = new();

		private static AssetBundle LoadAssetBundle(string name)
		{
			var asm = Assembly.GetExecutingAssembly();
			using var stream = asm.GetManifestResourceStream($"CinnamonFlavour.Assets.{name}");
			return AssetBundle.LoadFromStream(stream);
		}
		
        private void Awake()
        {
            new Harmony(ModId).PatchAll();
        }

		private void Start()
		{
			var artBundle = LoadAssetBundle("art");
			var attachmentsBundle = LoadAssetBundle("attachments");
			var sfxBundle = LoadAssetBundle("sfx");

			foreach (var res in Resources.FindObjectsOfTypeAll<SoundEvent>())
			{
				RoundsResources.TryAdd(res.name, res);
			}

			foreach (var res in artBundle.LoadAllAssets<GameObject>())
			{
				CustomResources.Add(res.name, res);
			}

			foreach (var res in attachmentsBundle.LoadAllAssets<GameObject>())
			{
				CustomResources.Add(res.name, res);
			}

			foreach (var res in sfxBundle.LoadAllAssets<SoundEvent>())
			{
				CustomResources.Add(res.name, res);
			}

			foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(type => type.GetCustomAttribute<CardAttribute>() != null))
			{
				AccessTools.Method(typeof(CustomCard), nameof(CustomCard.BuildCard)).MakeGenericMethod(type).Invoke(null, null);
			}
		}
	}
}