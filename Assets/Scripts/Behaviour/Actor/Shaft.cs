using UnityEngine;

public class Shaft : MonoBehaviour
{
    [SerializeField] private Node elevatorCollectNode;
    [SerializeField] private BuyShaftUI BuyShaftUI;
    [SerializeField] private UpgradeActorUI UpgradeActorUI;
    
    public ShaftManager ShaftManager;
    public Transform NextShaftTransform;
    
    public void Initialize(Actor elevator, FinanceManager financeManager, int shaftsAmount)
    {
        elevator.AddNode(elevatorCollectNode);
        
        BuyShaftUI.FinanceManager = financeManager;
        UpgradeActorUI.FinanceManager = financeManager;
        UpgradeActorUI.ShaftManager = ShaftManager;

        if (ShaftManager.MaxShafts <= shaftsAmount)
        {
            BuyShaftUI.gameObject.SetActive(false);
        }
    }
}