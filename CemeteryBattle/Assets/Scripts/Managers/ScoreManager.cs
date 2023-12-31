﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PainfulTest.Manager
{    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        [SerializeField, Range(5, 10)]
        public int _scorePerEnemyKilled;
        public int CurrentScore      
        {
            get; private set;
        }

        public TMPro.TextMeshProUGUI ScoreText;

        public int EnemiesKilled {
            get; private set;
        }

        public delegate void AddScoreEvent();
        public static event AddScoreEvent OnAddScore;

        void Awake()
        {
            Instance = this;
            CurrentScore = EnemiesKilled = 0;
            UpdateScoreText();

        }

        public static void CallOnAddScore() {
            if (OnAddScore != null) {
                OnAddScore();
            }
        
        }

        public void AddScore()
        {
            CurrentScore += _scorePerEnemyKilled;
            EnemiesKilled++;
            UpdateScoreText();
        }


        void UpdateScoreText()
        {
            ScoreText.text = CurrentScore.ToString();
        }

        private void OnEnable()
        {
            OnAddScore += AddScore;
        }
        private void OnDisable()
        {
            OnAddScore -= AddScore;
        }
    }
}