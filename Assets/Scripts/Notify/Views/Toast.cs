using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Fearth
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Toast : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup canvasGroup;
        [SerializeField] protected TextMeshProUGUI textMessage;

        protected Sequence showSequence = null;

        protected void OnValidate()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetMessage(string messageText)
        {
            textMessage.text = messageText;
        }

        public void Show(string message, float hideAfter = 1.0f)
        {
            // Set the message
            SetMessage(message);

            // Stop any previous sequence
            if (showSequence != null && showSequence.IsActive())
            {
                showSequence.Kill();
            }

            gameObject.SetActive(true);

            // Reset initial states
            canvasGroup.alpha = 0;
            transform.localScale = Vector3.zero;

            // Play fade-in and scale-up animation using DOTween
            showSequence = DOTween.Sequence();
            showSequence.Append(canvasGroup.DOFade(1, 0.3f))
                        .Join(transform.DOScale(Vector3.one, 0.3f))
                        .AppendInterval(hideAfter)
                        .AppendCallback(Hide);
        }

        protected void Hide()
        {
            // Play fade-out and scale-down animation using DOTween
            Sequence hideSequence = DOTween.Sequence();
            hideSequence.Append(canvasGroup.DOFade(0, 0.3f))
                        .Join(transform.DOScale(Vector3.zero, 0.3f))
                        .OnComplete(() => gameObject.SetActive(false));
        }
    }
}
