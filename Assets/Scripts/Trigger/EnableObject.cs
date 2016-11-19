using UnityEngine;
using System.Collections;

public class EnableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectToEnable;

    private void Start()
    {
        GetComponent<MoveObjectOnTrigger>().OnFinishedMoving += Enable;
    }

    public void Enable()
    {
        _objectToEnable.SetActive(true);
    }

}
