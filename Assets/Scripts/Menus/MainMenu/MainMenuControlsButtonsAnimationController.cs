using UnityEngine;

public class MainMenuControlsButtonsAnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShowControlsInterface()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(MainMenuStaticObjects.GetAnimationTags().MainMenuButtonsGroupActiveAnimation))
        {
            _animator.SetTrigger("Show");
        }
    }
}
