using UnityEngine;

public class Shaft : MonoBehaviour
{
    [SerializeField] private Node elevatorCollectNode;
    [SerializeField] private BuyShaftUI BuyShaftUI;
    [SerializeField] private UpgradeActorUI UpgradeActorUI;

    public Transform NextShaftTransform { get; set; }
    public ShaftManager Manager { get; private set; }

    public void Initialize(Actor elevator, ShaftManager shaftManager, FinanceManager financeManager, int shaftsAmount)
    {
        elevator.AddNode(elevatorCollectNode);
        
        Manager = shaftManager;
        BuyShaftUI.FinanceManager = financeManager;
        UpgradeActorUI.FinanceManager = financeManager;
        UpgradeActorUI.ShaftManager = shaftManager;

        if (shaftManager.MaxShafts <= shaftsAmount)
        {
            BuyShaftUI.gameObject.SetActive(false);
        }
    }
}