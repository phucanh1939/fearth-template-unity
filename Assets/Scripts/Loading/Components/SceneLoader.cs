
using UnityEngine;

namespace Fearth
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] protected SceneName sceneName = SceneName.LoadingScene;
        [SerializeField] protected bool showLoadingScreen = true;

        protected void Start()
        {
            LoadingManager.Instance.LoadScene(sceneName.ToString(), showLoadingScreen);
        }
    }
}