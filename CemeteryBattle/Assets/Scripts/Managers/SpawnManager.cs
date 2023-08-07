using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;


namespace PainfulTest.Manager
{

    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance;

        [SerializeField] private List<Enemy.Enemy> _enemies = new List<Enemy.Enemy>();

        [SerializeField] private List<Enemy.Enemy> EnemiesInstantiated = new List<Enemy.Enemy>();

        [SerializeField] private int _maxEnemiesInstantiated;
        private int _currentEnemiesInstantiated;

        [SerializeField] private float _instantiateTimer;


        [SerializeField, Range(3, 10)]
        private float StartConterTime;
        private float _startTimer;

        [SerializeField] private List<GameObject> _spawnPoints = new List<GameObject>();

        public static UnityEvent RemoveEnemy;

        public TMPro.TextMeshProUGUI CountDownTimer;

        private bool CanSpawn;


        void Awake()
        {
            Instance = this;

            _startTimer = StartConterTime;

            CanSpawn = false;

            if (RemoveEnemy == null)
            {
                RemoveEnemy = new UnityEvent();
            }
            RemoveEnemy.AddListener(EnemyRemoved);
        }

        private void Start()
        {
            StartCoroutine(SpawnEnemies());
        }

        void Update()
        {
            if (_startTimer >= 0)
            {
                _startTimer -= Time.deltaTime;
                CountDownTimer.text = _startTimer.ToString("0");
            }
            if (_startTimer <= 0)
            {
                CountDownTimer.enabled = false;
                CanSpawn = true;
            }
           
        }
        private IEnumerator SpawnEnemies()
        {
            while (!Player.PlayerHealth.Instance.IsDead)
            {
                if (_currentEnemiesInstantiated <= _maxEnemiesInstantiated && CanSpawn)
                {
                    yield return new WaitForSeconds(_instantiateTimer);
                    Enemy.Enemy enemy = Instantiate(_enemies[Random.Range(0, _enemies.Count)],
                     _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform.position,
                     _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform.rotation);
                    EnemiesInstantiated.Add(enemy);
                    _currentEnemiesInstantiated++;
                }
                yield return null;
            }
        }

        public void EnemyRemoved()
        {
            if (_currentEnemiesInstantiated > 0)
            {
                _currentEnemiesInstantiated--;
            }
        }

        public void RemoveEnemyFromList(Enemy.Enemy enemy)
        {
            EnemiesInstantiated.Remove(enemy);
        }

        public void FreezeAllEnemies()
        {
            foreach (Enemy.Enemy enemy in EnemiesInstantiated)
            {
                enemy.CanAttackPlayerSetter(false);
                enemy.CanFollowPlayerSetter(false);
            }

        }
    }
}

