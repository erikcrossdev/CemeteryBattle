using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PainfulTest.Enemy
{
    public class ArcherEnemy : Enemy
    {
        [SerializeField] protected GameObject _arrow;
        [SerializeField] protected GameObject _bow;
        private const string _triggerReload = "reload";

        protected override void Start()
        {
            base.Start();
            StartCoroutine(AttackPlayerRoutine());
        }

        protected override void Update()
        {
            base.Update();
        }


        //This function is called in a animation Event so the animation frame can be synced
        public void Shoot()
        {
            _stats.PlayRandomSFX(_source, _stats.AttackSound);
            Instantiate(_arrow, _bow.transform.position, _bow.transform.rotation);
        }

        private IEnumerator AttackPlayerRoutine()
        {
            while (!_isDead)
            {                
                if (_distanceBetweenTarget <= _agent.stoppingDistance)
                {
                    yield return new WaitForSeconds(_stats.CooldownAttack);
                    _anim.SetTrigger(_triggerReload);
                    _bow.transform.LookAt(_target);
                    transform.LookAt(_target);
                }
                yield return null;
            }
        }
    }
}
