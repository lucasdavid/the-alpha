using UnityEngine;
using System;
using System.Collections;

public class Horde : MonoBehaviour {

    private static int _currentValue;          // How much value the player has on the field
    private static int _threatLevel;           // For determining how big of a threat the player is
    private static int _brainPoints;           // Money

    private static float _timer;

    static PointPlate pointPlate;

    public static int CurrentValue
    {
        get { return _currentValue; }
        set { _currentValue = value; }
    }

    public static int ThreatLevel
    {
        get { return _threatLevel; }
        set { _threatLevel = value; }
    }

    public static int BrainPoints
    {
        get { return _brainPoints; }
        set {
            _brainPoints = value;
            // update points in the HUD
            try
            {
                pointPlate.UpdateGraphics(_brainPoints);
            }
            catch ( NullReferenceException e )
            {
                pointPlate = GameObject.Find ("point-plate").GetComponent<PointPlate>();
                pointPlate.UpdateGraphics(_brainPoints);
            }
        }
    }

    // After each kill, reset ThreatLevel timer
    public static void ResetThreatTimer() {
        _timer = 5.0f;
    }

	// Use this for initialization
	void Start ()
    {
        _timer = 0;
        CurrentValue = 0;
        BrainPoints = 0;
        ThreatLevel = 0;
	}	

	// Update is called once per frame
	void Update ()
    {
        _timer -= Time.deltaTime;

        // Decrement ThreatLevel every 5 seconds while ThreatLevel > CurrentValue
        if ((ThreatLevel > CurrentValue) && (_timer < 0) && (ThreatLevel > 0))
        {
            _timer = 5.0f;
            ThreatLevel--;
        }

        // ThreatLevel shouldn't go below 0
        if (ThreatLevel < 0)
            ThreatLevel = 0;
	}

}
