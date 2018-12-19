using UnityEngine;

public class Shaft : MonoBehaviour
{
    [SerializeField] private Node elevatorCollectNode;
    [SerializeField] private BuyShaftUI BuyShaftUI;
    [SerializeField] private UpgradeActorUI UpgradeActorUI;
    
    public ShaftManager ShaftManager;
    public Transform NextShaftTransform;

    private void Start()
    {
        int rounds = GameSaveDataController.GetShaftSaveData(this);
        UpgradeActorUI.ResimUpgradeActor(rounds);
    }
    
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

    //public float GetShaftSkillMultiplier()
    //{
    //    return UpgradeActorUI.GetActorSkillMultiplierForUI();
    //}

    //public float GetNextShaftPrice()
    //{
    //    return UpgradeActorUI.FinanceManager.NextShaftPrice;
    //}

    //public void SetNextShaftPrice(float nextPrice)
    //{
    //    UpgradeActorUI.FinanceManager.NextShaftPrice = nextPrice;
    //}

    //public float GetUpgradePrice()
    //{
    //    return UpgradeActorUI.GetUpgradePrice();
    //}


    //public void SetUpgradePrice(float price)
    //{
    //    UpgradeActorUI.SetUpgradePrice(price);
    //}

    //public float GetSkill()
    //{
    //    return UpgradeActorUI.GetSkill();
    //}

    //public void SetSkill(float skill)
    //{
    //    UpgradeActorUI.SetSkill(skill);
    //}

}