using UnityEngine;
using System.Collections;

public class HudController : MonoBehaviour {

    Light selection, action;

    void Start()
    {
        selection = transform.FindChild("button-unit/light-selection").GetComponent<Light>();
        action    = transform.FindChild("button-action/light-selection").GetComponent<Light>();
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
