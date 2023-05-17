using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BrickBreaker.Core
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        
        [SerializeField] private Brick[] bricks;

        public static event Action WinGame;

        public Vector3 playerResetPosition, ballResetPosition;

        public int nextSceneId;

        private void Awake()
        {
            Instance = this;
            bricks = FindObjectsOfType<Brick>().Where(b => b.Unbreakable == false).ToArray();
        }

        private void OnEnable()
        {
            Brick.BrickBroke += BrickOnBrickBroke;
        }

        private void OnDisable()
        {
            Brick.BrickBroke -= BrickOnBrickBroke;
        }

        private void BrickOnBrickBroke()
        {
            if (bricks.All(b => b.gameObject.activeInHierarchy == false)) WinGame?.Invoke();
        }
    }
}