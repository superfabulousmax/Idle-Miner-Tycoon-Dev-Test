using System.Collections.Generic;
using UnityEngine;

public class ShaftManager : MonoBehaviour
{
    [SerializeField] private Mine mine;
    [SerializeField] private Actor elevator;
    [SerializeField] private GameSettings settings;
    [SerializeField] private GameObject shaftPrefab;
    public int MaxShafts;

    private void Start()
    {
        if(mine == null)
        {
            Debug.Log("Mine not found");
        }
    }
    public void BuildNextShaft()
    {
        var position = mine.shafts[mine.shafts.Count - 1].NextShaftTransform.position;
        var newObject = Instantiate(shaftPrefab, position, Quaternion.identity);
        var shaft = newObject.GetComponent<Shaft>();
        shaft.name += mine.shafts.Count.ToString();
        mine.shafts.Add(shaft);
        shaft.ShaftManager = this;
        shaft.Initialize(elevator, mine.GetFinanceManager(), mine.shafts.Count);
        mine.GetFinanceManager().UpdateMoney(-mine.GetFinanceManager().NextShaftPrice);
        mine.GetFinanceManager().NextShaftPrice *= settings.ShaftIncrement;
        GameSaveDataController.SetShaftState(shaft);
    }

    public void ResimBuildNextShaft()
    {
        var position = mine.shafts[mine.shafts.Count - 1].NextShaftTransform.position;
        var newObject = Instantiate(shaftPrefab, position, Quaternion.identity);
        var shaft = newObject.GetComponent<Shaft>();
        shaft.name += mine.shafts.Count.ToString();
        mine.shafts.Add(shaft);
        shaft.ShaftManager = this;
        shaft.Initialize(elevator, mine.GetFinanceManager(), mine.shafts.Count);
        //mine.GetFinanceManager().UpdateMoney(-mine.GetFinanceManager().NextShaftPrice);
        mine.GetFinanceManager().NextShaftPrice *= settings.ShaftIncrement;
        Debug.Log("Rebuiltshaft " + shaft.name);
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
