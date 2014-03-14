using UnityEngine;
using System.Collections;

public class UnitSelect : MonoBehaviour {

	public bool selected;

	private void OnSelected()
	{
		selected = true;
        gameObject.GetComponentInChildren<HealthBar>().Enable();
	}
	
	private void OnUnselected()
	{
		selected = false;
        gameObject.GetComponentInChildren<HealthBar>().Disable();
	}
}
