using UnityEngine;
using System.Collections;

public class CharacterSpawn : MonoBehaviour
{
    public enum type {
        generic,
        scout,
        tank,
        enemy
    };

    public GameObject zombieSpawn;          // Where zombies spawn
    public GameObject humanSpawn;           // Where humans spawn
    public GameObject[] characters;         // Spawn array

    public int maxValue = 50;               // Character limit, based on unit's value
    public static int currentValue;         // How much value is currently on the field

    float cooldown = 2.0f;

    void Start () {
        currentValue = 0;
    }

    void Update ()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0) {
            // Figure out a cleaner way of doing this
            if (Input.GetKeyDown (KeyCode.Q))
                Spawn (characters [(int)type.generic]);
        
            if (Input.GetKeyDown (KeyCode.W))
                Spawn (characters [(int)type.scout]);

            if (Input.GetKeyDown (KeyCode.E))
                Spawn (characters [(int)type.tank]);

            if (Input.GetKeyDown (KeyCode.R))
                Spawn (characters [(int)type.enemy]);
        }

        if (Input.GetKeyDown (KeyCode.T)) { // FOR TESTING
            BP.BrainPoints++;
            Debug.Log ("BP: " + BP.BrainPoints);
        }
    }

    void Spawn (GameObject mob)
    {
        int cost = mob.GetComponent<Mob>().Value;

        if (BP.BrainPoints >= cost && (currentValue + cost) <= maxValue) {
            BP.BrainPoints -= cost;
            Debug.Log ("BP: " + BP.BrainPoints + ", Cost: " + cost + ", Value:" + currentValue + "/" + maxValue);

            Debug.Log ("CharacterSpawn@Spawn");

            if (mob.GetComponent<Mob>().Alliance == 0)
                Instantiate (mob, zombieSpawn.transform.position, Quaternion.identity);
            else
                Instantiate (mob, humanSpawn.transform.position, Quaternion.identity);

            currentValue += cost;
            // cooldown = 2.0f;
        } else {
            Debug.Log ("BP: " + BP.BrainPoints + ", Cost: " + cost + ", Value:" + currentValue + "/" + maxValue);
        }
    }
}
