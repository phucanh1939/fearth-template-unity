
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Fearth
{
    public class PopupYesNo : PopupBase
    {
        [Header("Yes/No UI")]
        [SerializeField] protected TextMeshProUGUI textHeader = null;
        [SerializeField] protected TextMeshProUGUI textMessage = null;
        [SerializeField] protected TextMeshProUGUI textYes = null;
        [SerializeField] protected TextMeshProUGUI textNo = null;
        [SerializeField] protected Button buttonYes = null;
        [SerializeField] protected Button buttonNo = null;

        protected UnityAction yesCallback = null;
        protected UnityAction noCallback = null;

        protected override void Awake()
        {
            base.Awake();
            buttonYes.onClick.AddListener(OnButtonYesPressed);
            buttonNo.onClick.AddListener(OnButtonNoPressed);
        }

        public void SetHeader(string header)
        {
            if (textHeader)
            {
                textHeader.text = header;
            }
        }

        public void SetMessage(string message)
        {
            textMessage.text = message;
        }

        public void SetTextYes(string text)
        {
            textYes.text = text;
        }

        public void SetTextNo(string text)
        {
            textNo.text = text;
        }

        public void SetYesCallback(UnityAction callback)
        {
            yesCallback = callback;
        }

        public void SetNoCallback(UnityAction callback)
        {
            noCallback = callback;
        }

        protected void OnButtonYesPressed()
        {
            yesCallback?.Invoke();
            Hide();
        }

        protected void OnButtonNoPressed()
        {
            noCallback?.Invoke();
            Hide();
        }
    }

}