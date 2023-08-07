using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace PainfulTest.Manager
{
    public class StatisticsManager : MonoBehaviour
    {
        public static StatisticsManager Instance;

        Animator _anim;

        private const string _triggerOn = "On";

        [Header("Text Componets")]
        [Space(5)]


        public Text GameOverLabel;
        public Text ScoreText;
        public Text EnemiesKilledText;
        public Text HighScoreText;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            _anim = GetComponent<Animator>();
        }
      
        public void ShowStatistics() {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (MatchManager.Instance.Timer <= 0) {
                GameOverLabel.text = "Time's up!";
            }
            if (Player.PlayerHealth.Instance.CurrentHealth <= 0) {
                GameOverLabel.text = "You died!";
            }

            ScoreText.text = PainfulTest.Manager.ScoreManager.Instance.CurrentScore.ToString();
            EnemiesKilledText.text= PainfulTest.Manager.ScoreManager.Instance.EnemiesKilled.ToString();
            if (!PlayerPrefs.HasKey("HighScore"))
            {
                HighScoreText.text = PainfulTest.Manager.ScoreManager.Instance.CurrentScore.ToString();
                PlayerPrefs.SetInt("HighScore", PainfulTest.Manager.ScoreManager.Instance.CurrentScore);
            }
            else {
                if (PainfulTest.Manager.ScoreManager.Instance.CurrentScore > PlayerPrefs.GetInt("HighScore"))
                {
                    PlayerPrefs.SetInt("HighScore", PainfulTest.Manager.ScoreManager.Instance.CurrentScore);
                }
                    HighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
               
            }

            _anim.SetTrigger(_triggerOn);
        }
    }
}