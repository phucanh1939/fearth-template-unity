
using UnityEngine;

namespace Fearth
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] protected SceneName sceneName = SceneName.LoadingScene;

        protected void Start()
        {
            LoadCache();
            LoadConfigs();
            LoadNextScene();
        }

        protected void LoadCache() { }

        protected void LoadConfigs() { }

        protected void LoadNextScene()
        {
            LoadingManager.Instance.LoadScene(sceneName.ToString(), false);
        }
    }
}