using UnityEngine;
using System.Collections;

public class StaticObjects : MonoBehaviour
{
    private static GameObject _player;
    private static GameObject _canvas;

    private void Start()
    {
        _player = GameObject.Find("Character");
        _canvas = GameObject.Find("Canvas");
    }

    public static GameObject GetPlayer()
    {
        return _player;
    }

    public static GameObject GetCanvas()
    {
        return _canvas;
    }
}
