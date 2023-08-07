using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PainfulTest.Player
{
    public class FPSCamera : MonoBehaviour
    {
        Vector2 _mouseLook;
        Vector2 _smoothVelocity;

        [Range(1.0f, 50.0f)]
        public float Sensivity = 4.0f;
        [Range(1.0f, 50.0f)]
        public float Smooth = 0.5f;

        public float minimumY;
        public float maximumY;

        GameObject Character;

        void Start()
        {
            Character = this.transform.parent.gameObject;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            if (!Player.PlayerHealth.Instance.IsDead && !Manager.MatchManager.Instance.TimeIsUp)
            {
                MouseLook();
            }
        }

        void MouseLook()
        {
            var _axis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            _axis = Vector2.Scale(_axis, new Vector2(Sensivity * Smooth, Sensivity * Smooth)) * Time.deltaTime;
            _smoothVelocity.x = Mathf.Lerp(_smoothVelocity.x, _axis.x, 1f / Smooth); //X axis
            _smoothVelocity.y = Mathf.Lerp(_smoothVelocity.y, _axis.y, 1f / Smooth); //Y axis
            _mouseLook += _smoothVelocity;

            if (-_mouseLook.y < minimumY && -_mouseLook.y > maximumY)
            {
                transform.localRotation = Quaternion.AngleAxis(-_mouseLook.y, Vector3.right); //Rotation using angle degrees in Y axis
            }
            Character.transform.localRotation = Quaternion.AngleAxis(_mouseLook.x, Character.transform.up); //Rotate the character using angle degrees in X axis
        }
    }
}
