using System;
using System.Collections.Generic;
using System.Data;

public class AccountHasAchievementRepository : DatabaseConnection
{
    public void Add(AccountHasAchievementEntity entity)
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("INSERT OR IGNORE INTO ACCOUNT_ACHIEVEMENT (ACCOUNT_ID, ACHIEVEMENT_ID)" +
            " VALUES ({0}, {1})", entity.AccountId, entity.AchievementId);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
    }

    public List<int> GetAllAchievementIdsFromAccount(int accountId)
    {
        List<int> achievementIds = new List<int>();
        _dbconnection.Open();
        string sqlQuery = String.Format("SELECT ACHIEVEMENT_ID " +
             "FROM ACCOUNT_ACHIEVEMENT WHERE ACCOUNT_ID = {0}", accountId);
        _dbcommand.CommandText = sqlQuery;
        IDataReader reader = _dbcommand.ExecuteReader();
        while (reader.Read())
        {
            achievementIds.Add(reader.GetInt32(0));
        }
        reader.Close();
        _dbconnection.Close();
        return achievementIds;
    }
}
