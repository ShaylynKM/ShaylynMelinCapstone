using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggler : MonoBehaviour
{
    private float _wiggleHeight = 0.2f;

    private float _wiggleSpeed = 3f;

    void Update()
    {
        float wiggleStagger = Mathf.Sin(Time.time * _wiggleSpeed) * _wiggleHeight;

        transform.position = new Vector3(0f, wiggleStagger, 0f);
    }

}

