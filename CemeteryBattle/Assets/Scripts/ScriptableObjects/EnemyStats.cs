using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PainfulTest.Enemy
{
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 1)]
    public class EnemyStats : ScriptableObject
    {
        [Header("Parameters")]
        [SerializeField] private float _cooldownAttack;
        public float CooldownAttack => _cooldownAttack;

        [SerializeField] private float _maxDistance;
        public float MaxDistance => _maxDistance;


        [SerializeField, Range(5, 10)]
        private int _maxLife = 5;
        public int MaxLife => _maxLife;
        [SerializeField, Range(1, 10)]
        private int _playerArrowDamage = 1;
        public int PlayerArrowDamage => _playerArrowDamage;

        [SerializeField, Range(0.0f, 100.0f)]
        protected float _percentageChanceToDropItem;
        public float PercentageChanceToDropItem => _percentageChanceToDropItem;


        #region AudioClips
        [Header("Audio Clips")]
        [Space(5)]
        [SerializeField] private AudioClip[] _attackSound;
        public AudioClip[] AttackSound => _attackSound;
        [SerializeField] private AudioClip[] _deathSound;
        public AudioClip[] DeathSound => _deathSound;
        [SerializeField] private AudioClip[] _damageSounds;
        public AudioClip[] DamageSounds => _damageSounds;
        #endregion


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlayRandomSFX(AudioSource source, AudioClip[] list) {
            int randIndex = Random.Range(0, list.Length);
            if (!source.isPlaying)
                source.PlayOneShot(list[randIndex]);
        }
    }
}
