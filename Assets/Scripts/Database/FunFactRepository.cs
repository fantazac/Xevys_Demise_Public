using System;
using System.Data;

public class FunFactRepository : DatabaseConnection
{
    public void Add(FunFactEntity entity)
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("INSERT INTO FUN_FACT (DESCRIPTION)" +
            "VALUES (\"{0}\")", entity.Description);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();

        sqlQuery = String.Format("SELECT ACHIEVEMENT_ID" +
             " FROM ACHIEVEMENT WHERE DESCRIPTION = \"{0}\"", entity.Description);
        _dbcommand.CommandText = sqlQuery;
        IDataReader reader = _dbcommand.ExecuteReader();
        while (reader.Read())
        {
            entity.FunFactId = reader.GetInt32(0);
        }
        reader.Close();
        _dbconnection.Close();
    }
}
