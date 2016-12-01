using UnityEngine;

public class MainMenuGroupButtonsFadeListener : MonoBehaviour
{
    private MainMenuCurrentInterfaceAnimator _mainMenuCurrentInterfaceAnimator;
    private Animator _animator;

    private void Start()
    {
        _mainMenuCurrentInterfaceAnimator = GetComponentInParent<MainMenuCurrentInterfaceAnimator>();

        switch (gameObject.name)
        {
            case "MainMenuAccountsButtons":
                _mainMenuCurrentInterfaceAnimator.OnAccountInterfaceFade += InterfaceFade;
                break;
            case "MainMenuMainButtons":
                _mainMenuCurrentInterfaceAnimator.OnMainInterfaceFade += InterfaceFade;
                break;
            case "MainMenuOptionsButtons":
                _mainMenuCurrentInterfaceAnimator.OnOptionsInterfaceFade += InterfaceFade;
                break;
            case "MainMenuControlsOptionsButtons":
                _mainMenuCurrentInterfaceAnimator.OnControlsInterfaceFade += InterfaceFade;
                break;
            case "MainMenuAudioOptionsButtons":
                _mainMenuCurrentInterfaceAnimator.OnAudioInterfaceFade += InterfaceFade;
                break;
        }

        _animator = GetComponent<Animator>();
    }

    private void InterfaceFade()
    {
        _animator.SetTrigger("FadeOut");
    }
}
