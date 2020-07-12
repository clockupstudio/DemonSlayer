using System;
using UnityEngine;
using UnityEngine.UI;

namespace ClockupStudio.DemonSlayer
{
    public class PlayerHealthText : MonoBehaviour
    {
        public PlayerHealth PlayerHealth;

        private Text _text;

        private void Start()
        {
            _text = GetComponent<Text>();
        }

        void Update()
        {
            _text.text = $"LIFE: {PlayerHealth.CurrentHp}";
        }
    }
}
