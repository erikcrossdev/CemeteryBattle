using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PainfulTest.Manager
{
    public class SceneLoader : MonoBehaviour
    {
        public string SceneName;

        public void LoadLevel()
        {
            StartCoroutine(LoadSceneAsync());
        }

        IEnumerator LoadSceneAsync()
        {
            if (Time.timeScale != 1) { Time.timeScale = 1; }
            AsyncOperation loadAsync = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);

            while (!loadAsync.isDone)
            {
                yield return null;
            }
        }

        public void Exit()
        {
            Application.Quit();
        }

    }
}
