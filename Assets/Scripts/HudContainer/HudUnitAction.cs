using UnityEngine;
using System.Collections;

public class HudUnitAction : MonoBehaviour {

    CameraMarquee marquee;
    
    void Start () {
        marquee = Camera.main.GetComponent<CameraMarquee>();
    }
    
    void OnMouseDown() {
        if ( gameObject.name == "sel_attack" )
            ;
        if ( gameObject.name == "sel_hold" )
            ;
        if ( gameObject.name == "sel_move" )
            ;
        if ( gameObject.name == "sel_stop" )
            marquee.StopUnits();
    }
}
