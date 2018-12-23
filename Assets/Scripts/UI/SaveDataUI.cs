using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataUI : MonoBehaviour {

	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Cleared all saved data");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerPrefs.Save();
            Debug.Log("Saved Mine Data");
        }

    }
}
