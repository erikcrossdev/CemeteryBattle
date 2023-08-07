using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PainfulTest.Manager
{
    public class MatchManager : MonoBehaviour
    {
        public static MatchManager Instance;

        public float Timer;

        public bool TimeIsUp { get; private set; }

        [Header("Text Componets")]
        [Space(5)]

        public Text TimerText;
        public Text ArrowAmount;

        public UnityEvent OnArrowUpdate;

        void Awake()
        {
            Instance = this;
            if (!PlayerPrefs.HasKey("Timer"))
            {
                //Default value: 3:00"
                PlayerPrefs.SetFloat("Timer", 180);
            }
            Timer = PlayerPrefs.GetFloat("Timer");

            OnArrowUpdate.AddListener(UpdateUI);

            TimeIsUp = false;
        }

        void Update()
        {
            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
            }
            if (Timer <= 0)
            {
                TimeIsUp = true;
                StatisticsManager.Instance.ShowStatistics();
                SpawnManager.Instance.FreezeAllEnemies();
                Timer = 0;
            }
            string _minutes = Math.Floor(Timer / 60).ToString("00");
            string _seconds = Math.Floor(Timer % 60).ToString("00");

            TimerText.text = string.Concat(_minutes, ":", _seconds);

           
        }

        void UpdateUI()
        {
            ArrowAmount.text = Player.ShootingBehaviour.Instance._currentAmmo.ToString();
        }

    }
}
