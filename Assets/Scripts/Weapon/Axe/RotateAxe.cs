using UnityEngine;
using System.Collections;

public class RotateAxe : MonoBehaviour
{
    [SerializeField]
    private float _rotationByFrame = 10f;

    public bool Rotate { get; set; }

    private void Start()
    {
        _rotationByFrame *= transform.localScale.x;
        Rotate = true;
    }

    void Update()
    {
        if (Rotate)
        {
            if (transform.localScale.y > 0)
            {
                transform.Rotate(new Vector3(0, 0, -1 * _rotationByFrame));
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, _rotationByFrame));
            }
        }
    }
}
