using UnityEngine;
using System.Collections;

public class PauseMenuGroupButtonsFadeListener : MonoBehaviour
{

    private PauseMenuCurrentInterfaceAnimator _pauseMenuCurrentInterfaceAnimator;
    private Animator _animator;

    private void Start()
    {
        _pauseMenuCurrentInterfaceAnimator = GetComponentInParent<PauseMenuCurrentInterfaceAnimator>();

        switch (gameObject.name)
        {
            case "PauseMenuMainButtons":
                _pauseMenuCurrentInterfaceAnimator.OnMainInterfaceFade += InterfaceFade;
                break;
            //case "PauseMenuOptionsButtons":
            //    _pauseMenuCurrentInterfaceAnimator.OnOptionsInterfaceFade += InterfaceFade;
            //    break;
            //case "PauseMenuControlsOptionsButtons":
            //    _pauseMenuCurrentInterfaceAnimator.OnControlsInterfaceFade += InterfaceFade;
            //    break;
            //case "PauseMenuAudioOptionsButtons":
            //    _pauseMenuCurrentInterfaceAnimator.OnAudioInterfaceFade += InterfaceFade;
            //    break;
        }

        _animator = GetComponent<Animator>();
    }

    private void InterfaceFade()
    {
        _animator.SetTrigger("FadeOut");
    }
}
