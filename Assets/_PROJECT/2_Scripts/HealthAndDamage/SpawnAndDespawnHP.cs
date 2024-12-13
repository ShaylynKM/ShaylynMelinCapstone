using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndDespawnHP : MonoBehaviour
{
    /// <summary>
    /// For spawning health pickups, since I'm having an issue using the normal spawners for it.
    /// </summary>
    
    [SerializeField] private hpDrop _hP;
    [SerializeField] private float _timeBeforeSpawn;
    [SerializeField] private float _timeBeforeDespawn;

    private void Start()
    {
        _hP.gameObject.SetActive(false);
        Debug.Log("Set hp inactive");
    }

    public void SpawnHealth()
    {
        Debug.Log("Trying to spawn health");
        StartCoroutine(WaitForSpawn());
    }

    public void DespawnHealth()
    {
        Debug.Log("Trying to despawn health");
        if(_hP != null)
        {
            Destroy(_hP.gameObject);
        }
    }

    IEnumerator WaitForSpawn()
    {
        Debug.Log("Waiting to spawn health");
        yield return new WaitForSeconds(_timeBeforeSpawn);
        Debug.Log("Waited to spawn health");
        _hP.gameObject.SetActive(true);
        Debug.Log("Spawned health");
        yield return new WaitForSeconds(_timeBeforeDespawn);
        Debug.Log("Waited to despawn health");
        DespawnHealth();
    }

}
