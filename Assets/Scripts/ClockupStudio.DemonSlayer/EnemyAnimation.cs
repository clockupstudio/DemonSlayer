using System;
using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimation : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int ReceivedDamage = Animator.StringToHash("ReceivedDamage");

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void ReceivedDamageAnimationEnd()
        {
            _animator.SetBool(ReceivedDamage, false);
        }

        public void ReceivedDamageAnimationStart()
        {
            _animator.SetBool(ReceivedDamage, true);
        }
    }
}