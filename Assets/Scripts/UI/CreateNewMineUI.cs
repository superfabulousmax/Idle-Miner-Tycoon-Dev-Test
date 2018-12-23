using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateNewMineUI : MonoBehaviour {

    [SerializeField] private GameSettings settings;
    [SerializeField] private FinanceManager _financeFinanceManager;
    [SerializeField] private Mine mine;
    [SerializeField] private float _price;

    private void Save()
    {
        var playerPrefData = JsonUtility.ToJson(GameSaveDataController.mineSaveData[GameSaveDataController.currentMineIndex]);
        PlayerPrefs.SetString(GameSaveDataController.saveKeyName, playerPrefData);
    }

    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.N)) // to go to next mine from current mine
        {
            PlayerPrefs.Save();
            if (mine.hasNextMine)
            {
                if (GameSaveDataController.currentMineIndex == GameSaveDataController.totalNumberOfMines)
                    GameSaveDataController.currentMineIndex = 0;
                else
                    GameSaveDataController.currentMineIndex += 1;
            }

            else
            {
                GameSaveDataController.currentMineIndex = 0;
            }

            Save();
            SceneManager.LoadScene(GameSaveDataController.newMineSceneName[GameSaveDataController.currentMineIndex]);
            GameSaveDataController.UpdateSaveInfo();
        }

        if (!mine.canBuyNewMine)
            return;
        if (Input.GetKeyDown(KeyCode.B)) // to buy a new mine
        {
            if(!GameSaveDataController.mineSaveData[GameSaveDataController.currentMineIndex].nextMineUnlocked)
            {
                if(_price <= _financeFinanceManager.TotalMoney)
                {
                    GameSaveDataController.mineSaveData[GameSaveDataController.currentMineIndex].nextMineUnlocked = true;
                    mine.hasNextMine = true;

                    _financeFinanceManager.UpdateMoney(-_price);
                }
            }
        }

    }
}
