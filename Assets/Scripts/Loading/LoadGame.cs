using System.Collections;
using Prime31.TransitionKit;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    private int _scene = 3;
    private int i = 0;

    private IEnumerator Start()
    {
        //Application.backgroundLoadingPriority = ThreadPriority.Low;
        DontDestroyOnLoad(this);
        yield return Application.LoadLevelAsync("MainLevel"); ;
    }
}
