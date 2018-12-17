using System.Collections.Generic;
using UnityEngine;

public class ShaftManager : MonoBehaviour
{
    [SerializeField] private Mine mine;
    [SerializeField] private FinanceManager financeManager;
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
        shaft.Initialize(elevator, financeManager, mine.shafts.Count);
        financeManager.UpdateMoney(-financeManager.NextShaftPrice);
       
        financeManager.NextShaftPrice *= settings.ShaftIncrement;
    }

    private void Update()
    {
        mine.UpdateMineData(financeManager.TotalMoney);
    }

    public List<Shaft> Shafts
    {
        get { return mine.shafts; }
    }
}
