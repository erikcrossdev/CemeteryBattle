using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace PainfulTest.Settings
{
    public class SoundSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixer _soundFXMixer;
        [SerializeField] private AudioMixer _soundtrackMixer;

        private void Start()
        {
            _soundFXMixer.SetFloat("SoundFXVolume", GlobalSettings.GlobalSoundFXVolume);
            _soundtrackMixer.SetFloat("SoundtrackVolume", GlobalSettings.GlobalMusicVolume);
        }
    }
}