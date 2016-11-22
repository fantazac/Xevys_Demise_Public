using UnityEngine;
using System.Collections;

public class UnityTags : MonoBehaviour
{

    public string Player { get; private set; }
    public string BasicAttackHitbox { get; private set; }
    public string Knife { get; private set; }
    public string Axe { get; private set; }
    public string Water { get; private set; }
    public string FlyingPlatform { get; private set; }
    public string Wall { get; private set; }
    public string KnifeDrop { get; private set; }
    public string AxeDrop { get; private set; }
    public string BackgroundCam { get; private set; }
    public string Scarab { get; private set; }
    public string Bat { get; private set; }
    public string Skeltal { get; private set; }
    public string AxeHandle { get; private set; }
    public string AxeBlade { get; private set; }
    public string EarthArtefactItem { get; private set; }
    public string AirArtefactItem { get; private set; }
    public string WaterArtefactItem { get; private set; }
    public string FireArtefactItem { get; private set; }
    public string FeatherItem { get; private set; }
    public string IronBootsItem { get; private set; }
    public string BubbleItem { get; private set; }
    public string FireProofArmorItem { get; private set; }
    public string BaseKnifeItem { get; private set; }
    public string BaseAxeItem { get; private set; }
    public string KnifePickableItem { get; private set; }
    public string AxePickableItem { get; private set; }
    public string LevelWall { get; private set; }
    public string Spike { get; private set; }

    private void Start()
    {
        Player = "Player";
        BasicAttackHitbox = "BasicAttackHitbox";
        Knife = "Knife";
        Axe = "Axe";
        Water = "Water";
        FlyingPlatform = "FlyingPlatform";
        Wall = "Wall";
        KnifeDrop = "KnifeDrop";
        AxeDrop = "AxeDrop";
        BackgroundCam = "BackgroundCam";
        Bat = "Bat";
        Skeltal = "Skeltal";
        Scarab = "Scarab";
        AxeHandle = "AxeHandle";
        AxeBlade = "AxeBlade";
        EarthArtefactItem = "EarthArtefactItem";
        AirArtefactItem = "AirArtefactItem";
        WaterArtefactItem = "WaterArtefactItem";
        FireArtefactItem = "FireArtefactItem";
        FeatherItem = "FeatherItem";
        IronBootsItem = "IronBootsItem";
        BubbleItem = "BubbleItem";
        FireProofArmorItem = "FireProofArmorItem";
        BaseKnifeItem = "BaseKnifeItem";
        BaseAxeItem = "BaseAxeItem";
        KnifePickableItem = "KnifePickableItem";
        AxePickableItem = "AxePickableItem";
        LevelWall = "LevelWall";
        Spike = "Spike";
    }
}
