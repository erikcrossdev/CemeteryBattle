using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PainfulTest.Manager
{
    public class SpawnPoint : MonoBehaviour
    {

        [SerializeField] private ParticleSystem[] _particle;

        private void Awake()
        {
            _particle = GetComponentsInChildren<ParticleSystem>();
        }
        public void PlayParticle()
        {
            foreach (var particle in _particle)
            {
                particle.Play();
            }
            
        }
    }
}
