using UnityEngine;
using System.Collections;

public class EnableDoor : MonoBehaviour
{
    [SerializeField]
    private float _distanceToDrop = 4f;
    [SerializeField]
    private float _speed = 0.2f;

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
                float currentDescent = _speed * Time.fixedDeltaTime;
                transform.position = new Vector3(transform.position.x, transform.position.y - currentDescent, transform.position.z);
                _currentRelativeHeight += currentDescent;
            }
            else
            {
                IsActivated = false;
            }
        }
    }

}
