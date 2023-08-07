using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace PainfulTest.Manager
{
    public class MatchManager : MonoBehaviour
    {
        public static MatchManager Instance;

        [SerializeField] private float _timer = 180;
        public float Timer => _timer;

        public bool TimeIsUp { get; private set; }

        [Header("Text Componets")]
        [Space(5)]

        [SerializeField] protected TMPro.TextMeshProUGUI _timerText;
        [SerializeField] protected TMPro.TextMeshProUGUI _arrowAmount;

        public UnityEvent OnArrowUpdate;

        void Awake()
        {
            Instance = this;
            if (!PlayerPrefs.HasKey("Timer"))
            {
                //Default value: 3:00"
                PlayerPrefs.SetFloat("Timer", 180);
                Debug.LogError("Setting float to 180 ");
            }
            _timer = PlayerPrefs.GetFloat("Timer");
            Debug.LogError("Timer from player prefs is " + PlayerPrefs.GetFloat("Timer"));

            OnArrowUpdate.AddListener(UpdateUI);

            TimeIsUp = false;
        }

        void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            if (Timer <= 0)
            {
                TimeIsUp = true;
                StatisticsManager.Instance.ShowStatistics();
                SpawnManager.Instance.FreezeAllEnemies();
                _timer = 0;
            }
            string _minutes = Math.Floor(Timer / 60).ToString("00");
            string _seconds = Math.Floor(Timer % 60).ToString("00");

            _timerText.text = string.Concat(_minutes, ":", _seconds);
        }

        void UpdateUI()
        {
            _arrowAmount.text = Player.ShootingBehaviour.Instance._currentAmmo.ToString();
        }

    }
}
