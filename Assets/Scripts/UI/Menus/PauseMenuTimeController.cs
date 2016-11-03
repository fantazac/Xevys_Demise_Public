using UnityEngine;
using System.Collections;

public class PauseMenuTimeController : MonoBehaviour
{
    public static float deltaTime;
    private static float _lastframetime;

    void Start()
    {
        _lastframetime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        deltaTime = Time.realtimeSinceStartup - _lastframetime;
        _lastframetime = Time.realtimeSinceStartup;
    }
}
