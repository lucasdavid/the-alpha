using UnityEngine;
using System.Collections;

public class HudUnitAction : MonoBehaviour {

    CameraMarquee marquee;
    
    void Start () {
        marquee = Camera.main.GetComponent<CameraMarquee>();
    }

    void Update()
    {
        if ( Input.GetKeyDown(Keymap.kmAction.Unhold) )
            marquee.UnholdUnits();
        if ( Input.GetKeyDown(Keymap.kmAction.Hold) )
            marquee.HoldUnits();
        if ( Input.GetKeyDown(Keymap.kmAction.Resume) )
            marquee.ResumeUnits();
        if ( Input.GetKeyDown(Keymap.kmAction.Stop) )
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
