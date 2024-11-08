using System.Collections;
using UnityEngine;

namespace _PROJECT._0_TryingStuffOut
{
    public class TryingStuffSpawnStrategy : MonoBehaviour
    {
        //this handles whatever internal logic it needs to for spawning, whether it spawns or not
        public virtual void Spawn(Vector3 position, GameObject prefab)
        {
            //keeps track of all it
            StartCoroutine(SpawnWithSinPattern(position, prefab));
        }
        
        IEnumerator SpawnWithSinPattern(Vector3 initialPosition, GameObject prefab)
        {
            float sinTime = 0;
            float timeInc = .5f;
            float waveHeight = 5f;
            float waveSpeed = 2f;
            var bounds = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        
            while (true)
            {
                var position = new Vector3(initialPosition.x + sinTime * waveSpeed, initialPosition.y + Mathf.Sin(timeInc) * bounds.y / 2, 0f);
                Instantiate(prefab, position, prefab.transform.rotation);
                sinTime += timeInc;
                if (bounds.x + sinTime * waveSpeed > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x)
                    sinTime = 0;
                yield return new WaitForSeconds(timeInc);
            }
        }
        
    }
}