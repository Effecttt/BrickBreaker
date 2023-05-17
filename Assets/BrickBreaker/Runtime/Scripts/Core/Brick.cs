using System;
using AudioSystem.Scripts;
using BrickBreaker.Player;
using UnityEngine;
using Utils.PopUp;

namespace BrickBreaker.Core
{
    public class Brick : MonoBehaviour
    {
        public int life = 1;
        [SerializeField] private Sprite[] bricks;
        [SerializeField] private Sprite unbreakable;

        public static event Action BrickBroke;
        private Color[] _brickColors;
        public int Points => 100 * life;
        public Color CurrentColor => _brickColors[life-1];

        [field: SerializeField] public bool Unbreakable { get; private set; } = false;

        private int _lifeCache;
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _brickColors = new[]
            {
                Color.cyan, Color.green, Color.yellow, new Color(255, 89, 0, 255), Color.red
            };
            TextPopUp.Create(Vector3.zero, "");
        }

        private void Start()
        {
            _lifeCache = life;
            UpdateBrick();
        }

        private void Update()
        {
            if(_lifeCache != life) UpdateBrick();
        }

        void UpdateBrick()
        {
            if (Unbreakable)
            {
                _renderer.sprite = unbreakable;
                return;
            }
            if(life <= 0) return;
            _lifeCache = life;
            _renderer.sprite = bricks[life - 1];
        }

        void Damage()
        {
            if(Unbreakable) return;
            GameManager.Instance.Hit(this);
            life--;
            if (life <= 0)
            {
                gameObject.SetActive(false);
                AudioManager.Instance.Play("DestroyBrick");
                BrickBroke?.Invoke();
                return;
            }
            AudioManager.Instance.Play("Point");
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Ball ball))
            {
                Damage();
            }
        }
    }
}