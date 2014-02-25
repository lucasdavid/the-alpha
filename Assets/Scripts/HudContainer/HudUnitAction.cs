using UnityEngine;
using System.Collections;

public class HudUnitAction : MonoBehaviour {

    CameraMarquee marquee;
    
    void Start () {
        marquee = Camera.main.GetComponent<CameraMarquee>();
    }
    
    void OnMouseDown()
    {
        if ( gameObject.name == "sel_attack" )
            marquee.UnholdUnits();
        if ( gameObject.name == "sel_hold" )
            marquee.HoldUnits();
        if ( gameObject.name == "sel_move" )
            marquee.ResumeUnits();
        if ( gameObject.name == "sel_stop" )
            marquee.StopUnits();
    }
}
