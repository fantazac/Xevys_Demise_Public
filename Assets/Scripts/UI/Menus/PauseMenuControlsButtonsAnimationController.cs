using UnityEngine;
using System.Collections;

public class PauseMenuControlsButtonsAnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShowControlsInterface()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("PauseMenuButtonsGroupActiveAnimation"))
        {
            _animator.SetTrigger("Show");
        }
    }
}
