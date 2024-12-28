using UnityEngine;

using System.Collections.Generic;
using UnityEngine.Events;
using System;

namespace Fearth
{
    public class LocalizationManager : MonoBehaviour
    {
        public static readonly string LANGUAGE_CODE_DEFAULT = "EN";
        public static readonly string LANGUAGE_CODE_CACHE_KEY = "CK_LANGUAGE_CODE";
        protected static LocalizationManager instance = null;

        [SerializeField]
        protected List<LocalizationAssetConfig> localizationAssets = null;

        protected LocalizationConfig config = new();
        protected LanaguageCode currentLanguageCode;
        protected UnityAction<LanaguageCode> onLanguageChanged = null;
        public static LocalizationManager Instance => instance;

        protected void Awake()
        {
            instance = this;
        }

        public void Init()
        {
            var codeStr = PlayerPrefs.GetString(LANGUAGE_CODE_CACHE_KEY, LANGUAGE_CODE_DEFAULT);
            if (Enum.TryParse(codeStr, out LanaguageCode code)) {
                ChangeLanguage(code);
            }

        }

        public bool ChangeLanguage(LanaguageCode languageCode)
        {
            if (currentLanguageCode == languageCode)
            {
                Debug.LogWarning($"[LocalizationManager] <ChangeLanguage> Language {languageCode} already active!!!");
                return false;
            }
            Debug.Log($"[LocalizationManager] <ChangeLanguage> Change language to {languageCode}");
            var textAsset = GetTextAsset(languageCode);
            if (!textAsset)
            {
                Debug.LogError($"[LocalizationManager] <ChangeLanguage> Localization file for language {languageCode} not found!!!");
                return false;
            }
            config = JsonUtility.FromJson<LocalizationConfig>(textAsset.text);
            currentLanguageCode = languageCode;
            CacheLanguageCode();
            onLanguageChanged?.Invoke(languageCode);
            return true;
        }

        /// <summary>
        /// Get text for a key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetText(string key)
        {
            return config.GetText(key);
        }

        public string GetMessageWithErrorCode(string key, int errorCode)
        {
            var message = GetText(key);
            return message + "\n" + "(" + errorCode + ")";
        }

        public void AddLanguageChangedListener(UnityAction<LanaguageCode> listener)
        {
            onLanguageChanged += listener;
        }

        public void RemoveLanguageChangedListener(UnityAction<LanaguageCode> listener)
        {
            onLanguageChanged -= listener;
        }

        protected TextAsset GetTextAsset(LanaguageCode languageCode)
        {
            var asset = localizationAssets.Find(asset => asset.GetLanguageCode() == languageCode);
            return asset?.GetLanguageFile();
        }

        protected void CacheLanguageCode()
        {
            PlayerPrefs.SetString(LANGUAGE_CODE_CACHE_KEY, currentLanguageCode.ToString());
            PlayerPrefs.Save();
        }
    }
}
