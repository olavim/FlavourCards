using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HarmonyLib;
using UnityEngine;

namespace CinnamonFlavour.Extensions
{
    public class CharacterStatModifiersAdditionalData
    {
        private float _brandDuration = 2f;
        private float _brandDamageMultiplier = 1f;

        public int ShotsAfterReload { get; set; } = 0;
        public int ShotsAfterBlockRefresh { get; set; } = 0;
        public float BrandDuration
        {
            get => Mathf.Max(0.25f, this._brandDuration);
            set => this._brandDuration = value;
        }
        public float BrandDamageMultiplier
        {
            get => Mathf.Max(0, this._brandDamageMultiplier);
            set => this._brandDamageMultiplier = value;
        }
        public float BrandDurationMultiplier { get; set; } = 1f;
        public List<BrandObjectsToSpawn> BrandObjectsToSpawn { get; set; } = new();
        public bool EnableBrandLifeSteal { get; set; } = false;
        public Action<Vector2, bool, Player> DealtDamageToPlayerAction = default;

        /// <summary>
        /// Invoked when the the owner has branded another player.
        /// </summary>
        public Action<Player> PlayerBrandedAction = default;
    }

    public static class CharacterStatModifiersExtension
    {
        public static readonly ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalData> data = new();

        public static CharacterStatModifiersAdditionalData GetAdditionalData(this CharacterStatModifiers characterstats)
        {
            return data.GetOrCreateValue(characterstats);
        }
    }

    [HarmonyPatch(typeof(CharacterStatModifiers), "ResetStats")]
    class CharacterStatModifiersPatch_ResetStats
    {
        private static void Prefix(CharacterStatModifiers __instance)
        {
            __instance.GetAdditionalData().ShotsAfterReload = 0;
            __instance.GetAdditionalData().ShotsAfterBlockRefresh = 0;
            __instance.GetAdditionalData().EnableBrandLifeSteal = false;
            __instance.GetAdditionalData().BrandDuration = 1f;
            __instance.GetAdditionalData().BrandDurationMultiplier = 1f;
            __instance.GetAdditionalData().BrandDamageMultiplier = 1f;
            __instance.GetAdditionalData().BrandObjectsToSpawn = new();
            __instance.GetAdditionalData().DealtDamageToPlayerAction = default;
            __instance.GetAdditionalData().PlayerBrandedAction = default;
        }
    }

    [HarmonyPatch(typeof(CharacterStatModifiers), "DealtDamage")]
    class CharacterStatModifiersPatch_DealtDamage
    {
        private static void Postfix(CharacterStatModifiers __instance, Vector2 damage, bool selfDamage, Player damagedPlayer)
        {
            if (damagedPlayer != null)
            {
                __instance.GetAdditionalData().DealtDamageToPlayerAction?.Invoke(damage, selfDamage, damagedPlayer);
            }
        }
    }
}