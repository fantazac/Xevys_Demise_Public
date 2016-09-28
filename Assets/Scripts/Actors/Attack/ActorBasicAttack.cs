using UnityEngine;
using System.Collections;

public class ActorBasicAttack : MonoBehaviour
{
    private InputManager _inputManager;
    private Rigidbody2D _rigidbody2D;

	void Start ()
	{
        _inputManager = GetComponent<InputManager>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

	    _inputManager.OnBasicAttack += OnAttack;
	}

	void Update ()
    {
	    
	}

    void OnAttack()
    {
        
    }
}
