using System.Collections.Generic;
using UnityEngine;

public class ShaftManager : MonoBehaviour
{
    [SerializeField] private FinanceManager financeManager;
    [SerializeField] private Actor elevator;
    [SerializeField] private GameSettings settings;
    [SerializeField] private GameObject shaftPrefab;

    public int MaxShafts;

    public List<Shaft> Shafts;

    public void BuildNextShaft()
    {
        var position = Shafts[Shafts.Count - 1].NextShaftTransform.position;
        var newObject = Instantiate(shaftPrefab, position, Quaternion.identity);
        
        var shaft = newObject.GetComponent<Shaft>();
        Shafts.Add(shaft);
        shaft.ShaftManager = this;
        shaft.Initialize(elevator, financeManager, Shafts.Count);

        financeManager.UpdateMoney(-financeManager.NextShaftPrice);
        financeManager.NextShaftPrice *= settings.ShaftIncrement;
    }
}