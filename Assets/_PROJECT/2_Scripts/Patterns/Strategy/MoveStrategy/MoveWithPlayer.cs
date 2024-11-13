using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlayer : MoveStrategy
{
    private GameObject _playerObject;

    public GameObject PlayerObject
    {
        get
        {
            return _playerObject;
        }
        set
        {
            _playerObject = value;
        }
    }
}
