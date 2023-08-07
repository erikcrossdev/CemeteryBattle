using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PainfulTest.Player
{

    public class FPSCharacterController : MonoBehaviour
    {
        public delegate bool MoveLeft(bool isPressed);
        public static event MoveLeft OnClickLeft;

        public delegate bool MoveRight(bool isPressed);
        public static event MoveRight OnClickRight;

        public static FPSCharacterController Instance
        { get; private set; }

        [Header("Moviment settings")]
        [Space(20)]

        [Range(2.0f, 10.0f)]
        public float WalkSpeed;

        [Range(2.0f, 10.0f)]
        public float RunSpeed;

        private float _currentSpeed;

        [Range(1f, 10f)]
        public float Impulse;

        private Rigidbody _rigidbody;

        private bool _isGrounded;

        public Transform PlayerTransform;

        private void Awake()
        {
            Instance = this;
            _rigidbody = GetComponent<Rigidbody>();
            PlayerTransform = GetComponent<Transform>();
        }

        void Update()
        {
            if (!Player.PlayerHealth.Instance.IsDead && !Manager.MatchManager.Instance.TimeIsUp)
            {
                MoveCharacter();
                Jump();
            }
        }

        void MoveCharacter()
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _currentSpeed = RunSpeed;
            }
            else
            {
                _currentSpeed = WalkSpeed;
            }

            float _vertical = Input.GetAxis("Vertical") * _currentSpeed;
            float _horizontal = Input.GetAxis("Horizontal") * _currentSpeed;
            _vertical *= Time.deltaTime;
            _horizontal *= Time.deltaTime;

            transform.Translate(_horizontal, 0, _vertical);
        }

        void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                _rigidbody.AddForce(new Vector3(0, Impulse, 0), ForceMode.Impulse);
                _isGrounded = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(Settings.TagManager.TerrainTag) || collision.gameObject.CompareTag(Settings.TagManager.PropsTag))
            {
                _isGrounded = true;
            }
        }
    }
}