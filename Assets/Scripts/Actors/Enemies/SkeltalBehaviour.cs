using UnityEngine;
using System.Collections;

public class SkeltalBehaviour : MonoBehaviour
{

    [SerializeField]
    protected float _cooldownAfterAttack = 1f;

    [SerializeField]
    protected float _attackTime = 1.5f;

    private BoxCollider2D _swordHitbox;

    protected Animator _animator;

    protected Vector3 _initialPosition;

    protected SkeltalOrientation _skeltalOrientation;

    private WaitForSeconds _waitForAttack;
    private WaitForSeconds _waitForCooldown;

    public delegate void OnSkeltalAttackStartHandler();
    public event OnSkeltalAttackStartHandler OnSkeltalAttackStart;

    public delegate void OnSkeltalAttackFinishedHandler();
    public event OnSkeltalAttackFinishedHandler OnSkeltalAttackFinished;

    public delegate void OnSkeltalMovementStartHandler();
    public event OnSkeltalMovementStartHandler OnSkeltalMovementStart;

    public delegate void OnSkeltalMovementFinishedHandler();
    public event OnSkeltalMovementFinishedHandler OnSkeltalMovementFinished;

    protected virtual void Start()
    {
        _initialPosition = transform.position;
        _swordHitbox = GetComponentsInChildren<BoxCollider2D>()[1];
        _swordHitbox.offset = new Vector2(_swordHitbox.offset.x * -1, _swordHitbox.offset.y);
        _animator = GetComponent<Animator>();

        _waitForAttack = new WaitForSeconds(_attackTime);
        _waitForCooldown = new WaitForSeconds(_cooldownAfterAttack);

        _skeltalOrientation = GetComponent<SkeltalOrientation>();

        OnSkeltalMovementStart += StartSkeltalMovement;
        OnSkeltalMovementFinished += StartSkeltalAttack;
        GetComponent<Health>().OnDeath += StopMovementOnDeath;

        OnSkeltalMovementStart();
    }

    protected void StartSkeltalAttack()
    {
        StartCoroutine("SkeltalAttack");
    }

    protected virtual void StartSkeltalMovement()
    {
        StartCoroutine("SkeltalMovement");
    }

    protected IEnumerator SkeltalAttack()
    {
        _swordHitbox.enabled = true;
        OnSkeltalAttackStart();

        yield return _waitForAttack;

        _swordHitbox.enabled = false;
        OnSkeltalAttackFinished();

        yield return _waitForCooldown;

        OnSkeltalMovementStart();
    }

    protected virtual IEnumerator SkeltalMovement()
    {
        yield return null;
    }

    protected void StopMovementOnDeath()
    {
        StopAllCoroutines();
        enabled = false;
    }

    protected virtual void SkeltalMovementFinished()
    {
        OnSkeltalMovementFinished();
    }
}
