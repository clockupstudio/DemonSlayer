using System;
using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    public class PlayerHealth : MonoBehaviour
    {
        public int MaxHP = 1;

        private int _currentHP = Int32.MaxValue;

        private void Start()
        {
            _currentHP = MaxHP;
        }

        public void Decrease()
        {
            _currentHP--;
        }

        public int CurrentHp => _currentHP;
    }
}
