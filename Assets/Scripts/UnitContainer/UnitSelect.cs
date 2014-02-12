using UnityEngine;
using System.Collections;

public class UnitSelect : MonoBehaviour {

	public bool selected;
	
	private void OnSelected()
	{
		selected = true;
		renderer.material.color = Color.red;
	}
	
	private void OnUnselected()
	{
		selected = false;
		renderer.material.color = Color.white;
	}
}
