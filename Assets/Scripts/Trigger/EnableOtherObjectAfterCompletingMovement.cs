using UnityEngine;
using System.Collections;

public class EnableOtherObjectAfterCompletingMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectToEnable;

    private void Start()
    {
        GetComponent<MoveObjectOnTrigger>().OnFinishedMoving += Enable;
    }

    private void Enable()
    {
        _objectToEnable.SetActive(true);
    }
}
