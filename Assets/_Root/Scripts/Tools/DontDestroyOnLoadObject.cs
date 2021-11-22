using UnityEngine;

namespace Tools
{
    internal class DontDestroyOnLoadObject : MonoBehaviour
    {
        private void Awake()
        {
            if (enabled)
                DontDestroyOnLoad(gameObject);
        }
    }
}
