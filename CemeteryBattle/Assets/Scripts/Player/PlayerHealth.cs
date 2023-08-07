using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PainfulTest.Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerHealth : MonoBehaviour, Characters.IDamageable
    {
        public static PlayerHealth Instance;

        [SerializeField, Range(10.0f, 110.0f)]
        private float _initialHealth;

        [SerializeField, Range(1, 110)]
        private float _healthPackRecoverValue;

        public float CurrentHealth { get; private set; }

        [SerializeField] private float _minDamage;
        [SerializeField] private float _maxDamage;

        private float _damageAlpha;
        public bool IsDead { get; private set; }

        [SerializeField] private Image _damageUI;

        private AudioSource _source;

        [Header("Audio Clips")]
        [Space(5)]
        [SerializeField] private AudioClip _recoverLife;
        [SerializeField] private AudioClip _damageClip;


        public static UnityEvent TakePlayerDamage;

        private void Awake()
        {
            Instance = this;
            _source = GetComponent<AudioSource>();
            CurrentHealth = _initialHealth;
        }
        void Start()
        {
            if (TakePlayerDamage == null)
            {
                TakePlayerDamage = new UnityEvent();
            }
            TakePlayerDamage.AddListener(TakeDamage);
            _damageUI.color = new Color(1, 1, 1, 0);
        }

        public void TakeDamage()
        {
            if (CurrentHealth > 0)
            {
                _source.PlayOneShot(_damageClip);
                float damage = Random.Range(_minDamage, _maxDamage);
                CurrentHealth -= damage;
                _damageAlpha += (damage / _initialHealth);
                _damageUI.color = new Color(1, 1, 1, _damageAlpha);
            }

            if (CurrentHealth <= 0) {
                IsDead = true;
                Manager.SpawnManager.Instance.FreezeAllEnemies();
                Manager.StatisticsManager.Instance.ShowStatistics();
            }
        }

        private void RecoverHealth(GameObject other) {
            _source.PlayOneShot(_recoverLife);
            CurrentHealth += _healthPackRecoverValue;
            _damageAlpha -= (_healthPackRecoverValue / _initialHealth);
            _damageUI.color = new Color(1, 1, 1, _damageAlpha);
           
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Items.CollectableItem>() != null && other.gameObject.GetComponent<Items.CollectableItem>().Type == Items.CollectableType.LifePack)
            {
                RecoverHealth(other.gameObject);
                Items.CollectableItem item = other.gameObject.GetComponent<Items.CollectableItem>();
                item.DestroyOnCollect();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(Settings.TagManager.EnemyArrowTag))
            {
                TakeDamage();
                Destroy(other.gameObject);
            }
        }
    }
}
