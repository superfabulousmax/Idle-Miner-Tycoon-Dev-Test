using UnityEngine;

[CreateAssetMenu(menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    public float ShaftIncrement;

    public float ActorUpgradePriceIncrement;
    public float ActorUpgradeSkillIncrement;

    public float ActorPriceIncrementPerShaft;
    public float ActorSkillIncrementPerShaft;
    // how many mines allowed to the player
    public string[] saveDataNames;
    public float startMoney;
}