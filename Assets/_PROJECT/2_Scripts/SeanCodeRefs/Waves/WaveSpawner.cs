//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

//public class WaveSpawner : MonoBehaviour
//{
//    #region Serialize Fields
//    [SerializeField]
//    Transform startPosition, endPosition;
//    [SerializeField]
//    List<Wave> waves = new List<Wave>();

//    #endregion
//    #region Public variables
//    [HideInInspector]
//    public UnityEvent targetHit;
//    #endregion
//    #region Private variables

//    #endregion

//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//    public void StartSpawn()
//    {
//        //this is called when our game begins
//        Debug.Log("We started spawning");
//        StopAllCoroutines();
//        StartCoroutine(WaveSpawning());

//    }
//    public void StopSpawn() //when the game time is over, no matter what waves exist, stop the waves
//    {
//        StopAllCoroutines();
//    }
//    IEnumerator WaveSpawning()
//    {
//        //for our wave spawning, we want to specify the direction for our targets to move (this is specific to this situation, and is very different for some of your games)
//        Vector3 direction = (endPosition.position - startPosition.position).normalized; //subtract our end point from our start point to get the direction
//        foreach (Wave wave in waves)
//        {
//            //our waves have a number of items to spawn
//            for (int i = 0; i < wave.Data.NumberOfTargets; i++) //for each wave target
//            {
//                WaveTarget newTarget = Instantiate(wave.Data.TargetPrefab, startPosition.position, Quaternion.Euler(0, 90, 0)); //spawn a new target at the start location, then rotate it 90 degrees
//                newTarget.StartMoving(direction, wave.Data.Speed);
//                newTarget.targetHit.AddListener(() => targetHit.Invoke()); //we create an anonymous function to Invoke an event call whenever our targetHit event happens on the new target
//                yield return new WaitForSeconds(wave.Data.TimeBeforeSpawn);//so wait for a certain length of time after each item is spawned
//            }
//            yield return new WaitForSeconds(wave.Data.TimeAfterWave);
//        }
//    }
//}