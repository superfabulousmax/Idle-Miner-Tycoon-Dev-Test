using UnityEngine;

public class FinalInventory : Inventory
{
    [SerializeField] private FinanceManager financeManager;

    public override void AddResource(float amount)
    {
        money += amount;
        TransferMoney();
    }

    public void TransferMoney()
    {
        financeManager.UpdateMoney(DrawAll());
    }
}