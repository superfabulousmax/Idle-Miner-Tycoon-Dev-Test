using UnityEngine;

public class NextShaft : MonoBehaviour
{
    public Shaft shaft;

    public void BuyShaft()
    {
        if(shaft == null)
        {
            if (UpgradeActorUI.GetGrandparentTag(this.transform) == "Shaft")
            {
                
                shaft = transform.parent.parent.GetComponentInParent<Shaft>();
                Debug.Log("Found shaft tag "+ shaft == null);
            }
        }

        if(shaft != null)
        {
            shaft.hasBoughtNextShaft = true;
            shaft.ShaftManager.BuildNextShaft();
            // record buyinf of next shaft
            GameSaveDataController.SetBuyNextShaftState(shaft);
           
        }

        else
        {
            Debug.Log("Shaft in NextShaft is null!!");
        }
        
    }
}