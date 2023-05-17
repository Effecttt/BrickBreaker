using BrickBreaker.Core;
using UnityEngine;

namespace BrickBreaker.Player
{
    public class MissZone : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.TryGetComponent(out Ball b))
            {
                GameManager.Instance.Miss();
            }
        }
    }
}
