using UnityEngine;
using System.Collections;

public class EyeAnimationOnDeath : MonoBehaviour
{
    [SerializeField]
    private float _timeBeforeCallingDeathAnimation = 0.25f;

    public delegate void OnAnimationOverHandler();
    public event OnAnimationOverHandler OnAnimationOver;

    private Animator _anim;
    private ActivateTrigger _activateTrigger;
    private WaitForSeconds _finishedDeathAnimationDelay;

    private void Start()
	{
	    _anim = GetComponentInChildren<Animator>();
	    _activateTrigger = GetComponent<ActivateTrigger>();
        _finishedDeathAnimationDelay = new WaitForSeconds(_timeBeforeCallingDeathAnimation);
        _activateTrigger.OnTrigger += OnTrigger;
	}

    private void OnTrigger()
    {       
        StartCoroutine(PlayAnimationAndCallMoveObjectCoroutine());
    }

    private IEnumerator PlayAnimationAndCallMoveObjectCoroutine()
    {
        _anim.SetBool("IsDead", true);
        yield return _finishedDeathAnimationDelay;
        OnAnimationOver();
    }


}
