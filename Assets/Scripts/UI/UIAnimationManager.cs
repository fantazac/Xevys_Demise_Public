using UnityEngine;
using System.Collections;

public class UIAnimationManager : MonoBehaviour
{
    [SerializeField]
    private PauseMenuInputs _pauseMenuInputs;

    private GameObject _uIPanel;
    private Animator _animator;
    private bool _active;

    private void Start()
    {
        _uIPanel = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        _animator = GetComponent<Animator>();
        _pauseMenuInputs.TriggerAnimations += FadeUI;
        _active = true;
    }

    private void FadeUI()
    {
    Debug.Log(_active);
        if (!_active)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }

    private void FadeOut()
    {
        _animator.SetTrigger("FadeOut");
        _active = false;
    }

    private void FadeIn()
    {
        _animator.SetTrigger("FadeIn");
        _active = true;
    }
}
