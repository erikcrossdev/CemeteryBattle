using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PainfulTest.Manager
{

    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        [Range(5, 10)]
        public int ScorePerEnemyKilled;
        public int CurrentScore      
        {
            get; private set;
        }

        public Text ScoreText;

        public int EnemiesKilled {
            get; private set;
        }

        void Awake()
        {
            Instance = this;
            CurrentScore = EnemiesKilled = 0;
            UpdateScoreText();

        }

        public void AddScore()
        {
            CurrentScore += ScorePerEnemyKilled;
            EnemiesKilled++;
            UpdateScoreText();
        }


        void UpdateScoreText()
        {
            ScoreText.text = CurrentScore.ToString();
        }

        private void OnEnable()
        {
            Enemy.Enemy.OnEnemyDeath += AddScore;
        }
        private void OnDisable()
        {
            Enemy.Enemy.OnEnemyDeath -= AddScore;
        }
    }
}