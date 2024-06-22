using UnityEngine;

namespace Audio
{
    public class BackgroundMusic : MonoBehaviour
    {
        private static BackgroundMusic _instance;
        
        private void Start()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            _instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }
}