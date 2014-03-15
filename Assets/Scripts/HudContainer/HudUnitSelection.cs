using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HudUnitSelection : MonoBehaviour {
    
    static HudController controller;

    public int id;
    
    void Start()
    {
        if ( controller == null ) controller = HudController.main.GetComponent<HudController>();
    }

    public void OnMouseDown()
    {
        controller.OnInnerButtonPressed(id);
    }

}
