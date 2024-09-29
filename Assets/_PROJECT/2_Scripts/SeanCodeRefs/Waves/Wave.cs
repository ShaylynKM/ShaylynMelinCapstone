//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;
//using UnityEngine.Events;

//[Serializable]
//public class Wave
//{
//    private WaveData _data; //this is the field representation of WaveData, and we use this notation whenever we have a situation where our get or set has particular logic in it beyond setting up the variable
//    [field: SerializeField]
//    public WaveData Data { get; private set; }
//    public virtual IEnumerator WaveFunction()
//    {
//        yield return null;
//    }
//}
//[Serializable]
//public class WaveData
//{
//    [field: SerializeField]
//    public float Speed { get; private set; }
//    [field: SerializeField]
//    public float TimeBeforeSpawn { get; private set; }
//    [field: SerializeField]
//    public float TimeAfterWave { get; private set; }
//    [field: SerializeField]
//    public WaveTarget TargetPrefab { get; private set; }
//    [field: SerializeField]
//    public int NumberOfTargets { get; private set; }
//}