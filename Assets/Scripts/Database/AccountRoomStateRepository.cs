using System;
using System.Data;

public class AccountRoomStateRepository : DatabaseConnection
{
    public AccountRoomStateEntity Get(int accountId)
    {
        AccountRoomStateEntity entity = new AccountRoomStateEntity();
        entity.AccountId = accountId;
        _dbconnection.Open();
        string sqlQuery = String.Format("SELECT ROOM_STATES" +
            " FROM ROOM_STATE WHERE ACCOUNT_ID = {0}", accountId);
        _dbcommand.CommandText = sqlQuery;
        IDataReader reader = _dbcommand.ExecuteReader();
        while (reader.Read())
        {
            entity.RoomStates = reader.GetString(0);
        }
        reader.Close();
        _dbconnection.Close();
        entity.AccountId = accountId;
        return entity;
    }

    public void Add(AccountRoomStateEntity entity)
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("INSERT INTO ROOM_STATE (ROOM_STATES, ACCOUNT_ID)" +
            " VALUES (\"{0}\", {1})", entity.RoomStates, entity.AccountId);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
    }

    public void UpdateEntity(AccountRoomStateEntity entity)
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("UPDATE ROOM_STATE SET ROOM_STATES = \"{0}\"" +
            " WHERE ACCOUNT_ID = {1}", entity.RoomStates, entity.AccountId);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
    }
}
