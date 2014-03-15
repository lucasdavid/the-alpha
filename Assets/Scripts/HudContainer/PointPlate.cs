using UnityEngine;
using System;
using System.Collections;

public class PointPlate : MonoBehaviour {

    public GameObject shop;
    GameObject innerText;

    static bool tutorial;

    public void UpdateGraphics( int _points )
    {
        if (innerText == null) innerText = transform.FindChild("_base/text").gameObject;

        innerText.GetComponent<TextMesh>().text = "Brain points " + _points;
    }

    IEnumerator OnMouseOver()
    {
        if (tutorial == false)
        {
            tutorial = true;

            HintController hint = Camera.main.GetComponent<HintController>();

            hint.Add("You can open the shop clicking here or pressing the key O.");
            yield return new WaitForSeconds(1f);
            hint.Add("Use keys 1-4 to purchase units.");
            yield return new WaitForSeconds(1f);
            hint.Add("Click here or press the key O again in order to close the shop.");
        }
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
            transform
                .FindChild("_shop")
                .GetComponent<Shop>()
                .Interact();
    }
}
