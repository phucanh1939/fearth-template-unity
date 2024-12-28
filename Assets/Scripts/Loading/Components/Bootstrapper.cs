using System;
using System.Collections;
using UnityEngine;

namespace Fearth
{
    /// <summary>
    /// This component will execute some boostrap loading task 
    /// When:
    ///  - After user login to the game
    ///  - And before actually playing the game
    /// </summary>
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] protected SceneName nextScene = SceneName.MenuScene;
        [SerializeField] protected LoadingScreen loadingScreen = null;

        protected float currentProgress = 0.00f;
        protected bool isNextSceneLoaded = false;

        protected void LoadCache() { }
        protected void LoadConfigs() { }
        protected void RequestServerData() { }
        protected bool HasServerData() { return true; }

        protected void Start()
        {
            StartCoroutine(Load());
        }

        protected IEnumerator LoadTo(float targetProgress)
        {
            while (currentProgress < targetProgress)
            {
                currentProgress += Time.deltaTime;
                currentProgress = Math.Min(currentProgress, targetProgress);
                loadingScreen.SetProgress(currentProgress);
                yield return null;
            }
        }

        protected IEnumerator Load()
        {
            // Init
            currentProgress = 0.00f;
            isNextSceneLoaded = false;

            // Start loading cache at 0%
            LoadCache();

            // Start loading configs at 25%
            yield return StartCoroutine(LoadTo(0.25f));
            LoadConfigs();
            
            // Start requesting server data and loading next scene at 50%
            yield return StartCoroutine(LoadTo(0.50f));
            RequestServerData();
            LoadNextScene();

            // Start checking for server data and next scene loading finished at 90%
            yield return StartCoroutine(LoadTo(0.90f));
            yield return new WaitUntil(() => HasServerData() && IsNextSceneLoaded());

            // Finish loading process
            yield return StartCoroutine(LoadTo(1.00f));
            ActivateNextScene();
        }

        protected void LoadNextScene()
        {
            LoadingManager.Instance.LoadScene(nextScene);
        }

        protected void ActivateNextScene()
        {
            LoadingManager.Instance.ActivateNextScene();
        }

        protected bool IsNextSceneLoaded()
        {
            return isNextSceneLoaded;
        }

        protected void OnNextSceneLoaded()
        {
            isNextSceneLoaded = true;
        }

        protected void OnNextScenceLoadingProgress(float progress)
        {

        }
    }

}