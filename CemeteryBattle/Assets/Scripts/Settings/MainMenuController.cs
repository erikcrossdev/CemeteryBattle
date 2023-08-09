using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PainfulTest.Settings
{
    public class MainMenuController : MonoBehaviour
    {
        public Animator SettingsAnim;

        private const string _triggerON = "On";
        private const string _triggerOFF = "Off";


        public void BackToMainMenu()
        {
            SettingsAnim.SetTrigger(_triggerOFF);
        }

        public void SettingsMenu()
        {
            SettingsAnim.SetTrigger(_triggerON);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

    }
}
