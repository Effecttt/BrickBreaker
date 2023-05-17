using UnityEngine;

namespace BrickBreaker.Core
{
    public class ApplyGameManagerRules : MonoBehaviour
    {
        public static ApplyGameManagerRules Rules;

        private void Awake()
        {
            Rules = this;
        }

        public void ApplyRules()
        {
            AudioRules();
        }

        public void AudioRules()
        {
            if (GameManager.Instance.gameAudio)
            {
                AudioListener.volume = 1;
            }
            else
            {
                AudioListener.volume = 0;
            }
        }
    }
}