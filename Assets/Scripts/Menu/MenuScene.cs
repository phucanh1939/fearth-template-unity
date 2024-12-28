using UnityEngine;
using UnityEngine.UI;

namespace Fearth
{
    public class MenuScene : MonoBehaviour
    {
        [SerializeField] protected Button buttonPlay;

        protected void Awake()
        {
            buttonPlay.onClick.AddListener(OnPlayPressed);
        }

        protected void OnPlayPressed()
        {
            LoadingManager.Instance.LoadScene(SceneName.GameScene, true);
        }
    }
}