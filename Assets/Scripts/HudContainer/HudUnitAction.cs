using UnityEngine;
using System.Collections;

public class HudUnitAction : MonoBehaviour {

    static CameraMarquee marquee;
    static GameObject selectionLight;

    void Start ()
    {
        marquee = Camera.main.GetComponent<CameraMarquee>();
        selectionLight = GameObject.Find("HUD/button-action/light-selection");
    }

    void Update()
    {
        if ( Input.GetKeyDown(Keymap.action.Unhold) )
            marquee.UnholdUnits();
        if ( Input.GetKeyDown(Keymap.action.Hold) )
            marquee.HoldUnits();
        if ( Input.GetKeyDown(Keymap.action.Resume) )
            marquee.ResumeUnits();
        if ( Input.GetKeyDown(Keymap.action.Stop) )
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


        selectionLight.transform.position = new Vector3(
            transform.position.x,
            selectionLight.transform.position.y,
            selectionLight.transform.position.z
        );
        selectionLight.GetComponent<Light>().enabled = true;
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
