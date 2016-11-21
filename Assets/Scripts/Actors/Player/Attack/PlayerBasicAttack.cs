using UnityEngine;
using System.Collections;

public class PlayerBasicAttack : MonoBehaviour
{
    [SerializeField]
    private float _attackSpeed = 3f;

    [SerializeField]
    private int _soundId = 0;

    private const float BASIC_ATTACK_SPEED = 1f;
    private const float ATTACK_DURATION_MULTIPLIER = 0.6f;
    private const float ATTACK_COOLDOWN_MULTIPLIER = 1.2f;

    private float _attackDuration;

    private WaitForSeconds _finishedAttackDelay;
    private WaitForSeconds _finishAttackAnimDelay;
    private WaitForSeconds _allowNewAttackDelay;

    private InputManager _inputManager;
    private BoxCollider2D _attackHitBox;
    private AudioSourcePlayer _soundPlayer;

    private bool _canAttack = true;
    private float _attackFrequency;

    private void Start()
    {
        _attackFrequency = BASIC_ATTACK_SPEED / _attackSpeed;
        _attackDuration = _attackFrequency * ATTACK_DURATION_MULTIPLIER;
        _inputManager = GetComponentInChildren<InputManager>();
        _attackHitBox = GameObject.Find("CharacterBasicAttackBox").GetComponent<BoxCollider2D>();
        _soundPlayer = GetComponent<AudioSourcePlayer>();

        _inputManager.OnBasicAttack += OnBasicAttack;

        _allowNewAttackDelay = new WaitForSeconds(_attackFrequency * ATTACK_COOLDOWN_MULTIPLIER);
        _finishAttackAnimDelay = new WaitForSeconds(_attackFrequency);
        _finishedAttackDelay = new WaitForSeconds(_attackDuration);
    }

    private void OnBasicAttack()
    {
        if (_canAttack && !PlayerState.IsKnockedBack)
        {
            ActivateBasicAttack();
        }
    }

    private void ActivateBasicAttack()
    {
        PlayerState.SetAttacking(true, _attackSpeed);
        _canAttack = false;
        _soundPlayer.Play(_soundId);
        _attackHitBox.enabled = true;
        StartCoroutine(OnBasicAttackFinished());
        StartCoroutine(FinishAttackAnimation());
        StartCoroutine(AllowNewAttack());
    }

    private IEnumerator FinishAttackAnimation()
    {
        yield return _finishAttackAnimDelay;

        PlayerState.SetAttacking(false, 1);
    }

    private IEnumerator AllowNewAttack()
    {
        yield return _allowNewAttackDelay;

        _canAttack = true;
    }

    private IEnumerator OnBasicAttackFinished()
    {
        yield return _finishedAttackDelay;

        _attackHitBox.enabled = false;
    }
}
