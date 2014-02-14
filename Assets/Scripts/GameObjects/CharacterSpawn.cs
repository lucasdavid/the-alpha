using UnityEngine;
using System.Collections;

public class CharacterSpawn : MonoBehaviour {

	public enum type {
		generic, scout, tank
	};

	public GameObject spawnPoint;
	public GameObject[] characters;

	float cooldown = 2.0f;

	void Update ()
	{
		cooldown -= Time.deltaTime;

		// Figure out a cleaner way of doing this
		if ( Input.GetKeyDown(KeyCode.Q) && cooldown <= 0 )
			Spawn(characters[ ( int ) type.generic] );
		
		if ( Input.GetKeyDown(KeyCode.W) && cooldown <= 0 )
			Spawn(characters[ ( int ) type.scout] );

		if ( Input.GetKeyDown(KeyCode.E) && cooldown <= 0 )
			Spawn(characters[ ( int ) type.tank]);
	}

	void Spawn(GameObject mob)
	{
		Debug.Log ("CharacterSpawn@Spawn");

		Instantiate(mob, spawnPoint.transform.position, Quaternion.identity);
		cooldown = 2.0f;
	}
}
