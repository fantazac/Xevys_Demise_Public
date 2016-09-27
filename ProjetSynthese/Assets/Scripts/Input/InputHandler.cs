using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    public delegate void MovementInputHandler(Vector3 direction);
    public delegate void JumpInputHandler();

    public event MovementInputHandler OnMovement;
    public event JumpInputHandler OnJump;

	private void FixedUpdate () 
    {
	    HandleMovement();
	    HandleJump();
	}

    private void HandleMovement() 
    {
        Vector3 translation = Vector3.zero;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
            translation += Vector3.left;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            translation += Vector3.right;

        if (translation != Vector3.zero)
            if (OnMovement != null) OnMovement(translation);
    }

    private void HandleJump() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
            if (OnJump != null) OnJump();
    }
}
