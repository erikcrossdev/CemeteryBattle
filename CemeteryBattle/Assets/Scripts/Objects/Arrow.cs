using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PainfulTest.Objects
{
    public class Arrow : MonoBehaviour
    {
        Rigidbody _rigidbody;
        public float Lifetime;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            _rigidbody.velocity = transform.TransformDirection(Vector3.forward * 10);

            Destroy(gameObject, Lifetime);
        }
    }
}
