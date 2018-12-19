using System.Collections.Generic;
using UnityEngine;

public class ShaftManager : MonoBehaviour
{
    [SerializeField] private Mine mine;
    [SerializeField] private Actor elevator;
    [SerializeField] private GameSettings settings;
    [SerializeField] private GameObject shaftPrefab;
    public int MaxShafts;

    public void BuildNextShaft()
    {
        var position = mine.shafts[mine.shafts.Count - 1].NextShaftTransform.position;
        var newObject = Instantiate(shaftPrefab, position, Quaternion.identity);
        var shaft = newObject.GetComponent<Shaft>();
        mine.shafts.Add(shaft);
        shaft.ShaftManager = this;
        shaft.Initialize(elevator, mine.GetFinanceManager(), mine.shafts.Count);
        mine.GetFinanceManager().UpdateMoney(-mine.GetFinanceManager().NextShaftPrice);
        mine.GetFinanceManager().NextShaftPrice *= settings.ShaftIncrement;
    }

    public void ResimBuildNextShaft()
    {
        var position = mine.shafts[mine.shafts.Count - 1].NextShaftTransform.position;
        var newObject = Instantiate(shaftPrefab, position, Quaternion.identity);
        var shaft = newObject.GetComponent<Shaft>();
        mine.shafts.Add(shaft);
        shaft.ShaftManager = this;
        shaft.Initialize(elevator, mine.GetFinanceManager(), mine.shafts.Count);
        //mine.GetFinanceManager().UpdateMoney(-mine.GetFinanceManager().NextShaftPrice);
        mine.GetFinanceManager().NextShaftPrice *= settings.ShaftIncrement;
    }

    public float NextShaftPrice
    {
        get { return mine.GetFinanceManager().NextShaftPrice; }
    }


    private void Update()
    {
        GameSaveDataController.SetMineState(mine, mine.GetFinanceManager().TotalMoney, true);
    }

    public List<Shaft> Shafts
    {
        get { return mine.shafts; }
    }
}
