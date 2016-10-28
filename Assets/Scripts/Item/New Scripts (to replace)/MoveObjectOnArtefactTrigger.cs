using UnityEngine;
using System.Collections;

public class MoveObjectOnArtefactTrigger : MonoBehaviour
{
    private enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    [SerializeField]
    private GameObject _objectToMove;

    [SerializeField]
    private GameObject _triggerActivationObject;

    [SerializeField]
    private MoveDirection _moveDirection;

    [SerializeField]
    private float _distanceToMoveObject;

    [SerializeField]
    private float _speedInUnitsPerSecond;

    private ActivateArtefactTrigger _trigger;

    private void Start()
    {
        _trigger = _triggerActivationObject.GetComponent<ActivateArtefactTrigger>();
        _trigger.OnTrigger += MoveObject;
    }

    private void MoveObject()
    {
        //faire une cooroutine qui bouge l'objet avec un Time.deltaTime dans la bonne direction et appeler ObjectDoneMoving() quand c'est fini
    }

    private void ObjectDoneMoving()
    {

    }

}
