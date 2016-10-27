using UnityEngine;
using System.Collections;

public class WaterHitboxHeight : MonoBehaviour
{

    private float _height = 0;

    public float Height { get { return _height; } }

    private void Start()
    {
        _height = transform.position.y + 0.1f;
    }

}
