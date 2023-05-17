using BrickBreaker.Core;
using UnityEngine;

namespace BrickBreaker.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Paddle : MonoBehaviour
    {
        public float force = 2f;
        public bool isPlayer = true;
        private Rigidbody2D _rb;
        private Vector2 _movement;
        private readonly float _maxBounceAngle = 75;

        private Ball _ball;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _ball = FindObjectOfType<Ball>();
        }

        private void Update()
        {
            if (!isPlayer)
            {
                Vector3 pos = transform.position;
                pos = new Vector3(_ball.transform.position.x, pos.y, pos.z);
                transform.position = pos;
            }
            _movement = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        }

        private void FixedUpdate()
        {
            if(_movement.sqrMagnitude != 0) _rb.AddForce(_movement * force);
        }

        public void ResetPaddle()
        {
            _rb.velocity = Vector2.zero;
            transform.position = LevelManager.Instance.playerResetPosition;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Ball ball = other.gameObject.GetComponent<Ball>();

            if (ball)
            {
                Vector3 paddlePosition = transform.position;
                Vector2 contactPoint = other.GetContact(0).point;

                float offset = paddlePosition.x - contactPoint.x;
                float width = other.otherCollider.bounds.size.x/2;

                float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rb.velocity);
                float bounceAngle = (offset / width) * _maxBounceAngle;
                float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -_maxBounceAngle, _maxBounceAngle);

                Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
                ball.rb.velocity = rotation * Vector3.up * ball.rb.velocity.magnitude;
            }
        }
    }
}
