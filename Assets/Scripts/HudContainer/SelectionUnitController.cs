using UnityEngine;
using System.Collections;

public class SelectionUnitController : MonoBehaviour {
    
    CameraMarquee marquee;
    
    void Start () {
        marquee = Camera.main.GetComponent<CameraMarquee>();
    }
    
    void OnMouseDown() {
        if ( gameObject.name == "btn_all" )
            ;
        if ( gameObject.name == "btn_alpha" )
            ;
        if ( gameObject.name == "btn_unit_1" )
            ;
        if ( gameObject.name == "btn_unit_2" )
            ;
        if ( gameObject.name == "btn_unit_3" )
            ;
        if ( gameObject.name == "btn_unit_4" )
            ;
        
    }
}
