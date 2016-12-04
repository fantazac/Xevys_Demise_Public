using System;
using System.Collections.Generic;
using System.Data;

public class AccountRepository : DatabaseConnection
{
    public AccountEntity Get(string username)
    {
        AccountEntity entity = new AccountEntity();
        _dbconnection.Open();
        string sqlQuery = String.Format("SELECT ACCOUNT_ID" +
             " FROM ACCOUNT WHERE USERNAME = \"{0}\"", username);
        _dbcommand.CommandText = sqlQuery;
        IDataReader reader = _dbcommand.ExecuteReader();
        while (reader.Read())
        {
            entity.AccountId = reader.GetInt32(0);
        }
        reader.Close();
        _dbconnection.Close();
        return entity;
    }

    public List<AccountEntity> GetAll()
    {
        List<AccountEntity> entities = new List<AccountEntity>();
        _dbconnection.Open();
        string sqlQuery = String.Format("SELECT USERNAME" +
             " FROM ACCOUNT");
        _dbcommand.CommandText = sqlQuery;
        IDataReader reader = _dbcommand.ExecuteReader();
        while (reader.Read())
        {
            AccountEntity entity = new AccountEntity();
            entity.Username = reader.GetString(0);
            entities.Add(entity);
        }
        reader.Close();
        _dbconnection.Close();
        return entities;
    }

    public void Add(AccountEntity entity)
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("INSERT INTO ACCOUNT (USERNAME)" +
            "VALUES (\"{0}\")", entity.Username);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();

        sqlQuery = String.Format("SELECT ACCOUNT_ID" +
            " FROM ACCOUNT WHERE USERNAME = \"{0}\"", entity.Username);
        _dbcommand.CommandText = sqlQuery;
        IDataReader reader = _dbcommand.ExecuteReader();
        while (reader.Read())
        {
            entity.AccountId = reader.GetInt32(0);
        }
        reader.Close();
        _dbconnection.Close();
    }
}
