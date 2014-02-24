using UnityEngine;
using System.Collections;

public class UnitSelect : MonoBehaviour {

	public bool selected;
	
	private void OnSelected()
	{
		selected = true;
		gameObject.GetComponentInChildren<Light>().enabled = true;
		//renderer.material.color = Color.red;
	}
	
	private void OnUnselected()
	{
		selected = false;
		//renderer.material.color = Color.white;
		gameObject.GetComponentInChildren<Light>().enabled = false;
	}
}
