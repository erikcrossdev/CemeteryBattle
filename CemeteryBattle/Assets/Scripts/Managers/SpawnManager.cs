using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace PainfulTest.Manager
{

    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance;

        public List<Enemy.Enemy> Enemies = new List<Enemy.Enemy>();

        public List<Enemy.Enemy> EnemiesInstantiated = new List<Enemy.Enemy>();

        public int MaxEnemiesInstantiated;
        private int _currentEnemiesInstantiated;

        public float InstantiateTimer;
        private float _timer;


        [Range(3, 10)]
        public float StartConterTime;
        private float _startTimer;

        public List<GameObject> SpawnPoints = new List<GameObject>();

        public static UnityEvent RemoveEnemy;

        public Text CountDownTimer;

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

            if (CanSpawn)
            {
                _timer += Time.deltaTime;
            } 
            if (_timer >= InstantiateTimer)
            {
                if (_currentEnemiesInstantiated <= MaxEnemiesInstantiated && !Player.PlayerHealth.Instance.IsDead)
                {
                    Enemy.Enemy enemy = Instantiate(Enemies[Random.Range(0, Enemies.Count)],
                    SpawnPoints[Random.Range(0, SpawnPoints.Count)].transform.position,
                    SpawnPoints[Random.Range(0, SpawnPoints.Count)].transform.rotation);
                    EnemiesInstantiated.Add(enemy);
                    _currentEnemiesInstantiated++;
                }
                _timer = 0;
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
                enemy.CanAttackPlayer = false;
                enemy.CanFollowPlayer = false;
            }

        }
    }
}

