using UnityEngine;

[CreateAssetMenu(menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    public float ShaftIncrement;

    public float ActorUpgradePriceIncrement;
    public float ActorUpgradeSkillIncrement;

    public float ActorPriceIncrementPerShaft;
    public float ActorSkillIncrementPerShaft;
}