using UnityEngine;
using System.Collections;

public class AnimateCrowOnPlayerHit : MonoBehaviour
{
    [SerializeField]
    private float _hitDuration = 0.2f;

    private WaitForSeconds _delayAnimation;

    private GameObjectTags _objectTags;
    private AnimationTags _animTags;

    private Animator _animator;

    public delegate void OnCrowAnimationStartHandler();
    public event OnCrowAnimationStartHandler OnCrowAnimationStart;

    private void Start ()
	{
        _delayAnimation = new WaitForSeconds(_hitDuration);

        _animTags = StaticObjects.GetAnimationTags();
        _objectTags = StaticObjects.GetObjectTags();

        _animator = GetComponent<Animator>();
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (CanGetHit(collider))
        {
            OnCrowAnimationStart();
            _animator.SetBool(_animTags.IsHit, true);
            StartCoroutine(FinishAnimation());
        }
    }

    private IEnumerator FinishAnimation()
    {
        yield return _delayAnimation;

        _animator.SetBool(_animTags.IsHit, false);
    }

    private bool CanGetHit(Collider2D collider)
    {
        return collider.gameObject.tag == _objectTags.BasicAttackHitbox || collider.gameObject.tag == _objectTags.Knife || collider.gameObject.tag == _objectTags.AxeBlade;
    }
}
