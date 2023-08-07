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

        [Range(10.0f, 110.0f)]
        public float InitialHealth;

        [Range(1, 110)]
        public float HealthPackRecoverValue;

        public float CurrentHealth { get; private set; }

        public float MinDamage;
        public float MaxDamage;

        private float _damageAlpha;
        public bool IsDead { get; private set; }

        public Image DamageUI;

        private AudioSource _source;

        [Header("Audio Clips")]
        [Space(5)]
        public AudioClip RecoverLife;
        public AudioClip DamageClip;


        public static UnityEvent TakePlayerDamage;

        private void Awake()
        {
            Instance = this;
        }
        void Start()
        {
            _source = GetComponent<AudioSource>();
            CurrentHealth = InitialHealth;

            if (TakePlayerDamage == null)
            {
                TakePlayerDamage = new UnityEvent();
            }
            TakePlayerDamage.AddListener(TakeDamage);
            DamageUI.color = new Color(1, 1, 1, 0);
        }

        public void TakeDamage()
        {
            if (CurrentHealth > 0)
            {
                _source.PlayOneShot(DamageClip);
                float damage = Random.Range(MinDamage, MaxDamage);
                CurrentHealth -= damage;
                _damageAlpha += (damage / InitialHealth);
                DamageUI.color = new Color(1, 1, 1, _damageAlpha);
            }

            if (CurrentHealth <= 0) {
                IsDead = true;
                Manager.SpawnManager.Instance.FreezeAllEnemies();
                Manager.StatisticsManager.Instance.ShowStatistics();
            }
        }

        private void RecoverHealth(GameObject other) {
            _source.PlayOneShot(RecoverLife);
            CurrentHealth += HealthPackRecoverValue;
            _damageAlpha -= (HealthPackRecoverValue / InitialHealth);
            DamageUI.color = new Color(1, 1, 1, _damageAlpha);
           
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
