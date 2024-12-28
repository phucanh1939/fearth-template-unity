using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

namespace Fearth
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ProcessingOverlay : MonoBehaviour
    {
        protected static readonly string DEFAULT_MESSAGE = "Processing";

        [Header("UI")]
        [SerializeField] protected CanvasGroup canvasGroup;
        [SerializeField] protected TextMeshProUGUI textMessage;
        [SerializeField] protected Image imageProcessing;

        [Header("Animation Config")]
        [SerializeField] protected float textAnimDelay = 0.25f;
        [SerializeField] protected float imageRotationDuration = 1.0f;

        protected Sequence showSequence = null;
        protected Sequence hideSequence = null;
        protected Coroutine textAnimationCoroutine = null;
        protected string message = "";

        protected void OnValidate()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetMessage(string messageText)
        {
            message = string.IsNullOrEmpty(messageText) ? DEFAULT_MESSAGE : messageText;
        }

        public void Show(float hideAfter = 1.0f, string message = null)
        {
            SetMessage(message);
            StopShowHideSequences();
            gameObject.SetActive(true);
            canvasGroup.alpha = 0;
            if(hideAfter > 0f)
            {
                showSequence = DOTween.Sequence();
                showSequence.Append(canvasGroup.DOFade(1, 0.1f))
                            .AppendInterval(hideAfter)
                            .AppendCallback(Hide);
                StartProcessingAnimation();
            }
            else
            {
                showSequence = DOTween.Sequence();
                showSequence.Append(canvasGroup.DOFade(1, 0.1f));
                StartProcessingAnimation();
            }

        }

        public void Hide()
        {
            StopShowHideSequences();
            hideSequence = DOTween.Sequence();
            hideSequence.Append(canvasGroup.DOFade(0, 0.1f))
                        .OnComplete(OnHideDone);
        }

        protected void OnHideDone()
        {
            StopProcessingAnimation();
            SetMessage(DEFAULT_MESSAGE);
            gameObject.SetActive(false);
        }

        protected void StopShowSequence()
        {
            if (showSequence != null && showSequence.IsActive())
            {
                showSequence.Kill();
            }
        }

        protected void StopHideSequence()
        {
            if (hideSequence != null && hideSequence.IsActive())
            {
                hideSequence.Kill();
            }
        }

        protected void StopShowHideSequences()
        {
            StopShowSequence();
            StopHideSequence();
        }

        protected void StopProcessingAnimation()
        {
            if (textAnimationCoroutine != null)
            {
                StopCoroutine(textAnimationCoroutine);
                textAnimationCoroutine = null;
            }

            // Stop any spinning animation
            imageProcessing.rectTransform.DOKill();
        }

        protected void StartProcessingAnimation()
        {
            StopProcessingAnimation(); // Ensure no other animations are running
            textAnimationCoroutine = StartCoroutine(AnimateTextMessage());
            imageProcessing.rectTransform.DORotate(Vector3.forward * -360, imageRotationDuration, RotateMode.FastBeyond360)
                                         .SetLoops(-1, LoopType.Restart)
                                         .SetEase(Ease.Linear);
        }

        protected IEnumerator AnimateTextMessage()
        {
            while (true)
            {
                textMessage.text = message + ".";
                yield return new WaitForSeconds(textAnimDelay);
                textMessage.text = message + "..";
                yield return new WaitForSeconds(textAnimDelay);
                textMessage.text = message + "...";
                yield return new WaitForSeconds(textAnimDelay);
            }
        }
    }
}
