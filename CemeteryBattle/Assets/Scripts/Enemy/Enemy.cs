using System.Collections;
using System.Collections.Generic;
using PainfulTest.Characters;
using UnityEngine;
using UnityEngine.AI;
using PainfulTest.Manager;

namespace PainfulTest.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Animator))]
    public class Enemy : MonoBehaviour, Characters.IDamageable
    {
        [SerializeField] protected GameObject _enemyMesh;
        [SerializeField] protected GameObject _particle;
        [SerializeField] protected List<GameObject> _itemsToDrop = new List<GameObject>();
      

        protected Transform _target;
        protected Animator _anim;

        [SerializeField] protected EnemyStats _stats;


        #region Attack
        [Header("Attack Parameters")]
        [SerializeField] protected bool _canFollowPlayer;
        [SerializeField] protected bool _canAttackPlayer;
        #endregion
              
        protected Vector3 _destination;
        protected NavMeshAgent _agent;
        protected AudioSource _source;

        #region Events

        public delegate void EnemyDeath();
        private event EnemyDeath OnEnemyDeath;

        #endregion

        protected int _currentLife;
        protected float _currentSpeed;
        protected float _distanceBetweenTarget;
        protected bool _isDead;

        [SerializeField] protected float EnterTheGroundTime = 2f;
        protected float _groundTimer = 2f;

        protected const string _triggerWalk = "walk";
        protected const string _triggerHit = "hit";
        protected const string _triggerDead = "isDead";

        protected virtual void Start()
        {
            _anim = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _target = Player.FirstPersonCharacterController.Instance.transform;
            _source = GetComponent<AudioSource>();
           
            _canAttackPlayer = true;

            _isDead = false;

            _currentLife = _stats.MaxLife;
            _agent.speed = _stats.Speed;
        }

        protected virtual void Update()
        {
            FollowPlayer();
            if (_isDead)
            {
                EnterTheGround();
            }
        }

        protected virtual void FollowPlayer()
        {
            if (_canFollowPlayer && !_isDead)
            {
                _distanceBetweenTarget = Vector3.Distance(transform.position, _target.position);
                if (_distanceBetweenTarget > _stats.MaxDistance && !_isDead)
                {
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(_target.position, out hit, _stats.MaxDistance, ~0))
                    {
                        _destination = hit.position;
                        _agent.destination = _destination;
                    }

                    _currentSpeed = _agent.velocity.magnitude;
                    _anim.SetFloat(_triggerWalk, _currentSpeed);
                }
            }
        }

        protected void KillEnemy()
        {
            _isDead = true;
            _canFollowPlayer = false;
            _anim.SetBool(_triggerDead, _isDead);
            _stats.PlayRandomSFX(_source, _stats.DeathSound);
            Manager.SpawnManager.RemoveEnemy.Invoke();
            Manager.ScoreManager.CallOnAddScore();

            Destroy(gameObject, 5);           
        }

        protected void DropLoot() {
            float dropChance = Random.Range(0, 100);
            Instantiate(_particle, transform.position, transform.rotation);
            if (dropChance <= _stats.PercentageChanceToDropItem)
            {
                Instantiate(_itemsToDrop[Random.Range(0, _itemsToDrop.Count)], new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.rotation);
            }

        }

        public void EnterTheGround()
        {
            _groundTimer += Time.deltaTime;
            if (_groundTimer >= EnterTheGroundTime)
            {
                _enemyMesh.transform.Translate(new Vector3(0, -2 * Time.deltaTime, 0));
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
                Destroy(collision.gameObject, 0.1f);
            }
        }

        public void TakeDamage()
        {
            if (_currentLife > 0)
            {
                _anim.SetTrigger(_triggerHit);
                _currentLife -= _stats.PlayerArrowDamage;
                _stats.PlayRandomSFX(_source, _stats.DamageSounds);
            }
            else
            {
                CallOnEnemyDeath();
            }
        }

        private void CallOnEnemyDeath()
        {
            if (OnEnemyDeath != null)
            {
                OnEnemyDeath();
                OnEnemyDeath = null;
            }
        }

        public void CanAttackPlayerSetter(bool value) {
            _canAttackPlayer = value;
        }

        public void CanFollowPlayerSetter(bool value)
        {
            _canFollowPlayer = value;
        }

        private void OnEnable()
        {
            OnEnemyDeath += KillEnemy;
            OnEnemyDeath += DropLoot;
        }

        private void OnDisable()
        {
            OnEnemyDeath -= KillEnemy;
            OnEnemyDeath -= DropLoot;
        }


    }

}
