using System;
using UnityEngine;

public class AccountHasAchievementRepository : DatabaseConnection
{
    public void Add(int accountId, int achievementId)
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("INSERT INTO ACCOUNT_ACHIEVEMENT (ACCOUNT_ID, ACHIEVEMENT_ID)" +
            " VALUES ({0}, {1})", accountId, achievementId);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
    }
}
