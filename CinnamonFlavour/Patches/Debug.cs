using HarmonyLib;
using UnboundLib.Utils;

namespace CinnamonFlavour.Patches
{

#if DEBUG

    [HarmonyPatch(typeof(DevConsole), "Send")]
    class Debug_DevConsolePatch_Send
    {
        static bool Prefix(string message)
        {
            if (message.StartsWith("/brand"))
            {
                string[] parts = message.Split(' ');
                int branderID = parts.Length > 1 ? int.Parse(parts[1]) : 0;
                int targetID = parts.Length > 2 ? int.Parse(parts[2]) : 0;
                float duration = parts.Length > 3 ? float.Parse(parts[3]) : 3600;

                Player target = PlayerManager.instance.players.Find(p => p.playerID == targetID);

                if (!target)
                {
                    UnityEngine.Debug.Log("Invalid target: " + targetID);
                }

                target.transform.GetComponent<BrandHandler>().Brand(branderID, duration);

                return false;
            }

            if (message.StartsWith("/resetbrands"))
            {
                foreach (var player in PlayerManager.instance.players)
                {
                    player.transform.GetComponent<BrandHandler>().Reset();
                }

                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(LevelManager), "SpawnMap")]
    class Debug_LevelManagerPatch_SpawnMap
    {
        static bool Prefix(string message)
        {
            return !message.StartsWith("/brand") && !message.StartsWith("/resetbrands");
        }
    }

#endif

}