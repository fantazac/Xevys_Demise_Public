using UnityEngine;
using System.Collections;

public class EnableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectToEnable;

    private MoveObjectOnTrigger _moveObjectOnTrigger;

    private void Start()
    {
        _moveObjectOnTrigger = GetComponent<MoveObjectOnTrigger>();
        _moveObjectOnTrigger.OnFinishedMoving += Enable;
    }

    public void Enable()
    {
        _objectToEnable.SetActive(true);
    }

}
