using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;

/**
 * Saves the state of the game
 * Serves as a singleton for saving state between scene loads
 * @author Sinead Urisohn
 * 
 * */
public class GameSaveDataController : MonoBehaviour {

    public static GameSaveDataController uniqueInstance;
    public static MineSaveData[] mineSaveData;
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private bool isDebug = false;
    private static string[] _saveKeyNames;
    public static string[] newMineSceneName = { "Main", "Second Mine" };
    public static string saveKeyName;
    public static int currentMineIndex = 0;
    public static int totalNumberOfMines = 1;
    private static float startMoney;

    public static bool HasController()
    {
        return uniqueInstance != null;
    }

    private void Awake()
    {
        //singleton behaviour
        if(uniqueInstance != null)
        {
            // it already exists
            // so destroy this one
            Destroy(gameObject);
        }

        else
        {
            // create a new one
            uniqueInstance = this;
            // don't destroy when loading a new scene
            DontDestroyOnLoad(gameObject);
        }
        startMoney = _gameSettings.startMoney;
        _saveKeyNames = _gameSettings.saveDataNames;
        UpdateSaveInfo();
        mineSaveData = new MineSaveData[_saveKeyNames.Length];
        LoadSavedData();
    }

    public static void UpdateSaveInfo()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == newMineSceneName[0])
        {
            saveKeyName = _saveKeyNames[0];
            currentMineIndex = 0;
        }

        if (currentScene.name == newMineSceneName[1])
        {
            saveKeyName = _saveKeyNames[1];
            currentMineIndex = 1;
        }
    }

    private void OnDestroy()
    {
        SaveGameData();
    }

    private void OnDisable()
    {
        SaveGameData();
    }

    public static void SaveGameData()
    {
        var playerPrefData = JsonUtility.ToJson(mineSaveData[currentMineIndex]);
        PlayerPrefs.SetString(saveKeyName, playerPrefData);
    }

    /**
     * Creates mine save data from json input
     **/
    public void LoadSavedData()
    {
        var playerPrefData = PlayerPrefs.GetString(saveKeyName);
        mineSaveData[currentMineIndex] = JsonUtility.FromJson<MineSaveData>(playerPrefData);
        
        if(mineSaveData[currentMineIndex] == null)
        {
            Debug.Log("No saved data\nCreating new saved data in key: " + saveKeyName);
            MineSaveData newMineSaveData = new MineSaveData();
            mineSaveData[currentMineIndex] = newMineSaveData;
        }

        if(mineSaveData == null)
        {
            Debug.Log("Warning no mine save data found!!!");
        }
    }

    public static void CreateNewMineData(Mine mine, Shaft startShaft)
    {
        MineSaveData newMineSaveData = new MineSaveData();
        mineSaveData[currentMineIndex] = newMineSaveData;
        mineSaveData[currentMineIndex].hasSavedMine = true;
        mineSaveData[currentMineIndex].mineId = mine.name;
        mineSaveData[currentMineIndex].totalMoney = startMoney;
        CreateStartShaftData(startShaft);

    }

    [MenuItem("Tools/Clear PlayerPrefs")]
    private static void ClearSavedData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Cleared all saved data");
    }



    public static void SetBuyNextShaftState(Shaft shaft)
    {
        if (mineSaveData[currentMineIndex].shaftsInMine.Any(s => s.shaftId == shaft.name))
        {
            ShaftSaveData matchingShaftData = mineSaveData[currentMineIndex].shaftsInMine
            .Where(s => s.shaftId == shaft.name).First();
            matchingShaftData.nextShaftUnlocked = true;
        }
    }

    public static void CreateStartShaftData(Shaft start)
    {

        var shaftData = new ShaftSaveData()
        {
            shaftId = start.name,
            shaftUpgradePressCount = 0,
            nextShaftUnlocked = false
        };
        mineSaveData[currentMineIndex].shaftsInMine.Add(shaftData);
    }

    public static int GetShaftSaveData(Shaft shaft)
    {
        if (mineSaveData[currentMineIndex].shaftsInMine.Any(s => s.shaftId == shaft.name))
        {
            ShaftSaveData matchingShaftData = mineSaveData[currentMineIndex].shaftsInMine
            .Where(s => s.shaftId == shaft.name).First();
           return matchingShaftData.shaftUpgradePressCount;
        }
        return 0;
    }

    public static int GetShaftPos(Shaft shaft)
    {
        return mineSaveData[currentMineIndex].shaftsInMine.IndexOf(mineSaveData[currentMineIndex].shaftsInMine
            .Where(s => s.shaftId == shaft.name).First());
    }

    public static int GetComponentRounds(string tag)
    {
        if(tag == "Elevator") return mineSaveData[currentMineIndex].elevatorUpgradePressCount;
        return mineSaveData[currentMineIndex].warehouseUpgradePressCount;
    }



    public static void SetShaftState(Shaft shaft)
    {
        if (mineSaveData[currentMineIndex].shaftsInMine.Any(s => s.shaftId == shaft.name))
        {
            ShaftSaveData matchingShaftData = mineSaveData[currentMineIndex].shaftsInMine
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
            mineSaveData[currentMineIndex].shaftsInMine.Add(shaftData);
        }
    }

    public static void SetWarehouseState()
    {
        mineSaveData[currentMineIndex].warehouseUpgradePressCount += 1;
    }

    public static void SetElevatorState()
    {
        mineSaveData[currentMineIndex].elevatorUpgradePressCount += 1;
    }

    public static void SetMineState(Mine mine, double money, bool hasSavedMine)
    {
        mineSaveData[currentMineIndex].mineId = mine.name;
        mineSaveData[currentMineIndex].totalMoney = money;
        mineSaveData[currentMineIndex].hasSavedMine = hasSavedMine;

        if (mineSaveData[currentMineIndex].shaftsInMine == null)
        {
            mineSaveData[currentMineIndex].shaftsInMine = new List<ShaftSaveData>();
        }
    }

    public static bool GetMineState(Mine mine)
    {
        Debug.Log("GetMineState"+_saveKeyNames[currentMineIndex]);
        if (mineSaveData[currentMineIndex].hasSavedMine)
        {
            return true;
        }

        if (mineSaveData[currentMineIndex].shaftsInMine == null)
        {
            return false;
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
