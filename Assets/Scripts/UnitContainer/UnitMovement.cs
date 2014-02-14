using UnityEngine;
using System.Collections;

public class UnitMovement : MonoBehaviour {

	Vector3 target;
	bool aiming;
	NavMeshAgent agent;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}
	
	void Update ()
	{
		if (aiming) 
		{
			if ( aiming ) {
				agent.destination = target;

				// keep aiming until distance less than .1f
				aiming = Vector3.Distance(target, transform.position) > 1.0f;
			}
		}
	}

	public void Move(Vector3 _target)
	{
		target = _target;
		transform.rotation = Quaternion.LookRotation(target - transform.position);
		aiming = true;
	}
}
