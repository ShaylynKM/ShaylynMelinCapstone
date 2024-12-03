//using System.Collections;
//using UnityEngine;

//namespace _PROJECT._2_Scripts.Patterns.Strategy.PhaseStrategy
//{
//    public class SeansDummyPhase: Phase
//    {
//        [SerializeField] private float spawnTime = .25f;
//        [SerializeField] private float spawnAmount = 10;
//        public override void StartPhase(BulletSpawner bulletSpawner)
//        {
//            base.StartPhase(bulletSpawner);
//            StartCoroutine(DumbSpawner());

//        }

//        private IEnumerator DumbSpawner()
//        {
//            var spawnCounter = 0;
//            while (spawnCounter < spawnAmount)
//            {
//                if(SpawnWithDirection)
//                    _bulletSpawner.SpawnBulletWithDirection();
//                else
//                    _bulletSpawner.SpawnBulletWithTarget();
//                yield return new WaitForSeconds(spawnTime);
//                spawnCounter++;
//            }
//            FinishPhase();
//        }
        
//    }
//}