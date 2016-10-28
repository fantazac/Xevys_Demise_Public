using UnityEngine;
using System.Collections;

public class ActorBasicAttack : MonoBehaviour
{
    [SerializeField]
    private const float ATTACK_SPEED = 0.2f;

    private InputManager _inputManager;
    private GameObject _attackHitBox;
    private AudioSourcePlayer _soundPlayer;
    private Animator _anim;

    private bool _isAttacking = false;
    private float _attackCooldown;

    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _attackHitBox = GameObject.Find("CharacterBasicAttackBox");
        _anim = GameObject.Find("CharacterSprite").GetComponent<Animator>();
        _soundPlayer = GetComponent<AudioSourcePlayer>();
        _attackCooldown = ATTACK_SPEED;

        _inputManager.OnBasicAttack += OnBasicAttack;
    }

    private void Update()
    {
        _anim.SetBool("IsAttacking", _isAttacking);
        _attackCooldown += Time.deltaTime;

        if (_attackCooldown >= ATTACK_SPEED / 2)
        {
            _attackHitBox.GetComponent<BoxCollider2D>().enabled = false;
            _isAttacking = false;
        }
    }

    private void OnBasicAttack()
    {
        if (_attackCooldown >= ATTACK_SPEED)
        {
            _isAttacking = true;
            _attackCooldown = 0;
        }
    }

    public void OnBasicAttackEffective()
    {
        _soundPlayer.Play(0);
        _attackHitBox.GetComponent<BoxCollider2D>().enabled = true;
    }
}
