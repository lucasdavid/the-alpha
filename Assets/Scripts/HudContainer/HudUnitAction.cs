using UnityEngine;
using System.Collections;

public class HudUnitAction : MonoBehaviour {

    CameraMarquee marquee;
    
    void Start () {
        marquee = Camera.main.GetComponent<CameraMarquee>();
    }

    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.U) )
            marquee.UnholdUnits();
        if ( Input.GetKeyDown(KeyCode.H) )
            marquee.HoldUnits();
        if ( Input.GetKeyDown(KeyCode.M) )
            marquee.ResumeUnits();
        if ( Input.GetKeyDown(KeyCode.S) )
            marquee.StopUnits();
    }
    
    void OnMouseDown()
    {
        StopCoroutine("SignalMouseOverHUD");
        StartCoroutine("SignalMouseOverHUD");

        if ( gameObject.name == "sel_attack" )
            marquee.UnholdUnits();
        if ( gameObject.name == "sel_hold" )
            marquee.HoldUnits();
        if ( gameObject.name == "sel_move" )
            marquee.ResumeUnits();
        if ( gameObject.name == "sel_stop" )
            marquee.StopUnits();
    }

    /**
     * Signal marquee that the mouse is being used by the HUD and must be ignored.
     */
    IEnumerator SignalMouseOverHUD()
    {
        marquee.mouseIsBeingUsedByHUD = true;
        yield return new WaitForSeconds(.1f);
        marquee.mouseIsBeingUsedByHUD = false;
    }
}
