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

    void Start()
    {
        warehouse = GameObject.FindGameObjectWithTag("Warehouse");
        elevator = GameObject.FindGameObjectWithTag("Elevator");
        HasSavedMine = GameSaveDataController.GetMineState(this);
        RebuilSavedMine();
    }

    private void RebuilSavedMine()
    {
        shafts.Add(startShaft);
        if (HasSavedMine)
        {
            //GameSaveDataController.GetShaftSaveData(startShaft);
            financeManager.SetTotalMoney(800000);// GameSaveDataController.mineSaveData.totalMoney);
            if (GameSaveDataController.mineSaveData.shaftsInMine != null)
            {
                for (int s = 0; s < GameSaveDataController.mineSaveData.shaftsInMine.Count; s++)
                {
                    Debug.Log("s now " + s);
                    if (GameSaveDataController.mineSaveData.shaftsInMine[s].nextShaftUnlocked)
                    {
                        Debug.Log("saved shafts "+s + " shaft count " + shafts.Count);
                        shafts[s].ShaftManager.ResimBuildNextShaft();
                    }
                }
            }
            // need to first add shafts to work out upgrade and capacity per shaft level
            if (warehouse != null)
                warehouse.GetComponent<UpgradeActorUI>().ResimUpgradeActor(GameSaveDataController.mineSaveData.warehouseUpgradePressCount);
            if (elevator != null)
                elevator.GetComponent<UpgradeActorUI>().ResimUpgradeActor(GameSaveDataController.mineSaveData.elevatorUpgradePressCount);
        }

        else
        {
            GameSaveDataController.CreateStartShaftData(startShaft);
        }
    }
}
