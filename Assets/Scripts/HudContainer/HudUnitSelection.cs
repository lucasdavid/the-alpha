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
                if (selection[i].name != "BasicZombie(Clone)") {
                    selection.RemoveAt(i);
                    i--;
                }
            }
        }
        else if ( gameObject.name == "btn_unit_2" ) {
            selection.AddRange( GameObject.FindGameObjectsWithTag("selectable") );
            
            for ( int i = 0; i < selection.Count; i++ ) {
                if (selection[i].name != "ScoutZombie(Clone)") {
                    selection.RemoveAt(i);
                    i--;
                }
            }
        }
        else if ( gameObject.name == "btn_unit_3" ) {
            selection.AddRange( GameObject.FindGameObjectsWithTag("selectable") );
            
            for ( int i = 0; i < selection.Count; i++ ) {
                if (selection[i].name != "SoldierZombie(Clone)") {
                    selection.RemoveAt(i);
                    i--;
                }
            }
        }
        else if ( gameObject.name == "btn_unit_4" ) {
            selection.AddRange( GameObject.FindGameObjectsWithTag("selectable") );
            
            for ( int i = 0; i < selection.Count; i++ ) {
                if (selection[i].name != "TankZombie(Clone)") {
                    selection.RemoveAt(i);
                    i--;
                }
            }
        }
        
        marquee.SelectUnits( selection.ToArray() );

        selectionLight.transform.position = gameObject.transform.position + Vector3.back;
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
