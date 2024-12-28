using UnityEngine;
using UnityEngine.UI;

namespace Fearth
{
    public class LoginScene : MonoBehaviour
    {
        [SerializeField] protected Button buttonLogin;

        protected void Awake()
        {
            buttonLogin.onClick.AddListener(OnLoginPressed);
        }

        protected void OnLoginPressed()
        {
            LoadingManager.Instance.LoadScene(SceneName.LoadingScene);
        }
    }
}