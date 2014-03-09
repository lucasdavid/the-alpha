using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HudUnitSelection : MonoBehaviour {
    
    static CameraMarquee marquee;
    static GameObject    selectionLight;
    
    void Start()
    {
        marquee        = Camera.main.GetComponent<CameraMarquee>();
        selectionLight = GameObject.Find("HUD/button-unit/light-selection");
    }

    public void OnMouseDown()
    {
        StopCoroutine ("SignalMouseOverHUD");
        StartCoroutine("SignalMouseOverHUD");

        List<GameObject> selection = new List<GameObject>();
        
        if ( gameObject.name == "btn_all" )
        {
            selection.AddRange( GameObject.FindGameObjectsWithTag("selectable") );
        }
        else if ( gameObject.name == "btn_alpha" )
        {
            selection.Add( Alpha.GetAlpha() );
        }
        else if ( gameObject.name == "btn_unit_1" )
        {
            selection.AddRange( GameObject.FindGameObjectsWithTag("selectable") );
            
            for ( int i = 0; i < selection.Count; i++ )
            {
                if (selection[i].name != "BasicZombie(Clone)")
                {
                    selection.RemoveAt(i);
                    i--;
                }
            }
        }
        else if ( gameObject.name == "btn_unit_2" )
        {
            selection.AddRange( GameObject.FindGameObjectsWithTag("selectable") );
            
            for ( int i = 0; i < selection.Count; i++ )
            {
                if (selection[i].name != "ScoutZombie(Clone)")
                {
                    selection.RemoveAt(i);
                    i--;
                }
            }
        }
        else if ( gameObject.name == "btn_unit_3" )
        {
            selection.AddRange( GameObject.FindGameObjectsWithTag("selectable") );
            
            for ( int i = 0; i < selection.Count; i++ )
            {
                if (selection[i].name != "SoldierZombie(Clone)")
                {
                    selection.RemoveAt(i);
                    i--;
                }
            }
        }
        else if ( gameObject.name == "btn_unit_4" )
        {
            selection.AddRange( GameObject.FindGameObjectsWithTag("selectable") );
            
            for ( int i = 0; i < selection.Count; i++ )
            {
                if (selection[i].name != "TankZombie(Clone)")
                {
                    selection.RemoveAt(i);
                    i--;
                }
            }
        }
        
        marquee.SelectUnits( selection.ToArray() );

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
