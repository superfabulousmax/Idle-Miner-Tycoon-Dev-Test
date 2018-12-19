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

    void Start()
    {
        HasSavedMine = GameSaveDataController.GetMineState(this);
        RebuilSavedMine();
    }

    private void RebuilSavedMine()
    {
        shafts.Add(startShaft);
        if (HasSavedMine)
        {
            financeManager.SetTotalMoney(GameSaveDataController.mineSaveData.totalMoney);
            if (GameSaveDataController.mineSaveData.shaftsInMine != null)
            {
                for (int s = 0; s < GameSaveDataController.mineSaveData.shaftsInMine.Count; s++)
                {
                    if(GameSaveDataController.mineSaveData.shaftsInMine[s].nextShaftUnlocked)
                    {
                        if(shafts.Count <= s)
                            shafts[s - 1].ShaftManager.ResimBuildNextShaft();
                    }
                }
            }
        }
    }
}
