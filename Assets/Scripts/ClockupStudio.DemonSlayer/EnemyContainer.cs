using System.Collections.Generic;
using UnityEngine;

namespace ClockupStudio.DemonSlayer
{
    public class EnemyContainer : MonoBehaviour
    {
        private List<Enemy> _enemies = new List<Enemy>();
        
        public void Add(Enemy enemy)
        {
            _enemies.Add(enemy);    
        }

        public int Count()
        {
            return _enemies.Count;
        }
    }
}
