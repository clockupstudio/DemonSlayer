using System;
using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    public class Enemy : MonoBehaviour
    {
        public int Health = 1;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Health--;
            if (Health == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}