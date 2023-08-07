using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PainfulTest.UI
{
    public class FadeInFadeOut : MonoBehaviour
    {
        private Animator _anim;

        private const string _triggerOut = "Out";
        private const string _triggerIn = "In";

        void Start()
        {
            _anim = GetComponent<Animator>();
            FadeOut();
        }

        public void FadeIn()
        {
            _anim.SetTrigger(_triggerIn);
        }

        public void FadeOut()
        {
            _anim.SetTrigger(_triggerOut);
        }

    }
}