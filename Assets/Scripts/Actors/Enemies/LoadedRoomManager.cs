using UnityEngine;
using System.Collections;

public class LoadedRoomManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _rooms;

    private int _currentRoom = 0;

    private void Start()
    {
        StaticObjects.GetMainCamera().GetComponent<CameraManager>().OnAreaChanged += LoadPlayerRoom;
    }

    private void LoadPlayerRoom(int roomId)
    {
        for(int i = 0; i < _rooms.Length; i++)
        {
            if(_currentRoom != i && roomId == i)
            {
                _rooms[_currentRoom].GetComponent<LoadEnemiesInRoom>().UnloadRoom();
                _rooms[i].SetActive(true);
                _currentRoom = i;
                break;
            }
        }
    }
}
