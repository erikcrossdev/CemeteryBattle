using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace PainfulTest.Enemy
{
    public class SwordEnemy : Enemy
    {
        private const string _triggerAttack = "attack";
  
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
        public void Hit()
        {
            Vector3 directionToPlayer = transform.position - _target.position;
            float Angle = Vector3.Angle(transform.position, directionToPlayer);
            float distance = directionToPlayer.magnitude;
            if (Mathf.Abs(Angle) <= 90 || distance <= 90)
            {
                Player.PlayerHealth.TakePlayerDamage.Invoke();
            }
            _stats.PlayRandomSFX(_source, _stats.AttackSound);
            _canAttackPlayer = true;
        }
        private IEnumerator AttackPlayerRoutine()
        {
            while (!_isDead)
            {
                if (_distanceBetweenTarget <= _agent.stoppingDistance)
                {
                    yield return new WaitForSeconds(_stats.CooldownAttack);
                    _anim.SetTrigger(_triggerAttack);
                    transform.LookAt(_target);
                }
                yield return null;
            }
        }

    }
}