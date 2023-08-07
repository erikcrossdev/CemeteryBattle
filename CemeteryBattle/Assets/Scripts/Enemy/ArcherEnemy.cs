using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PainfulTest.Enemy
{
    public class ArcherEnemy : Enemy
    {
        public GameObject Arrow;
        public GameObject Bow;
        private const string _triggerReload = "reload";
     
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

        public void Shoot()
        {           
            _source.PlayOneShot(AttackSound);
            Instantiate(Arrow, Bow.transform.position, Bow.transform.rotation);
        }


        void AttackPlayer()
        {
            if (_distanceBetweenTarget <= _agent.stoppingDistance)
            {
                CooldownTimer += Time.deltaTime;
            }

            if (CooldownTimer >= CooldownAttack)
            {
                _anim.SetTrigger(_triggerReload);
                Bow.transform.LookAt(Target);
                transform.LookAt(Target);
                CooldownTimer = 0;

            }
        }
    }
}