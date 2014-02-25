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

    public Mob[] characters;

    public int maxValue = 50;               // Character limit, based on unit's value
    public static int currentValue;         // How much value is currently on the field

    float cooldown = 2.0f;

    void Start () {
        currentValue = 0;

        for (int i = 0; i < characters.Length; i++)
            characters[i].CreatePool();
    }

    void Update ()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0) {
            // Figure out a cleaner way of doing this
            if (Input.GetKeyDown (KeyCode.Q))
                Spawn ((int)type.generic);
        
            if (Input.GetKeyDown (KeyCode.W))
                Spawn ((int)type.scout);

            if (Input.GetKeyDown (KeyCode.E))
                Spawn ((int)type.tank);

            if (Input.GetKeyDown (KeyCode.R))
                Spawn ((int)type.enemy);
        }

        if (Input.GetKeyDown (KeyCode.T)) { // FOR TESTING
            BP.BrainPoints++;
            Debug.Log ("BP: " + BP.BrainPoints);
        }
    }

    void Spawn (int index)
    {
        int cost = characters[index].GetComponent<Mob>().Value;

        if (BP.BrainPoints >= cost && (currentValue + cost) <= maxValue) {
            BP.BrainPoints -= cost;
            Debug.Log ("BP: " + BP.BrainPoints + ", Cost: " + cost + ", Value:" + currentValue + "/" + maxValue);

            Debug.Log ("CharacterSpawn@Spawn");

            if (characters[index].GetComponent<Mob>().Alliance == 0) {
                ObjectPool.Spawn(characters[index], zombieSpawn.transform.position);
            } else {
                ObjectPool.Spawn(characters[index], humanSpawn.transform.position);
            }

            currentValue += cost;
            // cooldown = 2.0f;
        } else {
            Debug.Log ("BP: " + BP.BrainPoints + ", Cost: " + cost + ", Value:" + currentValue + "/" + maxValue);
        }
    }
}
