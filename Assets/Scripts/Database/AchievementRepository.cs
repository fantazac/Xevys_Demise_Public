using System;
using System.Data;

public class AchievementRepository : DatabaseConnection
{
    public void Add(AchievementEntity entity)
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("INSERT INTO ACHIEVEMENT (NAME, DESCRIPTION)" +
            " VALUES (\"{0}\", \"{1}\")", entity.Name, entity.Description);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();

        sqlQuery = String.Format("SELECT ACHIEVEMENT_ID" +
             " FROM ACHIEVEMENT WHERE NAME = \"{0}\"", entity.Name);
        _dbcommand.CommandText = sqlQuery;
        IDataReader reader = _dbcommand.ExecuteReader();
        while (reader.Read())
        {
            entity.AchievementId = reader.GetInt32(0);
        }
        reader.Close();
        _dbconnection.Close();
    }
}
