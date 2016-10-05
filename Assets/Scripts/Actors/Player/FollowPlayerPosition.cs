using UnityEngine;
using System.Collections;

public class FollowPlayerPosition : MonoBehaviour
{

    [SerializeField]
    private GameObject _player;

    void Update()
    {
        if (_player != null)
        {
            GetComponent<Transform>().position = _player.GetComponent<Transform>().position;
        }
    }
}
