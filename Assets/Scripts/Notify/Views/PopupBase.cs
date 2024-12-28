using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

namespace Fearth
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PopupBase : MonoBehaviour
    {
        [Header("Popup Settings")]
        [SerializeField] protected bool isTapOutsideToHide = true;
        [SerializeField] protected bool isRemoveOnHide = true;

        [Header("UI References")]
        [SerializeField] protected CanvasGroup canvasGroup;
        [SerializeField] protected Button overlay = null;
        [SerializeField] protected RectTransform panelRoot = null;
        [SerializeField] protected Button buttonClose = null;

        // Callbacks
        protected UnityAction showCallback = null;
        protected UnityAction hideCallback = null;
        protected UnityAction closeCallback = null;

        // Sequences
        protected Sequence showSequence = null;
        protected Sequence hideSequence = null;

        // Flags
        protected bool isShowing = false;
        protected bool isHiding = false;

        protected virtual void OnValidate()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        protected virtual void Awake()
        {
            // Set up the close button event
            buttonClose.onClick.AddListener(OnButtonClosePressed);

            // Set up the background overlay click event if the flag is true
            if (isTapOutsideToHide)
            {
                overlay.onClick.AddListener(OnOverlayPressed);
            }
        }

        public virtual void Show()
        {
            // Check is showing
            if (isShowing || isHiding) return;
            isShowing = true;

            // Stop any previous sequence
            if (showSequence != null && showSequence.IsActive())
            {
                showSequence.Kill();
            }

            // Init state
            gameObject.SetActive(true);
            canvasGroup.alpha = 0;
            panelRoot.localScale = Vector3.zero;

            // Animation
            showSequence = DOTween.Sequence();
            showSequence.Append(canvasGroup.DOFade(1, 0.3f))
                            .Join(panelRoot.DOScale(Vector3.one, 0.3f))
                            .AppendCallback(OnShowDone);
        }

        protected virtual void OnShowDone()
        {
            showCallback?.Invoke();
            isShowing = false;
            showSequence = null;
        }

        public virtual void Hide()
        {
            // Check is showing
            if (isShowing || isHiding) return;
            isHiding = true;

            // Stop any previous sequence
            if (hideSequence != null && hideSequence.IsActive())
            {
                hideSequence.Kill();
            }

            hideSequence = DOTween.Sequence();
            hideSequence.Append(canvasGroup.DOFade(0, 0.3f))
                            .Join(panelRoot.DOScale(Vector3.zero, 0.3f))
                            .OnComplete(OnHideDone);
        }

        protected virtual void OnHideDone()
        {
            hideCallback?.Invoke();
            isHiding = false;
            hideSequence = null;
            if (isRemoveOnHide)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public virtual void SetButtonCloseVisible(bool isVisible)
        {
            buttonClose.gameObject.SetActive(isVisible);
        }

        public virtual void SetShowCallback(UnityAction callback)
        {
            showCallback = callback;
        }

        public virtual void SetHideCallback(UnityAction callback)
        {
            hideCallback = callback;
        }

        public virtual void SetCloseCallback(UnityAction callback)
        {
            closeCallback = callback;
        }

        public virtual void Close()
        {
            Hide();
            closeCallback?.Invoke();
        }

        public virtual void SetRemoveOnHide(bool removeOnHide)
        {
            isRemoveOnHide = removeOnHide;
        }

        public virtual void SetTapOutsideToHide(bool tapOutsideToHide)
        {
            isTapOutsideToHide = tapOutsideToHide;
        }

        protected virtual void OnButtonClosePressed()
        {
            Close();
        }

        protected virtual void OnOverlayPressed()
        {
            Hide();
        }
    }
}