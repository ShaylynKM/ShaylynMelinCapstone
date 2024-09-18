using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackStrategy : AttackStrategy
{
    // Start is called before the first frame update


    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit you");
    }


}
