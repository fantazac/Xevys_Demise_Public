using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    private static GameObject _player;

    private void Start()
    {
        if(_player == null)
        {
            _player = gameObject;
        }
    }

    public static GameObject GetPlayer()
    {
        return _player;
    }
}
