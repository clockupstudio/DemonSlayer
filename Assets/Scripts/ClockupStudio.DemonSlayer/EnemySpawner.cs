using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ClockupStudio.DemonSlayer
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject EnemyPrefab;
        public Transform Target;

        public int StartEnemyNumber = 1;
        public int IncreasingStep = 1;
        
        private int _currentEnemyNumber = Int32.MaxValue;

        private void Start()
        {
            _currentEnemyNumber = StartEnemyNumber;
        }

        void Update()
        {
            if (transform.childCount > 0)
            {
                return;
            }
            
            Debug.Log(_currentEnemyNumber);
            for (var i = 0; i < _currentEnemyNumber; i++)
            {
                var enemy = Instantiate(EnemyPrefab, transform);
                var follow = enemy.GetComponent<EnemyFollow>();
                follow.Target = Target;
                follow.Speed = Random.Range(0.5f, 2f);
            }


            _currentEnemyNumber += IncreasingStep;
        }
    }
}
