using System.Collections.Generic;
using UnityEngine;

public class AccountDataHandler : MonoBehaviour
{
    private AccountEntity _entity;
    private AccountRepository _repository;

    private void Start()
    {
        _repository = new AccountRepository();
    }

    public void CreateNewEntry(string username)
    {
        _entity = new AccountEntity();
        _entity.Username = username;
        _repository.Add(_entity);
    }

    public void ChangeEntity(string username)
    {
        _entity = _repository.Get(username);
        GetComponent<DatabaseController>().AccountId = _entity.AccountId;
    }

    public List<string> GetAllUsernames()
    {
        List<string> usernames = new List<string>();
        List<AccountEntity> entites = _repository.GetAll();
        foreach (AccountEntity entity in entites)
        {
            usernames.Add(entity.Username);
        }
        return usernames;
    }
}



