using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HudUnitSelection : MonoBehaviour {
    
    static CameraMarquee marquee;
    static GameObject selectionLight;
    
    void Start ()
    {
        marquee = Camera.main.GetComponent<CameraMarquee>();
        selectionLight = GameObject.Find("HUD/button-unit/light-selection");
    }

    void OnMouseDown()
    {
        StopCoroutine("SignalMouseOverHUD");
        StartCoroutine("SignalMouseOverHUD");
 
        List<GameObject> selection = new List<GameObject>();
        
        if ( gameObject.name == "btn_all" ) {
            selection.AddRange( GameObject.FindGameObjectsWithTag("selectable") );
        }
        else if ( gameObject.name == "btn_alpha" ) {
            selection.Add( GameObject.Find ("Alpha") );
        }
        else if ( gameObject.name == "btn_unit_1" ) {
            selection.AddRange( GameObject.FindGameObjectsWithTag("selectable") );
            
            for ( int i = 0; i < selection.Count; i++ ) {
                if (selection[i].name != "GenericCharacter") {
                    selection.RemoveAt(i);
                    i--;
                }
            }
        }
        else if ( gameObject.name == "btn_unit_2" ) {
            selection.AddRange( GameObject.FindGameObjectsWithTag("selectable") );
            
            for ( int i = 0; i < selection.Count; i++ ) {
                if (selection[i].name != "GenericCharacter") {
                    selection.RemoveAt(i);
                    i--;
                }
            }
        }
        else if ( gameObject.name == "btn_unit_3" ) {
            selection.AddRange( GameObject.FindGameObjectsWithTag("selectable") );
            
            for ( int i = 0; i < selection.Count; i++ ) {
                if (selection[i].name != "TankCharacter") {
                    selection.RemoveAt(i);
                    i--;
                }
            }
        }
        else if ( gameObject.name == "btn_unit_4" ) {
            selection.AddRange( GameObject.FindGameObjectsWithTag("selectable") );
            
            for ( int i = 0; i < selection.Count; i++ ) {
                if (selection[i].name != "ScoutCharacter") {
                    selection.RemoveAt(i);
                    i--;
                }
            }
        }
        
        marquee.SelectUnits( selection.ToArray() );

        StopCoroutine("ShowSelectionLight");
        StartCoroutine("ShowSelectionLight");
    }

    IEnumerator ShowSelectionLight()
    {
        Debug.Log("HudUnitAction@ShowSelectionLight");
        selectionLight.transform.position = gameObject.transform.position;
        selectionLight.GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(.5f);
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
