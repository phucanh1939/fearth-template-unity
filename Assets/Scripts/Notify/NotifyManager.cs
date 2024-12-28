using UnityEngine;

using UnityEngine.Events;

namespace Fearth
{
    public class NotifyManager : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] protected PopupNotify popupNotifyPrefab = null;
        [SerializeField] protected PopupYesNo popupYesNoPrefab = null;

        [Header("UI")]
        [SerializeField] protected Transform popupRoot = null;
        [SerializeField] protected Toast toast = null;
        [SerializeField] protected ProcessingOverlay processingOverlay = null;

        protected static NotifyManager instance;
        public static NotifyManager Instance => instance;

        protected void Awake()
        {
            instance = this;
        }

        public PopupNotify ShowPopupNotify(string message)
        {
            var popup = Instantiate(popupNotifyPrefab, popupRoot);
            popup.SetMessage(message);
            popup.Show();
            return popup;
        }

        public PopupYesNo ShowPopupYesNo(string message, UnityAction yesCallback = null, UnityAction noCallback = null)
        {
            var popup = Instantiate(popupYesNoPrefab, popupRoot);
            popup.SetMessage(message);
            popup.SetYesCallback(yesCallback);
            popup.SetNoCallback(noCallback);
            popup.Show();
            return popup;
        }

        public void ShowToast(string message, float hideAfter = 1.0f)
        {
            toast.Show(message, hideAfter);
        }

        public void ShowProcessingOverlay(float hideAfter = 1.0f, string message = null)
        {
            processingOverlay.Show(hideAfter, message);
        }

        public void HideProcessingOverlay()
        {
            processingOverlay.Hide();
        }
    }
}

