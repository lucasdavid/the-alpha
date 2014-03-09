using UnityEngine;
using System.Collections;

public class HudController : MonoBehaviour {

    Light selection, action;

    void Start()
    {
        selection = transform.FindChild("button-unit/light-selection").GetComponent<Light>();
        action    = transform.FindChild("button-action/light-selection").GetComponent<Light>();
    }

    void Update()
    {
        // while shop is not opened and alternative key is not being pressed
        if ( ! Shop.open && ! Input.GetKey(Keymap.select.Alternative) )
        {
            GameObject current = null;

            if (Input.GetKeyDown(Keymap.select.Alpha))
                current = transform.FindChild("button-unit/btn_alpha").gameObject;
            if (Input.GetKeyDown(Keymap.select.Type0))
                current = transform.FindChild("button-unit/btn_unit_1").gameObject;
            if (Input.GetKeyDown(Keymap.select.Type1))
                current = transform.FindChild("button-unit/btn_unit_2").gameObject;
            if (Input.GetKeyDown(Keymap.select.Type2))
                current = transform.FindChild("button-unit/btn_unit_3").gameObject;
            if (Input.GetKeyDown(Keymap.select.Type3))
                current = transform.FindChild("button-unit/btn_unit_4").gameObject;
            if (Input.GetKeyDown(Keymap.select.All))
                current = transform.FindChild("button-unit/btn_all").gameObject;

            if ( current != null )
                current.GetComponent<HudUnitSelection>().OnMouseDown();
        }
    }

	public void CleanSelection()
    {
        selection.enabled = false;
    }

    public void CleanAction()
    {
        action.enabled = false;
    }

}
