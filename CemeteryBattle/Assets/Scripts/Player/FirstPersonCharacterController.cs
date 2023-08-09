using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PainfulTest.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonCharacterController : MonoBehaviour
    {
        [Header("Camera settings")]
        [Space(20)]
        [SerializeField] private Camera _camera;

        [SerializeField, Range(2.0f, 5.0f)]
        private float _lookSpeed;

        [SerializeField, Range(0, 180.0f)]
        private float _xLimit = 45f;

        private Vector3 _moveDirection = Vector3.zero;
        private float _movementYdirection;

        [SerializeField, Range(0, 180.0f)]
        private float _rotationX = 45f;

        [Header("Moviment settings")]
        [Space(20)]

        [SerializeField, Range(2.0f, 15.0f)]
        private float _walkSpeed;

        [SerializeField, Range(2.0f, 20.0f)]
        private float _runSpeed;

        [SerializeField, Range(2.0f, 20.0f)]
        private float _jumpForce;

        [SerializeField, Range(2.0f, 20.0f)]
        private float _gravity;

        [SerializeField] private bool _canMove = true;
        [SerializeField] private CharacterController _characterController;

        public static FirstPersonCharacterController Instance
        { get; private set; }

        private void Awake()
        {
            Instance = this;
            _characterController = GetComponent<CharacterController>();
        }

        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Movement()
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float currentSpeed = isRunning ? _runSpeed : _walkSpeed;
            float speedX = _canMove ? currentSpeed * Input.GetAxis("Vertical") : 0;
            float speedY = _canMove ? currentSpeed * Input.GetAxis("Horizontal") : 0;
            _movementYdirection = _moveDirection.y;
            _moveDirection = (forward * speedX) + (right * speedY);

        }

        private void Jump()
        {
            if (Input.GetKey(KeyCode.Space) && _canMove && _characterController.isGrounded)
            {
                _moveDirection.y = _jumpForce;
            }
            else
            {
                _moveDirection.y = _movementYdirection;
            }

            if (!_characterController.isGrounded)
            {
                _moveDirection.y -= _gravity * Time.deltaTime;
            }
        }

        private void RotateCamera()
        {
            _characterController.Move(_moveDirection * Time.deltaTime);

            if (_canMove)
            {
                _rotationX += -Input.GetAxis("Mouse Y") * _lookSpeed;
                _rotationX = Mathf.Clamp(_rotationX, -_xLimit, _xLimit);
                _camera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _lookSpeed, 0);
            }
        }

        // Update is called once per frame
        void Update()
        {
            Movement();
            Jump();
            RotateCamera();
        }
    }
}
