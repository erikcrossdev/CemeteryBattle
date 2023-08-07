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
        }

        protected override void Update()
        {
            base.Update();
            if (CanAttackPlayer)
                AttackPlayer();
        }

        public void Hit()
        {
            Vector3 directionToPlayer = transform.position - Target.position;
            float Angle = Vector3.Angle(transform.position, directionToPlayer);
            float distance = directionToPlayer.magnitude;
            if (Mathf.Abs(Angle) <= 90 || distance <= 90)
            {
                Player.PlayerHealth.TakePlayerDamage.Invoke();
            }
            _source.PlayOneShot(AttackSound);
        }

        void AttackPlayer()
        {
            if (_distanceBetweenTarget <= _agent.stoppingDistance)
            {
                CooldownTimer += Time.deltaTime;
            }

            if (CooldownTimer >= CooldownAttack)
            {
                _anim.SetTrigger(_triggerAttack);
                transform.LookAt(Target);
                CooldownTimer = 0;
            }
        }
    }
}