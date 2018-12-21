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
    [SerializeField] private float default_price;// this is upgrade price
    public ShaftManager ShaftManager;

    public FinanceManager FinanceManager
    {
        get { return _financeManager; }
        set { _financeManager = value; }
    }

    private void Start()
    {
        var shaftLevel = ShaftManager.Shafts.Count; // this should be shaftsInMine pos + 1 of shaft
        if (gameObject.tag == "Warehouse" || gameObject.tag == "Elevator")
        {
            shaftLevel = 1;
        }
        _price *= Mathf.Pow(FinanceManager.Settings.ActorPriceIncrementPerShaft, shaftLevel);
        Debug.Log("sdsdsd" + FinanceManager.Settings.ActorSkillIncrementPerShaft);
        _actor.SkillMultiplier = Mathf.Pow(FinanceManager.Settings.ActorSkillIncrementPerShaft, shaftLevel);
        if (gameObject.tag == "Warehouse" || gameObject.tag == "Elevator")
        {
            int rounds = GameSaveDataController.GetComponentRounds(gameObject.tag);
            ResimUpgradeActor(rounds, shaftLevel);
        }
        else if (GetGrandparentTag(this.transform) == "Shaft")
        {
            Shaft shaft = transform.parent.GetComponentInParent<Shaft>();
            int rounds = GameSaveDataController.GetShaftSaveData(shaft);
            int pos = GameSaveDataController.GetShaftPos(shaft);
            ResimUpgradeActor(rounds, pos);
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

    public void ResimUpgradeActor(int rounds, int pos)
    {
        var shaftLevel = pos + 1; // this should be shaftsInMine pos + 1 of shaft to reflect list size at this period
        Debug.Log("shaftLevel " + gameObject.name + " POS " + pos);
        if (gameObject.tag == "Warehouse" || gameObject.tag == "Elevator")
        {
            shaftLevel = 1;
        }

       float temp_price = default_price;
        temp_price *= Mathf.Pow(FinanceManager.Settings.ActorPriceIncrementPerShaft, shaftLevel);
        _actor.SkillMultiplier = Mathf.Pow(FinanceManager.Settings.ActorSkillIncrementPerShaft, shaftLevel);
        for (int i = 0; i < rounds; i++)
        {
            _actor.LevelUp(FinanceManager.Settings);
            temp_price *= FinanceManager.Settings.ActorUpgradePriceIncrement;    
        }
        UpdateUI();
        _upgradeAmount.text = Mathf.Round(temp_price).ToString();
        _capacityAmount.text = Mathf.Round(_actor.Settings.Skill * _actor.SkillMultiplier).ToString();
        _price = temp_price;
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