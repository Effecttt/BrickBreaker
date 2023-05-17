using TMPro;
using UnityEngine;
using Utils.PopUp;

namespace BrickBreaker.Core.HUD
{
    public class ScoreHUD : MonoBehaviour
    {
        [SerializeField] private TMP_Text score;

        private void Start()
        {
            score.text = 0.ToString();
        }

        public void MenuButton()
        {
            GameManager.Instance.Start();
        }
        
        public void AudioButton()
        {
            GameManager.Instance.gameAudio = !GameManager.Instance.gameAudio;
            TextPopUp.Create(new Vector3(-16.75f, -9.25f,0), GameManager.Instance.gameAudio? "ON":"OFF", Color.white, .2f);
            ApplyGameManagerRules.Rules.AudioRules();
        }

        private void OnEnable()
        {
            GameManager.Instance.ScoreChange += UpdateScore;
        }

        private void OnDisable()
        {
            GameManager.Instance.ScoreChange -= UpdateScore;
        }

        private void UpdateScore()
        {
            score.text = GameManager.Instance.score.ToString();
        }
    }
}
