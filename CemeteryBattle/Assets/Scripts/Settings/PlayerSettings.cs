using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace PainfulTest.Settings
{
    public class PlayerSettings : MonoBehaviour
    {

        [Header("Text Componets")]
        [Space(5)]

        public Text InitialCountdown;

        [Space(10)]
        [Header("Sliders Components")]
        [Space(10)]

        public Slider CountDownSlider;
        public Slider MusicSlider;
        public Slider SoundFXSlider;

        [Space(5)]
        [Header("Default Values")]
        [Space(10)]

        private float _countdown;

        [Space(5)]
        [Header("Audio Mixers")]
        [Space(10)]

        public AudioMixer SoundtrackMixer;
        public AudioMixer SoundFXMixer;

        void Start()
        {
            #region SetInitialValues



            if (PlayerPrefs.HasKey("MusicVolume"))
            {
                GlobalSettings.GlobalMusicVolume = PlayerPrefs.GetFloat("MusicVolume");
            }

            if (PlayerPrefs.HasKey("SoundFXVolume"))
            {
                GlobalSettings.GlobalSoundFXVolume = PlayerPrefs.GetFloat("SoundFXVolume");
            }

            if (PlayerPrefs.HasKey("Timer"))
            {
                CountDownSlider.value = _countdown = PlayerPrefs.GetFloat("Timer");
            }

            MusicSlider.value = GlobalSettings.GlobalMusicVolume;
            SoundFXSlider.value = GlobalSettings.GlobalSoundFXVolume;

            #endregion
            Cursor.visible = true;

            OnInitialCountdownSliderChange();
        }

        public void OnInitialCountdownSliderChange()
        {
            _countdown = CountDownSlider.value;
            string minutes = Mathf.Floor(_countdown / 60).ToString("00");
            string seconds = Mathf.Floor(_countdown % 60).ToString("00");
            InitialCountdown.text = string.Concat(minutes, ":", seconds);
            PlayerPrefs.SetFloat("Timer", _countdown);
        }

        public void OnMusicSliderChange()
        {
            PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
            GlobalSettings.GlobalMusicVolume = MusicSlider.value;
            SoundtrackMixer.SetFloat("SoundtrackVolume", GlobalSettings.GlobalMusicVolume);
        }

        public void OnSoundFXSliderChange()
        {
            PlayerPrefs.SetFloat("SoundFXVolume", MusicSlider.value);
            GlobalSettings.GlobalSoundFXVolume = SoundFXSlider.value;
            SoundFXMixer.SetFloat("SoundFXVolume", GlobalSettings.GlobalSoundFXVolume);
        }

    }
}
