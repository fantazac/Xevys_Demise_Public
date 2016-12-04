using System;
using System.Data;
using UnityEngine;

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
