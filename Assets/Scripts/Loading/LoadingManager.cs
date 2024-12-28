using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Events;

namespace Fearth
{
    /// <summary>
    /// Singleton Manager Class
    /// Manage game scenes: load/unload/ativate scenes, etc.
    /// </summary>
    public class LoadingManager : MonoBehaviour
    {
        [SerializeField] protected LoadingScreen loadingScreen = null;

        protected static LoadingManager instance = null;
        public static LoadingManager Instance => instance;

        protected string currentSceneName;
        protected string nextSceneName;
        protected bool isLoading = false;
        protected bool isLoadingScreenVisible = false;
        protected bool isActivateWhenFinishLoading = false;
        protected bool isNextSceneLoaded = false;
        protected AsyncOperation asyncOperation = null;
        protected UnityAction finishCallback = null;
        protected UnityAction<float> progressCallback = null;

        protected void Awake()
        {
            instance = this;
            SetLoadingScreenVisible(false);
        }

        public bool LoadScene(SceneName sceneName, bool isShowLoadingScreen = true, bool isActivateWhenFinish = true, UnityAction onFinished = null, UnityAction<float> onProgress = null)
        {
            return LoadScene(sceneName.ToString(), isShowLoadingScreen, isActivateWhenFinish, onFinished, onProgress);
        }

        /// <summary>
        /// Load a scene with name
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="isShowLoadingScreen"></param>
        /// <param name="isActivateWhenFinish"></param>
        /// <param name="onFinished"></param>
        /// <param name="onProgress"></param>
        /// <returns></returns>
        public bool LoadScene(string sceneName, bool isShowLoadingScreen = true, bool isActivateWhenFinish = true, UnityAction onFinished = null, UnityAction<float> onProgress = null)
        {
            Debug.Log($"[LoadingManager] <LoadScene> sceneName = {sceneName}, isShowLoadingScreen = {isShowLoadingScreen}, isActivateWhenFinish = {isActivateWhenFinish}");
            if (currentSceneName == sceneName) return false;
            if (isLoading) return false;
            isLoading = true;
            isNextSceneLoaded = false;
            nextSceneName = sceneName;
            finishCallback = onFinished;
            progressCallback = onProgress;
            isActivateWhenFinishLoading = isActivateWhenFinish;
            SetLoadingScreenVisible(isShowLoadingScreen);
            StartCoroutine(LoadNextSceneAsync());
            return true;
        }

        /// <summary>
        /// Activate next scene if it loaded
        /// </summary>
        /// <returns>true if activate successfully, otherwise false</returns>
        public bool ActivateNextScene()
        {
            Debug.Log($"[LoadingManager] <ActivateNextScene> {nextSceneName}");
            if (!isNextSceneLoaded) return false;
            if (asyncOperation == null) return false;
            if (isLoadingScreenVisible)
            {
                SetLoadingScreenVisible(false);
            }
            currentSceneName = nextSceneName;
            nextSceneName = null;
            asyncOperation.allowSceneActivation = true;
            return true;
        }

        protected void SetLoadingScreenVisible(bool visible)
        {
            isLoadingScreenVisible = visible;
            loadingScreen.gameObject.SetActive(visible);
        }

        protected IEnumerator LoadNextSceneAsync()
        {
            asyncOperation = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Single);
            asyncOperation.allowSceneActivation = false;
            // While not finish load scene assets
            while (asyncOperation.progress < 0.9f)
            {
                var progress = asyncOperation.progress;
                if (isLoadingScreenVisible)
                {
                    loadingScreen.SetProgress(progress);
                }
                progressCallback?.Invoke(progress);
                yield return null;
            }
            OnNextSceneLoaded();
        }

        protected void OnNextSceneLoaded()
        {
            Debug.Log($"[LoadingManager] <OnNextSceneLoaded> nextSceneId = {nextSceneName}");
            isLoading = false;
            isNextSceneLoaded = true;
            finishCallback?.Invoke();
            finishCallback = null;
            progressCallback = null;
            if (isActivateWhenFinishLoading)
            {
                ActivateNextScene();
            }
        }
    }
}