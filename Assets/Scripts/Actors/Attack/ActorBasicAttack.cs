using UnityEngine;
using System.Collections;

public class ActorBasicAttack : MonoBehaviour
{
    private const int FRAME_BUFFER = 30;

    private InputManager _inputManager;
    private SpriteRenderer _spriteRenderer;
    private int _count;

	void Start ()
	{
        _inputManager = GetComponent<InputManager>();
	    _spriteRenderer = GameObject.Find("CharacterBasicAttackBoxSprite").GetComponent<SpriteRenderer>();
	    _count = FRAME_BUFFER;

	    _inputManager.OnBasicAttack += OnBasicAttack;
	}

    void Update()
    {
        _count++;
        if (_count >= FRAME_BUFFER / 2)
        {
            _spriteRenderer.enabled = false;
        }
    }

    void OnBasicAttack()
    {
        if (_count >= FRAME_BUFFER)
        {
            _spriteRenderer.enabled = true;
            _count = 0;
        }          
    }
}
