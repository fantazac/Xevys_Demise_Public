using UnityEngine;
using System.Collections;

public class RetractHoverPlatform : MonoBehaviour
{
    [SerializeField]
    private float _distanceToDrop = 0.5f;
    [SerializeField]
    private float _speed = 0.025f;

    private float _currentRelativeHeight = 0;

    public bool IsActivated { set; get; }

    private void Start()
    {
        IsActivated = false;
    }
    private void Update()
    {
        if (IsActivated)
        {
            if (_currentRelativeHeight < _distanceToDrop)
            {
                float currentRetract = _speed * Time.fixedDeltaTime;
                transform.position = new Vector3(transform.position.x, transform.position.y - currentRetract, transform.position.z);
                _currentRelativeHeight += currentRetract;
            }
            else
            {
                Destroy(gameObject);
            }  
        }
    }
}
