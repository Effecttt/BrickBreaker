using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BrickBreaker.Core.HUD
{
    public class Multiplier : MonoBehaviour
    {
        [SerializeField] private TMP_Text multiplier;
        [SerializeField] private Image progress;
        [SerializeField] private Image timer;

        private void Start()
        {
            timer.enabled = false;
        }

        private void OnEnable()
        {
            GameManager.Instance.Multiplier.OnMultiplierChange += OnMultiplierChange;
        }

        private void OnDisable()
        {
            GameManager.Instance.Multiplier.OnMultiplierChange -= OnMultiplierChange;
        }

        private void Update()
        {
            progress.fillAmount = GameManager.Instance.Multiplier.CurrentProgress / GameManager.Instance.Multiplier.MaxProgress;
            float value = GameManager.Instance.Multiplier.Timer / GameManager.Instance.Multiplier.MaxTimer;
            timer.fillAmount = value;
            timer.color = new Color(255,255,255, 255*value);
        }

        private void OnMultiplierChange()
        {
            float value = GameManager.Instance.Multiplier.Multiplier;
            timer.enabled = value != 1f;
            multiplier.text = value+"x";
        }
    }
}