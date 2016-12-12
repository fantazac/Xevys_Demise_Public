public struct AccountStatsEntity
{
    public int AccountId { get; set; }
    public int SecondsPlayed { get; set; }
    public int LifeRemaining { get; set; }
    public int NbDeaths { get; set; }
    public bool GameCompleted { get; set; }
    public int NbScarabsKilled { get; set; }
    public int NbBatsKilled { get; set; }
    public int NbSkeltalsKilled { get; set; }

    public bool KnifeEnabled { get; set; }
    public int KnifeAmmo { get; set; }
    public bool AxeEnabled { get; set; }
    public int AxeAmmo { get; set; }
    public bool FeatherEnabled { get; set; }
    public bool BootsEnabled { get; set; }
    public bool BubbleEnabled { get; set; }
    public bool ArmorEnabled { get; set; }
    public bool EarthArtefactEnabled { get; set; }
    public bool AirArtefactEnabled { get; set; }
    public bool WaterArtefactEnabled { get; set; }
    public bool FireArtefactEnabled { get; set; }
}
