using System;
using UnityEngine;

public class FunFactRepository : DatabaseConnection
{
    public void Add(FunFactEntity entity)
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("INSERT INTO FUN_FACT (DESCRIPTION)" +
            "VALUES (\"{0}\")", entity.Description);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
    }
}
