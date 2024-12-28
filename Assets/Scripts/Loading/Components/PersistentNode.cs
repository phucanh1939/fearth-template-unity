using UnityEngine;

namespace Fearth
{
    /// <summary>
    /// This component will mark the game object and all its children persistent
    /// </summary>
    public class PersistentNode : MonoBehaviour
    {
        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}

