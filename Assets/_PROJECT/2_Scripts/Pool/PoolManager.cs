using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : Singleton<PoolManager>
{
    /// <summary>
    /// Reminder to myself:
    /// Push: Inserts an element at the top of the stack
    /// Pop: Removes an element from the top of the stack
    /// </summary>

    private Dictionary<string, Stack<PoolObject>> _stackDictionary = new Dictionary<string, Stack<PoolObject>>(); // This creates a dictionary of stacks of pool objects. Will be populated with references to our bullet prefabs, found on the bullet spawner.

    public void InitPool(PoolObject bulletPrefab, int poolSize)
    {
        string key = bulletPrefab.name.Replace("(Clone)", "").Trim(); // Used so that the name is consistent
        if(!_stackDictionary.ContainsKey(key)) // If this type of bullet prefab isn't in the dictionary already
        {
            Stack<PoolObject> objStack = new Stack<PoolObject>(); // Create a stack of this type of bullet prefab

            for(int i = 0; i < poolSize; i++)
            {
                PoolObject obj = Instantiate(bulletPrefab); // Instantiate the bullet prefabs of whatever specified variation
                obj.name = key;
                obj.gameObject.SetActive(false); // Set the new objects inactive
                objStack.Push(obj); // Push the new objects to the stack
            }
            _stackDictionary.Add(bulletPrefab.name, objStack); // Add the instantiated objects to the dictionary
        }
    }

    public PoolObject Spawn(PoolObject bulletPrefab)
    {
        //string key = bulletPrefab.name.Replace("(Clone)", "").Trim();

        Stack<PoolObject> objStack = _stackDictionary[bulletPrefab.name]; // Dictionary entry for the stack of this specific type of bullet

        if(objStack.Count <= 1) // In the event only one item is left in the pool (used 1 instead of 0 to prevent null reference exceptions:)
        {
            PoolObject newBullet = Instantiate(bulletPrefab); // Instantiate a new object
            newBullet.name = bulletPrefab.name;
            return newBullet; // Return this new object to be used
        }
        else // In the event more than one item is left in the pool
        {
            PoolObject existingBullet = objStack.Pop(); // Remove the object from the stack to be used
            existingBullet.gameObject.SetActive(true); // Set the object as active in the scene
            return existingBullet; // Return this object that already existed in the pool to be used
        }
    }
    public void Despawn(GameObject despawnObject)
    {
        if(despawnObject.TryGetComponent<PoolObject>(out PoolObject poolOjbect))
        {
            Despawn(poolOjbect);
        }
        else
        {
            Debug.Log("You are trying to despawn something that is not a pool object. No.");
        }
    }
    public void Despawn(PoolObject currentBullet)
    {
        string key = currentBullet.name;

        Stack<PoolObject> objStack = _stackDictionary[key]; // Dictionary entry for the stack of this specific type of bullet

        if (_stackDictionary.ContainsKey(key)) // If this object is in the dictionary
        {
            currentBullet.gameObject.SetActive(false); // Set this specific object as inactive in the scene
            objStack.Push(currentBullet); // Put the object back in the stack to be used later
        }
    }

}
