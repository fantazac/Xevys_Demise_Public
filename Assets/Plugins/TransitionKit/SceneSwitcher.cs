using UnityEngine;
using System.Collections;
using Prime31.TransitionKit;
using UnityEngine.SceneManagement;


/// <summary>
/// To use the demo just add all three scenes to your build settings making sure the BoostrapScene is scene 0
/// </summary>
public class SceneSwitcher : MonoBehaviour
{
    public Texture2D maskTexture;

    private void Start()
    {
        StartCoroutine(waitForthreeSeconds());
    }

    private IEnumerator waitForthreeSeconds()
    {
        yield return new WaitForSeconds(3f);

        var fader = new FadeTransition()
        {
            nextScene = 1,
            fadedDelay = 0.2f,
            fadeToColor = Color.black
        };
        TransitionKit.instance.transitionWithDelegate(fader);
    }


    void OnGUI()
    {
        if (GUILayout.Button("Fade to Scene"))
        {
            var fader = new FadeTransition()
            {
                nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1,
                fadedDelay = 0.2f,
                fadeToColor = Color.black
            };
            TransitionKit.instance.transitionWithDelegate(fader);
        }

        if (GUILayout.Button("Wind to Scene"))
        {
            var wind = new WindTransition()
            {
                nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1,
                duration = 1.0f,
                size = 0.3f
            };
            TransitionKit.instance.transitionWithDelegate(wind);
        }

        if (GUILayout.Button("Mask to Scene"))
        {
            var mask = new ImageMaskTransition()
            {
                maskTexture = maskTexture,
                backgroundColor = Color.yellow,
                nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1
            };
            TransitionKit.instance.transitionWithDelegate(mask);
        }
    }
}
