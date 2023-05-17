using System;
using System.Collections;
using AudioSystem.Scripts;
using UnityEngine;

namespace BrickBreaker.Core
{
    public class MultiplierManager : MonoBehaviour
    {
        public int Multiplier { get; private set; } = 1;
        public float CurrentProgress { get; private set; } = 0;
        public float MaxProgress => _baseProgress * 3;
        public event Action OnMultiplierChange;
        
        private readonly int _baseProgress = 25;

        public float Timer { get; private set; } = 2f;
        public float MaxTimer { get; private set; } = 2f;

        private void Start()
        {
            print($"Multiplier = {Multiplier}");
        }

        private void Update()
        {
            if (Timer > 0) Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                ResetProgress(true);
                Timer = 0;
            }

            if (CurrentProgress >= MaxProgress)
            {
                Upgrade(true);
            }
        }

        public void ResetProgress(bool sound)
        {
            if(Multiplier > 1 && sound) AudioManager.Instance.Play("MultiplierReset");
            Multiplier = 1;
            CurrentProgress = 0;
            OnMultiplierChange?.Invoke();
            print("reset");
        }

        void Upgrade(bool sound)
        {
            Multiplier++;
            CurrentProgress = 0;
            OnMultiplierChange?.Invoke();
            if(sound)AudioManager.Instance.Play("MultiplierUpgrade");
            print($"Multiplier = {Multiplier}");
        }

        public void AddProgress(float amount)
        {
            float time = 2f + .25f * Multiplier;
            MaxTimer = Mathf.Clamp(time, 2f, 3.25f);
            Timer = MaxTimer;
            CurrentProgress += amount;
        }
    }
}