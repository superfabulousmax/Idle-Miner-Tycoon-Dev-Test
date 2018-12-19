using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AllSettings
{
    public GameSettings gameSettings;
    public ActorSettings minerSettings;
}

[System.Serializable]
public class MineSaveData
{
    public MineSaveData()
    {
        hasSavedMine = false;
        mineId = "Mine";
        totalMoney = 0;
        elevatorUpgradePressCount = 0;
        warehouseUpgradePressCount = 0;
        shaftsInMine = new List<ShaftSaveData>();
    }
    public bool hasSavedMine = false;
    public string mineId = "Mine";
    public double totalMoney = 0;
    public int elevatorUpgradePressCount = 0;
    public int warehouseUpgradePressCount = 0;
    public List<ShaftSaveData> shaftsInMine = new List<ShaftSaveData>(); // each mine needs a list of shaft saved data to rebuild the mine
}

[System.Serializable]
public class ShaftSaveData
{
    public ShaftSaveData()
    {
        shaftId = "Shaft";
        shaftUpgradePressCount = 0;
        nextShaftUnlocked = false;
    }
    public string shaftId;
    public int shaftUpgradePressCount = 0;
    public bool nextShaftUnlocked = false;
}

