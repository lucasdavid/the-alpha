using UnityEngine;
using System;
using System.Collections;

public class PointPlate : MonoBehaviour {

    public GameObject [] number;
    public Sprite     [] sprites;

    public void UpdateGraphics( int _points )
    {
        number[0].GetComponent<SpriteRenderer>().sprite = sprites[ _points / 10 ];
        number[1].GetComponent<SpriteRenderer>().sprite = sprites[ _points % 10 ];

        number[0].GetComponent<Animator>().SetTrigger("AnimateNumber");
        number[1].GetComponent<Animator>().SetTrigger("AnimateNumber");
    }

}
