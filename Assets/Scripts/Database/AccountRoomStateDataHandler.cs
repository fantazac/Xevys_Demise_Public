using System;
using System.Collections.Generic;
using UnityEngine;

public class AccountRoomStateDataHandler : MonoBehaviour
{
    private AccountRoomStateEntity _entity;
    private AccountRoomStateRepository _repository;
    private List<TriggerRoomStateListener> _roomsWithTrigger;

    private void Start()
    {
        _entity = new AccountRoomStateEntity();
        _repository = new AccountRoomStateRepository();
        RoomsStateCollector.OnMainLevelStarted += LoadRoomsWithTrigger;
    }

    private void LoadRoomsWithTrigger()
    {
        _roomsWithTrigger = GameObject.Find(StaticObjects.GetMainObjects().RoomsStateCollector).GetComponent<RoomsStateCollector>().RoomCleanerTriggers;
    }

    public void CreateNewEntry(int accountId)
    {
        AccountRoomStateEntity entity = new AccountRoomStateEntity();
        entity.AccountId = accountId;
        _repository.Add(entity);
    }

    public void ChangeEntity(int accountId)
    {
        if (!GetComponent<DatabaseController>().IsGuest)
        {
            _entity = _repository.Get(accountId);
        }
        else
        {
            _entity = new AccountRoomStateEntity();    
        }
        ReloadRoomsState(_entity.RoomStates);
    }

    public void UpdateEntity()
    {
        _entity.RoomStates = GetAllRoomsState();
    }

    public void UpdateRepository()
    {
        _repository.UpdateEntity(_entity);
    }

    public string GetAllRoomsState()
    {
        string allRoomsState = "";
        foreach (TriggerRoomStateListener room in _roomsWithTrigger)
        {
            allRoomsState += Convert.ToInt32(room.State);
            allRoomsState += ',';
        }
        allRoomsState = allRoomsState.Remove(allRoomsState.Length - 1);
        return allRoomsState;
    }

    public void ReloadRoomsState(string roomStatesString)
    {
        string[] roomStates = roomStatesString.Split(',');
        for (int i = 0; i < roomStates.Length; i++)
        {
            if (roomStates[i] == "1")
            {
                _roomsWithTrigger[i].RoomCleaned();
                _roomsWithTrigger[i].RoomCleaner.ReactivateTriggers();
            }
        }
    }
}
