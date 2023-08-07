using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PainfulTest.Settings
{
    public class PauseMenu : MonoBehaviour
    {
        public static PauseMenu Instance
        {
            get; private set;
        }
      
        public GameObject PauseUI;
        public GameObject QuitSubMenu;
        public bool IsPaused
        {
            get; private set;
        }
        private void Awake()
        {
            Instance = this;
        }
        
        void Start()
        {
            QuitButtonActivation(false);
            IsPaused = false;
            PauseUI.SetActive(IsPaused);
            SetPauseActivation();
        }

       
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ResumePauseGame();
            }

            
        }

        public void ResumePauseGame()
        {
            IsPaused = !IsPaused;
            PauseUI.SetActive(IsPaused);
            SetPauseActivation();
        }


        void SetPauseActivation()
        {
            if (IsPaused)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
            }
        }

        public void QuitButtonActivation(bool activation)
        {
            QuitSubMenu.SetActive(activation);
        }

    }
}