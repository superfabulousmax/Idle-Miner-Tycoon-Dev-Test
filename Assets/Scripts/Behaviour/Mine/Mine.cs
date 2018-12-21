using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private FinanceManager financeManager;
    public MineManager mineManager;
    public Shaft startShaft;
    public List<Shaft> shafts;
    public bool HasSavedMine { get; set; }
    public FinanceManager GetFinanceManager() { return financeManager; }
    private GameObject warehouse;
    private GameObject elevator;

    void Awake()
    {
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
            //GameSaveDataController.GetShaftSaveData(startShaft);
            // default start money is 620.64
            financeManager.SetTotalMoney(GameSaveDataController.mineSaveData.totalMoney);
            if (GameSaveDataController.mineSaveData.shaftsInMine != null)
            {
                for (int s = 0; s < GameSaveDataController.mineSaveData.shaftsInMine.Count; s++)
                {
                    if (GameSaveDataController.mineSaveData.shaftsInMine[s].nextShaftUnlocked)
                    {
                        shafts[s].ShaftManager.ResimBuildNextShaft();
                    }
                }
            }
            // need to first add shafts to work out upgrade and capacity per shaft level
            if (warehouse != null)
                warehouse.GetComponent<UpgradeActorUI>().ResimUpgradeActor(GameSaveDataController.mineSaveData.warehouseUpgradePressCount, 0);
            if (elevator != null)
                elevator.GetComponent<UpgradeActorUI>().ResimUpgradeActor(GameSaveDataController.mineSaveData.elevatorUpgradePressCount, 0);
            return true;
        }

        else
        {
            GameSaveDataController.CreateStartShaftData(startShaft);
            return false;
        }
    }
}
