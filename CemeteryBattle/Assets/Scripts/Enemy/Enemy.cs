using System.Collections;
using System.Collections.Generic;
using PainfulTest.Characters;
using UnityEngine;
using UnityEngine.AI;

namespace PainfulTest.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Animator))]
    public class Enemy : MonoBehaviour, Characters.IDamageable
    {
        public GameObject EnemyMesh;
        public GameObject Particle;
        public List<GameObject> ItemsToDrop = new List<GameObject>();
        [Range(0.0f, 100.0f)]
        public float PercentageChanceToDropItem;

        public Transform Target;
        protected Animator _anim;

        #region AudioClips
        [Header("Audio Clips")]
        [Space(5)]
        public AudioClip AttackSound;
        public AudioClip DeathSound;
        public AudioClip[] DamageSounds;
        #endregion

        #region Attack
        [Header("Attack Parameters")]
        public float CooldownAttack;
        public bool CanFollowPlayer;
        public bool CanAttackPlayer;
        #endregion

        public float MaxDistance;
        protected Vector3 _destination;
        protected NavMeshAgent _agent;
        protected AudioSource _source;

        #region Events

        public delegate void EnemyDeath();
        public static event EnemyDeath OnEnemyDeath;

        #endregion


        [Range(5, 10)]
        public int MaxLife = 5;

        protected int _currentLife;

        [Range(5, 10)]
        public int PlayerArrowDamage = 1;


        protected float WalkSpeed;
        protected float _distanceBetweenTarget;
        protected float CooldownTimer;
        protected bool isDead;

        public float EnterTheGroundTime = 2f;
        protected float _groundTimer = 2f;

        protected const string _triggerWalk = "walk";
        protected const string _triggerHit = "hit";
        protected const string _triggerDead = "isDead";

        protected virtual void Start()
        {
            _anim = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            Target = Player.FPSCharacterController.Instance.PlayerTransform;
            _source = GetComponent<AudioSource>();
            CooldownTimer = 0;

            CanAttackPlayer = true;

            isDead = false;

            _currentLife = MaxLife;
        }

        protected virtual void Update()
        {
            FollowPlayer();
            CheckLife();
            if (isDead)
            {
                EnterTheGround();
            }
        }

        protected void FollowPlayer()
        {
            if (CanFollowPlayer && !isDead)
            {
                _distanceBetweenTarget = Vector3.Distance(transform.position, Target.position);
                if (_distanceBetweenTarget > MaxDistance && !isDead)
                {
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(Target.position, out hit, MaxDistance, ~0))
                    {
                        _destination = hit.position;
                        _agent.destination = _destination;
                    }

                    WalkSpeed = _agent.velocity.magnitude;
                    if (CooldownTimer < CooldownAttack)
                    {
                        _anim.SetFloat(_triggerWalk, WalkSpeed);
                    }
                }
            }
        }

        protected void CheckLife()
        {
            if (_currentLife <= 0 && !isDead)
            {
                isDead = true;
                CanFollowPlayer = false;
                _anim.SetBool(_triggerDead, isDead);
                _source.PlayOneShot(DeathSound);
                Manager.SpawnManager.RemoveEnemy.Invoke();
                float dropChance = Random.Range(0, 100);
                Instantiate(Particle, transform.position, transform.rotation);
                if (dropChance <= PercentageChanceToDropItem)
                {
                    Instantiate(ItemsToDrop[Random.Range(0, ItemsToDrop.Count)], new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.rotation);
                }
                Destroy(gameObject, 5);
                if (OnEnemyDeath != null)
                {
                    OnEnemyDeath();
                }
            }
        }

        public void EnterTheGround()
        {
            _groundTimer += Time.deltaTime;
            if (_groundTimer >= EnterTheGroundTime)
            {
                EnemyMesh.transform.Translate(new Vector3(0, -2 * Time.deltaTime, 0));
            }
        }

        private void OnDestroy()
        {
            Manager.SpawnManager.Instance.RemoveEnemyFromList(this);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(Settings.TagManager.PlayerArrowTag))
            {
                TakeDamage();
            }
        }

        public void TakeDamage()
        {
            if (_currentLife > 0)
            {
                _anim.SetTrigger(_triggerHit);
                _currentLife -= PlayerArrowDamage;
                int randIndex = Random.Range(0, DamageSounds.Length);
                if (!_source.isPlaying)
                    _source.PlayOneShot(DamageSounds[randIndex]);
            }
        }
    }
}
