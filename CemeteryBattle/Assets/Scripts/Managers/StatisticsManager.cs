using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace PainfulTest.Manager
{
    public class StatisticsManager : MonoBehaviour
    {
        public static StatisticsManager Instance;

        Animator _anim;

        private const string _triggerOn = "On";

        [Header("Text Components")]
        [Space(5)]


        [SerializeField] private TMPro.TextMeshProUGUI _gameOverLabel;
        [SerializeField] private TMPro.TextMeshProUGUI _scoreText;
        [SerializeField] private TMPro.TextMeshProUGUI _enemiesKilledText;
        [SerializeField] private TMPro.TextMeshProUGUI _highScoreText;

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
                _gameOverLabel.text = "Time's up!";
            }
            if (Player.PlayerHealth.Instance.CurrentHealth <= 0) {
                _gameOverLabel.text = "You died!";
            }

            _scoreText.text = PainfulTest.Manager.ScoreManager.Instance.CurrentScore.ToString();
            _enemiesKilledText.text= PainfulTest.Manager.ScoreManager.Instance.EnemiesKilled.ToString();
            if (!PlayerPrefs.HasKey("HighScore"))
            {
                _highScoreText.text = PainfulTest.Manager.ScoreManager.Instance.CurrentScore.ToString();
                PlayerPrefs.SetInt("HighScore", PainfulTest.Manager.ScoreManager.Instance.CurrentScore);
            }
            else {
                if (PainfulTest.Manager.ScoreManager.Instance.CurrentScore > PlayerPrefs.GetInt("HighScore"))
                {
                    PlayerPrefs.SetInt("HighScore", PainfulTest.Manager.ScoreManager.Instance.CurrentScore);
                }
                    _highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
               
            }

            _anim.SetTrigger(_triggerOn);
        }
    }
}