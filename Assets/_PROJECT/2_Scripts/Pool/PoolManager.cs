using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : Singleton<PoolManager>
{
    /// <summary>
    /// Push: Inserts an element at the top of the stack
    /// Pop: Removes an element from the top of the stack
    /// Peek: Returns an element that is at the top of the stack but does not remove it
    /// </summary>

    private Dictionary<string, Stack<PoolObject>> _stackDictionary = new Dictionary<string, Stack<PoolObject>>();

    public void InitPool(PoolObject bulletPrefab, int poolSize)
    {
        string key = bulletPrefab.name;

        if(!_stackDictionary.ContainsKey(key))
        {
            Stack<PoolObject> objStack = new Stack<PoolObject>();

            for(int i = 0; i < poolSize; i++)
            {
                PoolObject obj = Instantiate(bulletPrefab); // Instantiate the bullet prefabs of whatever necessary variation
                obj.gameObject.SetActive(false); // Set the new objects inactive
                objStack.Push(obj); // Push the new objects to the stack
            }
            _stackDictionary.Add(key, objStack); // Add the insantiated objects to the dictionary
        }

    }

    public PoolObject Spawn(string name)
    {
        Stack<PoolObject> objStack = _stackDictionary[name];

        if(objStack.Count <= 1) // In the event only one item is left in the pool
        {
            PoolObject poolObject = objStack.Peek(); // Look at the top item
            PoolObject objectClone = Instantiate(poolObject); // Instantiate a clone of the object
            objectClone.name = poolObject.name; // All objects must have the same name
            return objectClone; // If we are calling for another object and there isn't enough, we instantiate a new one
        }
        else // In the event more than one item is left in the pool
        {
            PoolObject oldPoolObject = objStack.Pop(); // Remove the object from the stack to be used
            oldPoolObject.gameObject.SetActive(true); // Set the object as active in the scene
            return oldPoolObject; // Return this object that already existed in the pool to be used
        }
    }

    public void Despawn(PoolObject poolObject)
    {
        string key = poolObject.name;

        Stack<PoolObject> objStack = _stackDictionary[poolObject.name];

        if (_stackDictionary.ContainsKey(key)) // If this object is in the dictionary
        {
            poolObject.gameObject.SetActive(false); // Set the object as inactive in the scene
            objStack.Push(poolObject); // Put the object back in the stack to be used later
        }
    }

}
