using UnityEngine;
using System.Collections;

public class EyeAnimationOnDeath : MonoBehaviour
{
    [SerializeField]
    private float _timeBeforeCallingDeathAnimation = 0.25f;

    public delegate void OnAnimationOverHandler();
    public event OnAnimationOverHandler OnAnimationOver;

    private Animator _anim;
    private WaitForSeconds _finishedDeathAnimationDelay;

    private void Start()
	{
	    _anim = GetComponentInChildren<Animator>();
        GetComponent<ActivateTrigger>().OnTrigger += OnTrigger;

        _finishedDeathAnimationDelay = new WaitForSeconds(_timeBeforeCallingDeathAnimation);
	}

    private void OnTrigger()
    {
        _anim.SetBool(StaticObjects.GetAnimationTags().IsDead, true);

        StartCoroutine(FinishAnimation());
    }

    private IEnumerator FinishAnimation()
    {
        yield return _finishedDeathAnimationDelay;

        OnAnimationOver();
    }
}
