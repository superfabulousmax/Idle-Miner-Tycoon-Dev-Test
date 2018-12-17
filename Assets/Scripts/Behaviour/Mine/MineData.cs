using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MineData")]
public class MineData : GenericSingletonScriptableObject<MineData> {
    public double totalMoney;
    public int amountOfMineShafts;
}
