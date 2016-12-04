using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    private int _scene = 3;

    private IEnumerator Start()
    {
        DontDestroyOnLoad(this);
        yield return SceneManager.LoadSceneAsync(_scene);
    }
}
