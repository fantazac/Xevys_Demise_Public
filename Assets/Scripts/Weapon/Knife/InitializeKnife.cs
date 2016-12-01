using UnityEngine;
using System.Collections;

public class InitializeKnife : MonoBehaviour
{
    [SerializeField]
    private float _horizontalSpeed = 15f;

    [SerializeField]
    private float _waterSpeedModifier = 0.48f;

    private int _flipKnife = 0;

    private void Start()
    {
        _flipKnife = StaticObjects.GetPlayer().GetComponent<ActorOrientation>().Orientation;
        GetComponent<Rigidbody2D>().velocity += (Vector2.right * _horizontalSpeed * _flipKnife);
        if (StaticObjects.GetPlayer().GetComponent<PlayerWaterMovement>().enabled)
        {
            GetComponent<Rigidbody2D>().velocity *= _waterSpeedModifier;
        }
        transform.localScale = new Vector2(transform.localScale.x * _flipKnife, transform.localScale.y);
    }
}
