using UnityEngine;
using System.Collections;

public class EyeState : MonoBehaviour
{
    private Animator _anim;
    private ActivateTrigger _activateTrigger;

	private void Start()
	{
	    _anim = GetComponent<Animator>();
	    _activateTrigger = GetComponentInParent<ActivateTrigger>();
	    _activateTrigger.OnTrigger += OnTriggerActivated;

	}

    private void OnTriggerActivated()
    {
        _anim.SetBool("IsDead", true);
    }
}
