using UnityEngine;
using UnityEngine.Events;

namespace BrickBreaker.Core
{
    public class SceneLoader : MonoBehaviour
    {
        public UnityEvent onLoadScene;

        private void Start()
        {
            onLoadScene?.Invoke();
        }
    }
}
