using UnityEngine;
using System.Collections;

public class UnitMovement : MonoBehaviour {

	public float speed;

	Vector3 target;
	bool aiming;
	CharacterController controller;

	void Start()
	{
		controller = GetComponent<CharacterController>();
	}
	
	void Update ()
	{
		if (aiming) 
		{
			controller.Move(
				Vector3.Normalize(target - transform.position) * speed * Time.deltaTime
			);

			// keep aiming until distance less than .1f
			aiming = Vector3.Distance(target, transform.position) > 1.0f;
		}
	}

	public void Move(Vector3 _target)
	{
		target = _target;
		transform.rotation = Quaternion.LookRotation(target - transform.position);
		aiming = true;
	}
}
