using UnityEngine;
using System.Collections;

public class CharacterSpawn : MonoBehaviour {
	public Transform nodeJawn;
	public GameObject GenericCharacter, ScoutCharacter, TankCharacter;
	float cooldown = 2.0f;


	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		cooldown -= Time.deltaTime;

		// Figure out a cleaner way of doing this
		if (Input.GetKeyDown(KeyCode.Q) && cooldown <= 0){
			spawnStuff(GenericCharacter);
		}
		if (Input.GetKeyDown(KeyCode.W) && cooldown <= 0){
			spawnStuff(ScoutCharacter);
		}
		if (Input.GetKeyDown(KeyCode.E) && cooldown <= 0){
			spawnStuff(TankCharacter);
		}
	}

	void spawnStuff(GameObject mob) {
        Instantiate(mob, nodeJawn.transform.position, nodeJawn.transform.rotation);

		cooldown = 2.0f;
		Debug.Log ("Spawn!");
	}
}
