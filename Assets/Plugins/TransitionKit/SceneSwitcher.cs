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
    private bool _isUiVisible = true;


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


    //void OnGUI()
    //{
    //    // hide the UI during transitions
    //    if (!_isUiVisible)
    //        return;

    //    if (GUILayout.Button("Fade to Scene"))
    //    {
    //        var fader = new FadeTransition()
    //        {
    //            nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1,
    //            fadedDelay = 0.2f,
    //            fadeToColor = Color.black
    //        };
    //        TransitionKit.instance.transitionWithDelegate(fader);
    //    }

    //    if (GUILayout.Button("Wind to Scene"))
    //    {
    //        var wind = new WindTransition()
    //        {
    //            nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1,
    //            duration = 1.0f,
    //            size = 0.3f
    //        };
    //        TransitionKit.instance.transitionWithDelegate(wind);
    //    }

    //    if (GUILayout.Button("Mask to Scene"))
    //    {
    //        var mask = new ImageMaskTransition()
    //        {
    //            maskTexture = maskTexture,
    //            backgroundColor = Color.yellow,
    //            nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1
    //        };
    //        TransitionKit.instance.transitionWithDelegate(mask);
    //    }
    //}


    void OnEnable()
    {
        TransitionKit.onScreenObscured += onScreenObscured;
        TransitionKit.onTransitionComplete += onTransitionComplete;
    }


    void OnDisable()
    {
        // as good citizens we ALWAYS remove event handlers that we added
        TransitionKit.onScreenObscured -= onScreenObscured;
        TransitionKit.onTransitionComplete -= onTransitionComplete;
    }


    void onScreenObscured()
    {
        _isUiVisible = false;
    }


    void onTransitionComplete()
    {
        _isUiVisible = true;
    }
}
