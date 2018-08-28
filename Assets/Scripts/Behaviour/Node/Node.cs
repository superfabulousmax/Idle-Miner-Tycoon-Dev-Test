using System.Collections;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Inventory Inventory;

    [SerializeField] private bool load;
    [SerializeField] private float jobTime;

    public void Reach(Actor actor)
    {
        StartCoroutine(PutToWork(actor));
    }

    private IEnumerator PutToWork(Actor actor)
    {
        var targetCapacity = actor.Settings.Skill * actor.SkillMultiplier;
        var targetFreeCapacity = targetCapacity - actor.Inventory.Money;

        var amountTransfered = TransferResources(actor, targetFreeCapacity);
        var effortMultiplier = amountTransfered / targetCapacity;

        yield return new WaitForSeconds(jobTime * effortMultiplier);

        actor.TriggerWorking(false);
    }

    private float TransferResources(Actor actor, float amount)
    {
        float availableAmount;

        if (load)
        {
            availableAmount = Inventory.DrawResource(amount);
            actor.Inventory.AddResource(availableAmount);
        }
        else
        {
            availableAmount = actor.Inventory.DrawAll();
            Inventory.AddResource(availableAmount);
        }

        return availableAmount;
    }
}