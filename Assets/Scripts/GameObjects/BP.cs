using UnityEngine;
using System.Collections;

public class BP : MonoBehaviour {
	public static int _brainPoints;

	// Use this for initialization
	void Start () {
        _brainPoints = 0;
	}

	public static int BrainPoints {
		get { return _brainPoints; }
		set { _brainPoints = value; }
	} 

}
