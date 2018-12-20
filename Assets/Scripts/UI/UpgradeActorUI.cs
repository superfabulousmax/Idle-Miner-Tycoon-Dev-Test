using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeActorUI : MonoBehaviour
{
    [SerializeField] private FinanceManager _financeManager;
    [SerializeField] private float _price;// this is upgrade price
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private Actor _actor;
    [SerializeField] private TextMeshProUGUI _upgradeAmount; // purchase
    [SerializeField] private TextMeshProUGUI _capacityAmount; // skill is capacity
    
    public ShaftManager ShaftManager;

    public FinanceManager FinanceManager
    {
        get { return _financeManager; }
        set { _financeManager = value; }
    }

    private void Start()
    {

         var shaftLevel = ShaftManager.Shafts.Count;
        _price *= Mathf.Pow(FinanceManager.Settings.ActorPriceIncrementPerShaft, shaftLevel);
        _actor.SkillMultiplier = Mathf.Pow(FinanceManager.Settings.ActorSkillIncrementPerShaft, shaftLevel);
        if (gameObject.tag == "Warehouse" || gameObject.tag == "Elevator")
        {
            int rounds = GameSaveDataController.GetComponentRounds(gameObject.tag);
            ResimUpgradeActor(rounds);
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        _purchaseButton.interactable = _price <= FinanceManager.TotalMoney;
        _upgradeAmount.text = Mathf.Round(_price).ToString();
        _capacityAmount.text = Mathf.Round(_actor.Settings.Skill * _actor.SkillMultiplier).ToString();
    }

    public void UpgradeActor()
    {
        _actor.LevelUp(FinanceManager.Settings);
        FinanceManager.UpdateMoney(-_price);
        _price *= FinanceManager.Settings.ActorUpgradePriceIncrement;
        UpdateUI();
        _purchaseButton.interactable = _price <= FinanceManager.TotalMoney;
        _upgradeAmount.text = Mathf.Round(_price).ToString();
        _capacityAmount.text = Mathf.Round(_actor.Settings.Skill * _actor.SkillMultiplier).ToString();

        if (gameObject.tag == "Warehouse")
        {
            GameSaveDataController.SetWarehouseState();
        }

        else if (gameObject.tag == "Elevator")
        {
            GameSaveDataController.SetElevatorState();
        }

        else if (GetGrandparentTag(this.transform) == "Shaft")
        {
            Shaft shaft = transform.parent.GetComponentInParent<Shaft>();
            if (shaft != null)
            {
                GameSaveDataController.SetShaftState(shaft);
            }
        }
    }

    public void ResimUpgradeActor(int rounds)
    {
        for(int i = 0; i < rounds; i++)
        {
            _actor.LevelUp(FinanceManager.Settings);
            _price *= FinanceManager.Settings.ActorUpgradePriceIncrement;    
        }
        UpdateUI();
        _upgradeAmount.text = Mathf.Round(_price).ToString();
        _capacityAmount.text = Mathf.Round(_actor.Settings.Skill * _actor.SkillMultiplier).ToString();
    }

    public static string GetGrandparentTag(Transform child)
    {
        Debug.Log("GetGrandparentTag for "+ child.name);
        if(child.parent == null)
        {
            return "";
        }

        if(child.parent.parent == null)
        {
            return "";
        }

        if (child.parent.parent)
        {
            return child.parent.parent.tag;
        }   
        return "";
    }

}