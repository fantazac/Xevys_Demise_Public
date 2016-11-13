using UnityEngine;
using System.Collections;

public class ActorBasicAttack : MonoBehaviour
{
    [SerializeField]
    private float _attackSpeed = 3f;

    [SerializeField]
    private int _soundId = 0;

    private const float BASIC_ATTACK_SPEED = 1f;
    private const float ATTACK_DURATION_MULTIPLIER = 0.6f;
    private const float INITIAL_ANIMATION_SPEED = 1f;
    private const float ATTACK_COOLDOWN_MULTIPLIER = 1.2f;

    private float _attackDuration;

    private WaitForSeconds _finishedAttackDelay;
    private WaitForSeconds _finishAttackAnimDelay;
    private WaitForSeconds _allowNewAttackDelay;

    private InputManager _inputManager;
    private BoxCollider2D _attackHitBox;
    private AudioSourcePlayer _soundPlayer;
    private Animator _anim;
    private PlayerGroundMovement _playerGroundMovement;

    private bool _isAttacking = false;
    private float _attackFrequency;

    private void Start()
    {
        _attackFrequency = BASIC_ATTACK_SPEED / _attackSpeed;
        _attackDuration = _attackFrequency * ATTACK_DURATION_MULTIPLIER;
        _inputManager = GetComponentInChildren<InputManager>();
        _attackHitBox = GameObject.Find("CharacterBasicAttackBox").GetComponent<BoxCollider2D>();
        _anim = GameObject.Find("CharacterSprite").GetComponent<Animator>();
        _soundPlayer = GetComponent<AudioSourcePlayer>();
        _playerGroundMovement = GetComponent<PlayerGroundMovement>();

        _inputManager.OnBasicAttack += OnBasicAttack;

        _allowNewAttackDelay = new WaitForSeconds(_attackFrequency * ATTACK_COOLDOWN_MULTIPLIER);
        _finishAttackAnimDelay = new WaitForSeconds(_attackFrequency);
        _finishedAttackDelay = new WaitForSeconds(_attackDuration);
    }

    private void OnBasicAttack()
    {
        if (!_isAttacking && !_playerGroundMovement.IsKnockedBack)
        {
            ActivateBasicAttack();
        }
    }

    private void ActivateBasicAttack()
    {
        _isAttacking = true;
        _anim.speed = _attackSpeed;
        _anim.SetBool("IsAttacking", _isAttacking);
        _soundPlayer.Play(_soundId);
        _attackHitBox.enabled = true;
        StartCoroutine("OnBasicAttackFinished");
        StartCoroutine("FinishAttackAnimation");
        StartCoroutine("AllowNewAttack");
    }

    private IEnumerator FinishAttackAnimation()
    {
        yield return _finishAttackAnimDelay;

        _anim.speed = INITIAL_ANIMATION_SPEED;
        _anim.SetBool("IsAttacking", false);
    }

    private IEnumerator AllowNewAttack()
    {
        yield return _allowNewAttackDelay;

        _isAttacking = false;
    }

    private IEnumerator OnBasicAttackFinished()
    {
        yield return _finishedAttackDelay;

        _attackHitBox.enabled = false;
    }

    public bool IsAttacking()
    {
        return _isAttacking;
    }
}
