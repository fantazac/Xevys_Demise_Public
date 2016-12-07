using UnityEngine;
using System.Collections;

public class ArtefactTooltip : MonoBehaviour
{
    private Animator _animator;
    private ActivateTrigger _trigger;

    private void Start ()
    {
        _animator = GetComponent<Animator>();
        _trigger = transform.parent.FindChild("Door Trigger").GetComponent<ActivateTrigger>();
        _trigger.OnTrigger += OnTrigger;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _animator.SetTrigger("FadeIn");
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _animator.SetTrigger("FadeOut");
        }
    }

    private void OnTrigger()
    {
        _animator.SetTrigger(("FadeOut"));
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
