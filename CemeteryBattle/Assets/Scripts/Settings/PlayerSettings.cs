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

        [SerializeField] private Text _initialCountdown;

        [Space(10)]
        [Header("Sliders Components")]
        [Space(10)]

        [SerializeField] private Slider _countDownSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundFXSlider;

        [Space(5)]
        [Header("Default Values")]
        [Space(10)]
        [SerializeField, Range(60, 180)]
        private float _countdown;

        [Space(5)]
        [Header("Audio Mixers")]
        [Space(10)]

        [SerializeField] private AudioMixer _soundtrackMixer;
        [SerializeField] private AudioMixer _soundFXMixer;

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
                _countDownSlider.value = _countdown = PlayerPrefs.GetFloat("Timer");
            }

            _musicSlider.value = GlobalSettings.GlobalMusicVolume;
            _soundFXSlider.value = GlobalSettings.GlobalSoundFXVolume;

            #endregion
            Cursor.visible = true;

            OnInitialCountdownSliderChange();
        }

        public void OnInitialCountdownSliderChange()
        {
            _countdown = _countDownSlider.value;
            string minutes = Mathf.Floor(_countdown / 60).ToString("00");
            string seconds = Mathf.Floor(_countdown % 60).ToString("00");
            _initialCountdown.text = string.Concat(minutes, ":", seconds);
            PlayerPrefs.SetFloat("Timer", _countdown);
        }

        public void OnMusicSliderChange()
        {
            PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);
            GlobalSettings.GlobalMusicVolume = _musicSlider.value;
            _soundtrackMixer.SetFloat("SoundtrackVolume", GlobalSettings.GlobalMusicVolume);
        }

        public void OnSoundFXSliderChange()
        {
            PlayerPrefs.SetFloat("SoundFXVolume", _musicSlider.value);
            GlobalSettings.GlobalSoundFXVolume = _soundFXSlider.value;
            _soundFXMixer.SetFloat("SoundFXVolume", GlobalSettings.GlobalSoundFXVolume);
        }

    }
}
