using UnityEngine;

namespace _PROJECT._0_TryingStuffOut
{
    [RequireComponent(typeof(MoveStrategy))]
    public class Spawner : MonoBehaviour
    {
        [SerializeField] protected GameObject spawnObject;
        private MoveStrategy _moveStrategy;


    }
}