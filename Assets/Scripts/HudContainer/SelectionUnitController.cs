﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionUnitController : MonoBehaviour {
    
    CameraMarquee marquee;
    
    void Start ()
    {
        marquee = Camera.main.GetComponent<CameraMarquee>();
    }

    void OnMouseDown()
    {
        StopCoroutine("SignalMouseClick");
        StartCoroutine("SignalMouseClick");
 
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
    }

    IEnumerator SignalMouseClick()
    {
        marquee.mouseIsBeingUsedByHUD = true;
        yield return new WaitForSeconds(.1f);
        marquee.mouseIsBeingUsedByHUD = false;
    }
}
