using UnityEngine;
using System.Collections;

public class CheatTeleportation : MonoBehaviour
{

    [SerializeField]
    private bool _cheatsEnabled = false;

    [SerializeField]
    private Vector3[] _itemLocations;

    private GameObject _player;

    private void Start()
    {
        _player = StaticObjects.GetPlayer();
        _player.GetComponentInChildren<InputManager>().OnCheat += TeleportToItem;
    }

    private void TeleportToItem(int item)
    {
        if (_cheatsEnabled)
        {
            _player.transform.position = _itemLocations[item];
        }
    }
}
