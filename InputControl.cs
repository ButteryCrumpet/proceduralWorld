using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour {

    public GameObject UI;
    private bool UIEnabled = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIEnabled)
            {
                UI.SetActive(false);
                UIEnabled = false;
            }
            else
            {
                UI.SetActive(true);
                UIEnabled = true;
            }
        }
    }
}
