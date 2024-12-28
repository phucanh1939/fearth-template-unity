using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fearth
{
    /// <summary>
    /// Contains all LoadingScreen (game object) behaviours
    /// Loading Screen is a simple gameobject container a loading bar, used to show loading progress
    /// </summary>
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] protected Image imageProgress = default;
        [SerializeField] protected TMP_Text textProcess = default;

        protected void Start()
        {
            SetProgress(0.0f);
        }

        public void SetProgress(float progress)
        {
            imageProgress.fillAmount = progress;
            textProcess.text = (progress * 100f).ToString("F2") + "%";
        }

        public void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}
