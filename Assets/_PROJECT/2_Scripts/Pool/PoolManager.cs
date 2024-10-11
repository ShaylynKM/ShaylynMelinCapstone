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

    private Dictionary<PoolObject, Stack<PoolObject>> _stackDictionary = new Dictionary<PoolObject, Stack<PoolObject>>();

    public void InitPool(PoolObject bulletPrefab, int poolSize)
    {
        if(!_stackDictionary.ContainsKey(bulletPrefab))
        {
            Stack<PoolObject> objStack = new Stack<PoolObject>();

            for(int i = 0; i < poolSize; i++)
            {
                PoolObject obj = Instantiate(bulletPrefab); // Instantiate the bullet prefabs of whatever necessary variation
                obj.gameObject.SetActive(false); // Set the new objects inactive
                objStack.Push(obj); // Push the new objects to the stack
            }
            _stackDictionary.Add(bulletPrefab, objStack); // Add the instantiated objects to the dictionary
        }

    }

    public PoolObject Spawn(PoolObject bulletPrefab)
    {
        Stack<PoolObject> objStack = _stackDictionary[bulletPrefab];

        if(objStack.Count <= 1) // In the event only one item is left in the pool
        {
            PoolObject newBullet = Instantiate(bulletPrefab); // Insantiate a new object
            return newBullet; // Return this new object to be used
        }
        else // In the event more than one item is left in the pool
        {
            PoolObject oldPoolObject = objStack.Pop(); // Remove the object from the stack to be used
            oldPoolObject.gameObject.SetActive(true); // Set the object as active in the scene
            return oldPoolObject; // Return this object that already existed in the pool to be used
        }
    }

    public void Despawn(PoolObject currentBullet, PoolObject bulletPrefab)
    {
        Stack<PoolObject> objStack = _stackDictionary[bulletPrefab];

        if (_stackDictionary.ContainsKey(bulletPrefab)) // If this object is in the dictionary
        {
            currentBullet.gameObject.SetActive(false); // Set this specific object as inactive in the scene
            objStack.Push(currentBullet); // Put the object back in the stack to be used later
        }
    }

}
