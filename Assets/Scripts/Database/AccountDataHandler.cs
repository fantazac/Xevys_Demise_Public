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

    public void CreateNewAccount(string username)
    {
        _entity = new AccountEntity();
        _entity.Username = username;
        _repository.Add(_entity);
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



