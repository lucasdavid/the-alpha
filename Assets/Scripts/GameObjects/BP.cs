using UnityEngine;
using System.Collections;

public class BP : MonoBehaviour {
	static int _brainPoints;

	void Start ()
    {
        _brainPoints = 0;
	}

	public static int BrainPoints
    {
		get { return _brainPoints; }
		set { _brainPoints = value; }
	}
}
