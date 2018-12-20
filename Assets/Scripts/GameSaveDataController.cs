using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using System;

/**
 * Saves the state of the game
 * @author Sinead Urisohn
 * 
 * */
public class GameSaveDataController : MonoBehaviour {

    public static MineSaveData mineSaveData;
    // @todo mineractor UI upgrade and capacity save
    private static AllSettings settings;
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private Mine currentMine;
    [SerializeField] private bool isDebug = true;
    [SerializeField] private ActorSettings _minerSettings;
    private static string saveKeyName = "MineData";

    private void Awake()
    {
        var allSettings = new AllSettings()
        {
            gameSettings = _gameSettings,
            minerSettings = _minerSettings
        };

        LoadSavedData();
    }

    private void OnDestroy()
    {
        SaveGameData();
    }

    private void OnDisable()
    {
        SaveGameData();
    }

    public void SaveGameData()
    {
        var playerPrefData = JsonUtility.ToJson(mineSaveData);
        PlayerPrefs.SetString(saveKeyName, playerPrefData);
    }

    /**
     * Creates mine save data from json input
     **/
    public void LoadSavedData()
    {
        var playerPrefData = PlayerPrefs.GetString(saveKeyName);
        mineSaveData = JsonUtility.FromJson<MineSaveData>(playerPrefData);
        
        if(mineSaveData == null)
        {
            Debug.Log("No saved data\nCreating new saved data in key: " + saveKeyName);
            mineSaveData = new MineSaveData();
        }
        if(mineSaveData == null)
        {
            Debug.Log("Warning no mine save data found!!!");
        }
    }

    [MenuItem("Tools/Clear PlayerPrefs")]
    private static void ClearSavedData()
    {
        PlayerPrefs.DeleteKey(saveKeyName);
        Debug.Log("Cleared player data for " + saveKeyName);
    }


    public static void SetBuyNextShaftState(Shaft shaft)
    {
        if (mineSaveData.shaftsInMine.Any(s => s.shaftId == shaft.name))
        {
            ShaftSaveData matchingShaftData = mineSaveData.shaftsInMine
            .Where(s => s.shaftId == shaft.name).First();
            matchingShaftData.nextShaftUnlocked = true;
        }
    }

    public static void CreateStartShaftData(Shaft start)
    {
        Debug.Log("creating START shaft data for " + start.name);
        var shaftData = new ShaftSaveData()
        {
            shaftId = start.name,
            shaftUpgradePressCount = 0,
            nextShaftUnlocked = false
        };
        mineSaveData.shaftsInMine.Add(shaftData);
    }

    public static int GetShaftSaveData(Shaft shaft)
    {
        if (mineSaveData.shaftsInMine.Any(s => s.shaftId == shaft.name))
        {
            ShaftSaveData matchingShaftData = mineSaveData.shaftsInMine
            .Where(s => s.shaftId == shaft.name).First();
           return matchingShaftData.shaftUpgradePressCount;
        }
        return 0;
    }

    public static int GetComponentRounds(string tag)
    {
        if(tag == "Elevator") return mineSaveData.elevatorUpgradePressCount;
        return mineSaveData.warehouseUpgradePressCount;
    }



    public static void SetShaftState(Shaft shaft)
    {
        if (mineSaveData.shaftsInMine.Any(s => s.shaftId == shaft.name))
        {
            ShaftSaveData matchingShaftData = mineSaveData.shaftsInMine
            .Where(s => s.shaftId == shaft.name).First();
            matchingShaftData.shaftUpgradePressCount += 1;
        }

        else
        {
            // Called from Upgrade Actor UI so press count
            // should be set to one when creating new shaft save data.
            Debug.Log("creating new shaft data for " + shaft.name);
            var shaftData = new ShaftSaveData()
            {
                shaftId = shaft.name,
                shaftUpgradePressCount = 0,
                nextShaftUnlocked = false
            };
            mineSaveData.shaftsInMine.Add(shaftData);
        }
    }

    public static void SetWarehouseState()
    {
        mineSaveData.warehouseUpgradePressCount += 1;
    }

    public static void SetElevatorState()
    {
        mineSaveData.elevatorUpgradePressCount += 1;
    }

    public static void SetMineState(Mine mine, double money, bool hasSavedMine)
    {
        mineSaveData.mineId = mine.name;
        mineSaveData.totalMoney = money;
        mineSaveData.hasSavedMine = hasSavedMine;

        if (mineSaveData.shaftsInMine == null)
        {
            mineSaveData.shaftsInMine = new List<ShaftSaveData>();
        }

        //if(mine.shafts != null)
        //{
        //    int shaftCount = mine.shafts.Count;
        //    for (int s = 0; s < shaftCount; s++)
        //    {         
        //        var shaftData = new ShaftSaveData()
        //        {
        //            shaftId = mine.shafts[s].name
        //        };
        //        mineSaveData.shaftsInMine.RemoveAll(ch => ch.shaftId == shaftData.shaftId);
        //        mineSaveData.shaftsInMine.Add(shaftData);
        //    }
        //}
    }

    public static bool GetMineState(Mine mine)
    {
        if (mineSaveData.shaftsInMine == null)
        {
            return false;
        }

        if (mineSaveData.hasSavedMine)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    void OnGUI()
    {
        if(isDebug)
        {
            //Fetch the PlayerPrefs settings and output them to the screen using Labels
            GUI.Label(new Rect(50, 100, 200, 900), "Name : " + PlayerPrefs.GetString(saveKeyName));
        }
       
    }

}
