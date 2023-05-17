using System;
using System.Collections;
using AudioSystem.Scripts;
using BrickBreaker.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.PopUp;

namespace BrickBreaker.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public event Action ScoreChange;
        public int level = 1;
        public int score = 0;
        public int lives = 3;

        public MultiplierManager Multiplier { get; private set; }

        public bool manyPopups = false;
        public bool gameAudio = true;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            Multiplier = GetComponent<MultiplierManager>();
        }

        private void OnEnable() => LevelManager.WinGame += LevelManagerOnWinGame;

        private void OnDisable() => LevelManager.WinGame -= LevelManagerOnWinGame;
        
        public void Start()
        {
            Multiplier.ResetProgress(false);
            SceneManager.LoadScene("Menu");
        }

        public void NewGame(int l)
        {
            score = 0;
            lives = 3;
            LoadLevel(l);
        }

        void LoadLevel(int l)
        {
            level = l;
            SceneManager.LoadScene(l);
        }

        public void Hit(Brick brick)
        {
            score += brick.Points * Multiplier.Multiplier;
            
            //brick
            Vector3 position = brick.transform.position;
            int points = brick.Points;
            Color color = brick.CurrentColor;
            
            if(manyPopups)StartCoroutine(ShowPoints(position, points, color));
            else TextPopUp.Create(brick.transform, "+"+points * Multiplier.Multiplier, color, .1f);
            Multiplier.AddProgress(25);
            ScoreChange?.Invoke();
        }
        
        public void Miss()
        {
            lives--;
            AudioManager.Instance.Play("Hurt");
            FindObjectOfType<Ball>().ResetBall();
            FindObjectOfType<Paddle>().ResetPaddle();
            Multiplier.ResetProgress(true);
            if (lives <= 0)
            {
                NewGame(level);
            }
        }
        
        private void LevelManagerOnWinGame()
        {
            if(PlayerPrefs.GetInt("highScore") < score) PlayerPrefs.SetInt("highScore", score);
            NewGame(FindObjectOfType<LevelManager>().nextSceneId);
        }

        IEnumerator ShowPoints(Vector3 pos, int points, Color color)
        {
            int multiplier = Multiplier.Multiplier;
            while (multiplier > 0)
            {
                TextPopUp.Create(pos, "+"+points, color, .1f);
                yield return new WaitForSeconds(0.2f);
                multiplier--;
            }
        }
    }
}
