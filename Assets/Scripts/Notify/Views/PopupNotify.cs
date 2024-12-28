
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Fearth
{
    public class PopupNotify : PopupBase
    {
        [Header("Notify UI")]
        [SerializeField] protected TextMeshProUGUI textHeader = null;
        [SerializeField] protected TextMeshProUGUI textMessage = null;
        [SerializeField] protected TextMeshProUGUI textOkay = null;
        [SerializeField] protected Button buttonOkay = null;

        protected UnityAction okayCallback = null;

        protected override void Awake()
        {
            base.Awake();
            buttonOkay.onClick.AddListener(OnButtonOkayPressed);
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

        public void SetTextOkay(string text)
        {
            textOkay.text = text;
        }

        public void SetOkayCallback(UnityAction callback)
        {
            okayCallback = callback;
        }

        protected void OnButtonOkayPressed()
        {
            okayCallback?.Invoke();
            Hide();
        }
    }
}
