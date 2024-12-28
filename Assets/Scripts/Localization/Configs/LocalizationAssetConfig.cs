using System;
using UnityEngine;

namespace Fearth
{
    [Serializable]
    public class LocalizationAssetConfig
    {
        [SerializeField] protected LanaguageCode languageCode;
        [SerializeField] protected TextAsset languageFile;

        public LanaguageCode GetLanguageCode() => languageCode;
        public TextAsset GetLanguageFile() => languageFile;
    }
}