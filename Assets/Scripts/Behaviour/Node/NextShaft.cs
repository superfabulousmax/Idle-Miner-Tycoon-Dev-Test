using UnityEngine;

public class NextShaft : MonoBehaviour
{
    public Shaft shaft;

    public void BuyShaft()
    {
        shaft.ShaftManager.BuildNextShaft();
        // record buyinf of next shaft
        GameSaveDataController.SetBuyNextShaftState(shaft);
    }
}