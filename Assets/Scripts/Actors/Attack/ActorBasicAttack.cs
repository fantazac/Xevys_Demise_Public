using UnityEngine;
using System.Collections;

public class ActorBasicAttack : MonoBehaviour
{
    [SerializeField]
    private const int ATTACK_SPEED = 30;

    private InputManager _inputManager;
    private GameObject _attackHitBox;
    private AudioSource[] _audioSources;
    private int _count;

    void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _attackHitBox = GameObject.Find("CharacterBasicAttackBox");
        _audioSources = GetComponents<AudioSource>();
        _count = ATTACK_SPEED;

        _inputManager.OnBasicAttack += OnBasicAttack;
    }

    void Update()
    {
        _count++;
        if (_count >= ATTACK_SPEED / 2)
        {
            _attackHitBox.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OnBasicAttack()
    {
        if (_count >= ATTACK_SPEED)
        {
            _attackHitBox.GetComponent<BoxCollider2D>().enabled = true;
            _count = 0;

            _audioSources[0].Play();
        }
    }
}
