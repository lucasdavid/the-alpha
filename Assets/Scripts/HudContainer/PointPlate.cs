using UnityEngine;
using System;
using System.Collections;

public class PointPlate : MonoBehaviour {

    public GameObject [] number;
    public Sprite     [] sprites;
    public GameObject    shop;
    int previous = 0;

    public void UpdateGraphics( int _points )
    {
        previous = _points - 1;

        number[0].GetComponent<SpriteRenderer>().sprite = sprites[ _points / 10 ];
        number[1].GetComponent<SpriteRenderer>().sprite = sprites[ _points % 10 ];

        // Only animate tens place if it changes
        if (previous / 10 != _points / 10)
            number[0].GetComponent<Animator>().SetTrigger("AnimateNumber");

        number[1].GetComponent<Animator>().SetTrigger("AnimateNumber");
    }

    void OnMouseDown() {
        if (Input.GetMouseButton(0)) {
            shop.GetComponent<Animator>().SetTrigger("interact");
            Shop.open = !Shop.open;
        }
    }
}
