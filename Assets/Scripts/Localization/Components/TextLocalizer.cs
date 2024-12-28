using TMPro;
using UnityEngine;

namespace Fearth
{
    public class TextLocalizer : MonoBehaviour
    {
        [SerializeField]
        protected string key = null;

        [SerializeField]
        protected TextMeshProUGUI text = null;

        protected void OnValidate()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        protected void OnEnable()
        {
            LocalizationManager.Instance.AddLanguageChangedListener(OnLanguageChanaged);
            ReloadText();
        }

        protected void OnDisable()
        {
            LocalizationManager.Instance.RemoveLanguageChangedListener(OnLanguageChanaged);
        }

        protected void OnLanguageChanaged(LanaguageCode languageCode)
        {
            ReloadText();
        }

        protected void ReloadText()
        {
            text.text = LocalizationManager.Instance.GetText(key);
        }
    }
}