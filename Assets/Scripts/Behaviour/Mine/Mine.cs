using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private FinanceManager financeManager;
    public int mineNumber;
    [SerializeField] private GameSettings _gameSettings;
    public MineManager mineManager;
    public Shaft startShaft;
    public List<Shaft> shafts;
    public bool HasSavedMine { get; set; }
    public FinanceManager GetFinanceManager() { return financeManager; }
    private GameObject warehouse;
    private GameObject elevator;
    public bool canBuyNewMine = false;
    public bool hasNextMine = false;

    void Awake()
    {
       
        if (mineNumber + 1 < _gameSettings.saveDataNames.Length)
            canBuyNewMine = true;
        gameObject.name += mineNumber;
        warehouse = GameObject.FindGameObjectWithTag("Warehouse");
        elevator = GameObject.FindGameObjectWithTag("Elevator");
        HasSavedMine = GameSaveDataController.GetMineState(this);
        RebuildSavedMine();
        
    }

    private bool RebuildSavedMine()
    {
        Debug.Log("RebuildSavedMine");
        shafts.Add(startShaft);
        if (HasSavedMine)
        {
            Debug.Log("Has saved mine");
            // default start money is 620.64
            financeManager.SetTotalMoney(GameSaveDataController.mineSaveData[mineNumber].totalMoney);
            hasNextMine = GameSaveDataController.mineSaveData[mineNumber].nextMineUnlocked;
            if (GameSaveDataController.mineSaveData[mineNumber].shaftsInMine != null)
            {
                for (int s = 0; s < GameSaveDataController.mineSaveData[mineNumber].shaftsInMine.Count; s++)
                {
                    if (GameSaveDataController.mineSaveData[mineNumber].shaftsInMine[s].nextShaftUnlocked)
                    {
                        shafts[s].ShaftManager.ResimBuildNextShaft();
                    }
                }
            }
            // need to first add shafts to work out upgrade and capacity per shaft level
            if (warehouse != null)
                warehouse.GetComponent<UpgradeActorUI>().ResimUpgradeActor(GameSaveDataController.mineSaveData[mineNumber].warehouseUpgradePressCount, 0);
            if (elevator != null)
                elevator.GetComponent<UpgradeActorUI>().ResimUpgradeActor(GameSaveDataController.mineSaveData[mineNumber].elevatorUpgradePressCount, 0);
            return true;
        }

        else
        {
            //GameSaveDataController.CreateStartShaftData(startShaft);
            GameSaveDataController.CreateNewMineData(gameObject.GetComponent<Mine>(), startShaft);
            GameSaveDataController.mineSaveData[mineNumber].hasSavedMine = true;
            return false;
        }
    }
}
