using UnityEngine;
using System.Collections;

public class ActorBasicAttack : MonoBehaviour
{
    private const int FRAME_BUFFER = 30;

    private InputManager _inputManager;
    private SpriteRenderer _spriteRenderer;
    private GameObject _attackHitBox;
    private AudioSource[] _audioSources;
    private int _count;

    void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _spriteRenderer = GameObject.Find("CharacterBasicAttackBoxSprite").GetComponent<SpriteRenderer>();
        _attackHitBox = GameObject.Find("CharacterBasicAttackBox");
        _audioSources = GetComponents<AudioSource>();
        _count = FRAME_BUFFER;

        _inputManager.OnBasicAttack += OnBasicAttack;
    }

    void Update()
    {
        _count++;
        if (_spriteRenderer.enabled && _count >= FRAME_BUFFER / 2)
        {
            _spriteRenderer.enabled = false;
            _attackHitBox.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OnBasicAttack()
    {
        if (_count >= FRAME_BUFFER)
        {
            _spriteRenderer.enabled = true;
            _attackHitBox.GetComponent<BoxCollider2D>().enabled = true;
            _count = 0;

            _audioSources[0].Play();
        }
    }
}
