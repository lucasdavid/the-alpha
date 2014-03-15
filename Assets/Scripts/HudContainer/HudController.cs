using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HudController : MonoBehaviour {

    public static GameObject main;

    public GameObject UnitSelectionIconPrefab;
    public int CustomSelectionsCount;

    CameraMarquee marquee;
    Light selectionLight, action;

    List<GameObject>[] selectionsAvailable;

    void Start()
    {
        main      = gameObject;
        marquee   = Camera.main.GetComponent<CameraMarquee>();
        selectionLight = transform.FindChild("button-unit/light-selection").GetComponent<Light>();
        action    = transform.FindChild("button-action/light-selection").GetComponent<Light>();

        selectionsAvailable = new List<GameObject>[7];
    }

    void Update()
    {
        // while shop is not opened and alternative key is not being pressed
        if (!Shop.IsOpen())
        {
            // player is wants to get a selection
            if (!Input.GetKey(Keymap.select.Alternative))
            {
                if (Input.GetKeyDown(Keymap.select.Alpha))
                    SelectUnitAlpha();
                else if (Input.GetKeyDown(Keymap.select.All))
                    SelectUnitAll();

                else if (Input.GetKeyDown(Keymap.select.custom1))
                    SelectUnitCustom(0);
                else if (Input.GetKeyDown(Keymap.select.custom2))
                    SelectUnitCustom(1);
                else if (Input.GetKeyDown(Keymap.select.custom3))
                    SelectUnitCustom(2);
                else if (Input.GetKeyDown(Keymap.select.custom4))
                    SelectUnitCustom(3);
                else if (Input.GetKeyDown(Keymap.select.custom5))
                    SelectUnitCustom(4);
                else if (Input.GetKeyDown(Keymap.select.custom6))
                    SelectUnitCustom(5);
                else if (Input.GetKeyDown(Keymap.select.custom7))
                    SelectUnitCustom(6);
            }
            // player wants to record a selection
            else
            {
                if (Input.GetKey(Keymap.select.custom1))
                    CreateCustomSelection(0);
                else if (Input.GetKey(Keymap.select.custom2))
                    CreateCustomSelection(1);
                else if (Input.GetKey(Keymap.select.custom3))
                    CreateCustomSelection(2);
                else if (Input.GetKey(Keymap.select.custom4))
                    CreateCustomSelection(3);
                else if (Input.GetKey(Keymap.select.custom5))
                    CreateCustomSelection(4);
                else if (Input.GetKey(Keymap.select.custom6))
                    CreateCustomSelection(5);
                else if (Input.GetKey(Keymap.select.custom7))
                    CreateCustomSelection(6);
            }
        }
    }

    public void OnInnerButtonPressed( int _id )
    {
        StopCoroutine("SignalMouseOverHUD");
        StartCoroutine("SignalMouseOverHUD");

        if (_id == -2)
            SelectUnitAlpha();
        else if (_id == -1)
            SelectUnitAll();

        else
            SelectUnitCustom(_id);
    }

	public void CleanAction()
    {
        action.enabled = false;
    }

    public void CleanSelection()
    {
        selectionLight.enabled = false;
    }
    
    public void SelectUnitAlpha()
    {
        marquee.SelectUnits( Alpha.GetAlpha() );
    }

    public void SelectUnitAll()
    {
        marquee.SelectUnits( new List<GameObject>( GameObject.FindGameObjectsWithTag("selectable") ) );
    }

    public void SelectUnitCustom(int _id)
    {
        Debug.Log("HudController@SeleectUnitCustom : " + selectionsAvailable[_id]);
        marquee.SelectUnits(selectionsAvailable[_id]);
    }

    void CreateCustomSelection(int _id)
    {
        Debug.Log("CreateCustomSelection");
        selectionsAvailable[_id] = new List<GameObject>(marquee.SelectedUnits);
        
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
