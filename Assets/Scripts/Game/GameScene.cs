using UnityEngine;
using UnityEngine.UI;

namespace Fearth
{
    public class GameScene : MonoBehaviour
    {
        [SerializeField] protected Button buttonExit;

        protected void Awake()
        {
            buttonExit.onClick.AddListener(OnExitPressed);
        }

        protected void OnExitPressed()
        {
            LoadingManager.Instance.LoadScene(SceneName.MenuScene);
        }
    }
}