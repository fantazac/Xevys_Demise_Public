using UnityEngine;
using System.Collections;

public class MoveEarthThorn : MonoBehaviour
{
    private const float SPEED = 2.5f;
    private float maximumHeight;
    private float initialHeight;

    private bool IsGoingUp { get; set; }
    private int VerticalDirection { get { return (IsGoingUp ? 1 : -1); } }

	private void Start ()
    {
        initialHeight = transform.position.y;
        maximumHeight = initialHeight + transform.parent.transform.localScale.y;
        IsGoingUp = true;
	}
	
	public void UpdateEarthThorn()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + VerticalDirection * Time.fixedDeltaTime * SPEED);
        if (transform.position.y >= maximumHeight)
        {
            IsGoingUp = false; 
        }
        else if (transform.position.y <= initialHeight)
        {
            Destroy(gameObject);
        }
	}
}
