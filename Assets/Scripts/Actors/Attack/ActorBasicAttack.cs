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

    private InputManager _inputManager;
    private GameObject _attackHitBox;
    private AudioSourcePlayer _soundPlayer;
    private Animator _anim;

    private bool _isAttacking = false;
    private float _attackFrequency;

    private void Start()
    {
        _attackFrequency = BASIC_ATTACK_SPEED / _attackSpeed;
        _attackDuration = _attackFrequency * ATTACK_DURATION_MULTIPLIER;
        _inputManager = GetComponentInChildren<InputManager>();
        _attackHitBox = GameObject.Find("CharacterBasicAttackBox");
        _anim = GameObject.Find("CharacterSprite").GetComponent<Animator>();
        _soundPlayer = GetComponent<AudioSourcePlayer>();

        _inputManager.OnBasicAttack += OnBasicAttack;
    }

    private void OnBasicAttack()
    {
        if (!_isAttacking)
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
        _attackHitBox.GetComponent<BoxCollider2D>().enabled = true;
        Invoke("OnBasicAttackFinished", _attackDuration);
        Invoke("FinishAttackAnimation", _attackFrequency);
        Invoke("AllowNewAttack", _attackFrequency * ATTACK_COOLDOWN_MULTIPLIER);
    }

    private void FinishAttackAnimation()
    {
        _anim.speed = INITIAL_ANIMATION_SPEED;
        _anim.SetBool("IsAttacking", false);
    }

    private void AllowNewAttack()
    {
        _isAttacking = false;
    }

    private void OnBasicAttackFinished()
    {
        _attackHitBox.GetComponent<BoxCollider2D>().enabled = false;
    }
}
