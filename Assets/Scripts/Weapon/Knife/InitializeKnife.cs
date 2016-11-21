using UnityEngine;
using System.Collections;

public class InitializeKnife : MonoBehaviour
{
    [SerializeField]
    private float _horizontalSpeed = 15f;

    private int _flipKnife = 0;

    private void Start()
    {
        _flipKnife = StaticObjects.GetPlayer().GetComponent<PlayerOrientation>().IsFacingRight ? 1 : -1;

        GetComponent<Rigidbody2D>().velocity = new Vector2(_flipKnife * _horizontalSpeed, 0);
        transform.localScale = new Vector2(transform.localScale.x * _flipKnife, transform.localScale.y);
    }
}
