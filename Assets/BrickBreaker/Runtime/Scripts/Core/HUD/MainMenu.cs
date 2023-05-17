using TMPro;
using UnityEngine;

namespace BrickBreaker.Core.HUD
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Text highScore;

        private void Start()
        {
            highScore.text = PlayerPrefs.GetInt("highScore").ToString();
        }

        public void ManyPopups(bool b)
        {
            GameManager.Instance.manyPopups = b;
        }
        
        public void Audio(bool b)
        {
            GameManager.Instance.gameAudio = b;
        }

        public void PlayGame()
        {
            GameManager.Instance.NewGame(2);
        }
    }
}
