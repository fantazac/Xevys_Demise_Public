using UnityEngine;

public class PauseMenuControlsButtonsAnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShowControlsInterface()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(StaticObjects.GetAnimationTags().PauseMenuButtonsGroupActiveAnimation))
        {
            _animator.SetTrigger("Show");
        }
    }
}
