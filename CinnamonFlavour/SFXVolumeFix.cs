using Sonigon;
using SoundImplementation;
using UnityEngine;
using UnityEngine.Audio;

namespace CinnamonFlavour
{
    [RequireComponent(typeof(SoundUnityEventPlayer))]
    public class SFXVolumeFix : MonoBehaviour
    {
        private void Awake()
        {
            var group = SoundVolumeManager.Instance.audioMixer.FindMatchingGroups("SFX")[0];
            var player = this.GetComponent<SoundUnityEventPlayer>();
            this.SetAudioMixerGroup(player.soundStart, group);
            this.SetAudioMixerGroup(player.soundStartLoop, group);
            this.SetAudioMixerGroup(player.soundEnd, group);
        }

        private void SetAudioMixerGroup(SoundEvent soundEvent, AudioMixerGroup group)
        {
            if (!soundEvent)
            {
                return;
            }

            soundEvent.variables.audioMixerGroup = group;
        }
    }
}
