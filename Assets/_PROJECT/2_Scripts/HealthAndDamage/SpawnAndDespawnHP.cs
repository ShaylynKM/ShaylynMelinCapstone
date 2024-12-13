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
    }

    public void SpawnHealth()
    {
        StartCoroutine(WaitForSpawn());
    }

    public void DespawnHealth()
    {
        if(_hP != null)
        {
            Destroy(_hP.gameObject);
        }
    }

    IEnumerator WaitForSpawn()
    {
        yield return new WaitForSeconds(_timeBeforeSpawn);
        _hP.gameObject.SetActive(true);
        yield return new WaitForSeconds(_timeBeforeDespawn);
        DespawnHealth();
    }

}
