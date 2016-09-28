using UnityEngine;
using System.Collections;

public class ActorBasicAttack : MonoBehaviour
{
    private InputManager _inputManager;
    private SpriteRenderer _spriteRenderer;
    private int _count = 30;

	void Start ()
	{
        _inputManager = GetComponent<InputManager>();
	    _spriteRenderer = GameObject.Find("CharacterBasicAttackBoxSprite").GetComponent<SpriteRenderer>();

	    _inputManager.OnBasicAttack += OnBasicAttack;
	}

    void Update()
    {
        _count++;
        if (_count >= 30)
        {
            _spriteRenderer.enabled = false;
        }
    }

    void OnBasicAttack()
    {
        if (_count >= 30)
        {
            _spriteRenderer.enabled = true;
            _count = 0;
        }          
    }
}
