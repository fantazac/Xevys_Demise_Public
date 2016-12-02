using System.Collections;
using Prime31.TransitionKit;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    private int _scene = 3;

    private IEnumerator Start()
    {
        DontDestroyOnLoad(this);
        yield return Application.LoadLevelAsync("MainLevel"); ;
    }
}
