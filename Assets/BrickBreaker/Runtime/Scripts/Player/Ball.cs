using System.Collections;
using AudioSystem.Scripts;
using BrickBreaker.Core;
using UnityEditor;
using UnityEngine;

namespace BrickBreaker.Player
{
    public class Ball : MonoBehaviour
    {
        public float speed = 10f;
        public Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            ResetBall();
        }
        
        private void FixedUpdate()
        {
            rb.velocity = rb.velocity.normalized * speed;
        }

        private void SetBallTrajectory()
        {
            Vector2 force = Vector2.zero;
            force.x = Random.Range(-1f, 1f);
            force.y = -1;

            rb.AddForce(force * speed);
        }
        
        public void ResetBall()
        {
            rb.velocity = Vector2.zero;
            transform.position = LevelManager.Instance.ballResetPosition;

            StartCoroutine(SetBallTrajectoryRoutine());
        }

        IEnumerator SetBallTrajectoryRoutine()
        {
            yield return new WaitForSeconds(1f);
            SetBallTrajectory();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            AudioManager.Instance.Play("Hit");
        }
    }
}
