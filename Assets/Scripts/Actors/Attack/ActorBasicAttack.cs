using UnityEngine;
using System.Collections;

public class ActorBasicAttack : MonoBehaviour
{
    private const int FRAME_BUFFER = 30;

    private InputManager _inputManager;
    private SpriteRenderer _spriteRenderer;
    private GameObject _attackHitBox;
    private AudioSource _audioSource;
    private int _count;

	void Start ()
	{
        _inputManager = GetComponent<InputManager>();
	    _spriteRenderer = GameObject.Find("CharacterBasicAttackBoxSprite").GetComponent<SpriteRenderer>();
        _attackHitBox = GameObject.Find("CharacterBasicAttackBox");
        _audioSource = GetComponent<AudioSource>();
	    _count = FRAME_BUFFER;

	    _inputManager.OnBasicAttack += OnBasicAttack;
	}

    void Update()
    {
        _count++;
        if (_count >= FRAME_BUFFER / 2)
        {
            _spriteRenderer.enabled = false;
            _attackHitBox.gameObject.tag = "Untagged";
        }
    }

    void OnBasicAttack()
    {
        if (_count >= FRAME_BUFFER)
        {
            _spriteRenderer.enabled = true;
            _attackHitBox.gameObject.tag = "IsActive";
            _count = 0;
        }
        _audioSource.Play();
    }
}
