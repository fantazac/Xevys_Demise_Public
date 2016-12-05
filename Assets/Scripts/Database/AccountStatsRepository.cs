using System;
using System.Data;

public class AccountStatsRepository : DatabaseConnection
{
    public AccountStatsEntity Get(int accountId)
    {
        AccountStatsEntity entity = new AccountStatsEntity();
        _dbconnection.Open();
        string sqlQuery = String.Format("SELECT SECONDS_PLAYED, NB_KILLED_SCARABS, NB_KILLED_BATS, NB_KILLED_SKELTALS, LIFE_REMAINING, KNIFE_PICKED, KNIFE_AMMO, AXE_PICKED, AXE_AMMO, FEATHER_PICKED, BOOTS_PICKED, BUBBLE_PICKED, ARMOR_PICKED, ARTEFACT1_PICKED, ARTEFACT2_PICKED, ARTEFACT3_PICKED, ARTEFACT4_PICKED" +
            " FROM STATS WHERE ACCOUNT_ID = {0}", accountId);
        _dbcommand.CommandText = sqlQuery;
        IDataReader reader = _dbcommand.ExecuteReader();
        while (reader.Read())
        {
            entity.SecondsPlayed = reader.GetInt32(0);
            entity.NbScarabsKilled = reader.GetInt32(1);
            entity.NbBatsKilled = reader.GetInt32(2);
            entity.NbSkeltalsKilled = reader.GetInt32(3);
            entity.LifeRemaining = reader.GetInt32(4);
            entity.KnifeEnabled = Convert.ToBoolean(reader.GetInt32(5));
            entity.KnifeAmmo = reader.GetInt32(6);
            entity.AxeEnabled = Convert.ToBoolean(reader.GetInt32(7));
            entity.AxeAmmo = reader.GetInt32(8);
            entity.FeatherEnabled = Convert.ToBoolean(reader.GetInt32(9));
            entity.BootsEnabled = Convert.ToBoolean(reader.GetInt32(10));
            entity.BubbleEnabled = Convert.ToBoolean(reader.GetInt32(11));
            entity.ArmorEnabled = Convert.ToBoolean(reader.GetInt32(12));
            entity.EarthArtefactEnabled = Convert.ToBoolean(reader.GetInt32(13));
            entity.AirArtefactEnabled = Convert.ToBoolean(reader.GetInt32(14));
            entity.WaterArtefactEnabled = Convert.ToBoolean(reader.GetInt32(15));
            entity.FireArtefactEnabled = Convert.ToBoolean(reader.GetInt32(16));
        }
        reader.Close();
        _dbconnection.Close();
        entity.AccountId = accountId;
        return entity;
    }

    public void Add(AccountStatsEntity entity)
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("INSERT INTO STATS (SECONDS_PLAYED, NB_KILLED_SCARABS, NB_KILLED_BATS, NB_KILLED_SKELTALS, LIFE_REMAINING, NB_DEATHS, KNIFE_PICKED, KNIFE_AMMO, AXE_PICKED, AXE_AMMO, FEATHER_PICKED, BOOTS_PICKED, BUBBLE_PICKED, ARMOR_PICKED, ARTEFACT1_PICKED, ARTEFACT2_PICKED, ARTEFACT3_PICKED, ARTEFACT4_PICKED, GAME_COMPLETED, ACCOUNT_ID)" +
            "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19})",
            entity.SecondsPlayed, entity.NbScarabsKilled, entity.NbBatsKilled, entity.NbSkeltalsKilled, entity.LifeRemaining, entity.NbDeaths, Convert.ToInt32(entity.KnifeEnabled), entity.KnifeAmmo, Convert.ToInt32(entity.AxeEnabled), entity.AxeAmmo, Convert.ToInt32(entity.FeatherEnabled), Convert.ToInt32(entity.BootsEnabled), Convert.ToInt32(entity.BubbleEnabled), Convert.ToInt32(entity.ArmorEnabled), Convert.ToInt32(entity.EarthArtefactEnabled), Convert.ToInt32(entity.AirArtefactEnabled), Convert.ToInt32(entity.WaterArtefactEnabled), Convert.ToInt32(entity.FireArtefactEnabled), Convert.ToInt32(entity.GameCompleted), entity.AccountId);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
    }

    public void UpdateEntity(AccountStatsEntity entity)
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("UPDATE STATS SET SECONDS_PLAYED = {0}, NB_KILLED_SCARABS = {1}, NB_KILLED_BATS = {2}, NB_KILLED_SKELTALS = {3}, LIFE_REMAINING = {4}, NB_DEATHS = {5}, KNIFE_PICKED = {6}, KNIFE_AMMO = {7}, AXE_PICKED = {8}, AXE_AMMO = {9}, FEATHER_PICKED = {10}, BOOTS_PICKED = {11}, BUBBLE_PICKED = {12}, ARMOR_PICKED = {13}, ARTEFACT1_PICKED = {14}, ARTEFACT2_PICKED = {15}, ARTEFACT3_PICKED = {16}, ARTEFACT4_PICKED = {17}, GAME_COMPLETED = {18} WHERE ACCOUNT_ID = {19}",
            entity.SecondsPlayed, entity.NbScarabsKilled, entity.NbBatsKilled, entity.NbSkeltalsKilled, entity.LifeRemaining, entity.NbDeaths, Convert.ToInt32(entity.KnifeEnabled), entity.KnifeAmmo, Convert.ToInt32(entity.AxeEnabled), entity.AxeAmmo, Convert.ToInt32(entity.FeatherEnabled), Convert.ToInt32(entity.BootsEnabled), Convert.ToInt32(entity.BubbleEnabled), Convert.ToInt32(entity.ArmorEnabled), Convert.ToInt32(entity.EarthArtefactEnabled), Convert.ToInt32(entity.AirArtefactEnabled), Convert.ToInt32(entity.WaterArtefactEnabled), Convert.ToInt32(entity.FireArtefactEnabled), Convert.ToInt32(entity.GameCompleted), entity.AccountId);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
    }
}
