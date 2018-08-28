using UnityEngine;

public class Shaft : MonoBehaviour
{
    [SerializeField] private Node elevatorCollectNode;
    [SerializeField] private BuyShaftUI BuyShaftUI;
    [SerializeField] private UpgradeActorUI UpgradeActorUI;
    [SerializeField] private ShaftManager shaftManager;

    public Transform NextShaftTransform;
    public ShaftManager Manager
    {
        get { return shaftManager; }
    }

    public void Initialize(Actor elevator, FinanceManager financeManager, int shaftsAmount)
    {
        elevator.AddNode(elevatorCollectNode);
        
        BuyShaftUI.FinanceManager = financeManager;
        UpgradeActorUI.FinanceManager = financeManager;
        UpgradeActorUI.ShaftManager = shaftManager;

        if (shaftManager.MaxShafts <= shaftsAmount)
        {
            BuyShaftUI.gameObject.SetActive(false);
        }
    }
}