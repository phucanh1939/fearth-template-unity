using System;
using System.Collections.Generic;

namespace Fearth
{
    [Serializable]
    public class LocalizationConfig
    {
        public Dictionary<string, string> texts = null;

        public string GetText(string key)
        {
            return texts.GetValueOrDefault(key, key);
        }
    }
}