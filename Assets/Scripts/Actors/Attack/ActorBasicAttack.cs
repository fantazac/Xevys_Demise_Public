using UnityEngine;
using System.Collections;

public class ActorBasicAttack : MonoBehaviour
{
    private const int FRAME_BUFFER = 30;

    private InputManager _inputManager;
    private SpriteRenderer _spriteRenderer;
    private GameObject _attackHitBox;
    private int _count;
    private bool _isActive;

	void Start ()
	{
        _inputManager = GetComponent<InputManager>();
	    _spriteRenderer = GameObject.Find("CharacterBasicAttackBoxSprite").GetComponent<SpriteRenderer>();
        _attackHitBox = GameObject.Find("CharacterBasicAttackBox");
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
    }
}
