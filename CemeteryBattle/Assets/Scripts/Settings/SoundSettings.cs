using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace PainfulTest.Settings
{
    public class SoundSettings : MonoBehaviour
    {
        public AudioMixer SoundFXMixer;
        public AudioMixer SoundtrackMixer;

        private void Start()
        {
            SoundFXMixer.SetFloat("SoundFXVolume", GlobalSettings.GlobalSoundFXVolume);
            SoundtrackMixer.SetFloat("SoundtrackVolume", GlobalSettings.GlobalMusicVolume);
        }
    }
}