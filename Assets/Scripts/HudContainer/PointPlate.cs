using UnityEngine;
using System.Collections;

public class PointPlate : MonoBehaviour {

    GameObject pointPlate;

	void Start ()
    {
        pointPlate = GameObject.Find("point-plate");
	}
}
