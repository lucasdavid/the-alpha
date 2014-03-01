using UnityEngine;
using System.Collections;

public class CharacterSpawn : MonoBehaviour
{
    public enum type {
        basic,
        scout,
        soldier,
        tank,
        enemy
    };

    public GameObject zombieSpawn;          // Where zombies spawn
    public GameObject humanSpawn;           // Where humans spawn

    public Mob[] characters;

    public int maxValue = 50;               // Character limit, based on unit's value

    float cooldown = 2.0f;

    void Start () {

        for (int i = 0; i < characters.Length; i++)
            characters[i].CreatePool();
    }

    void Update ()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0) {
            // Figure out a cleaner way of doing this
            if (Input.GetKeyDown (Keymap.kmSpawn.Basic))
                Spawn ((int)type.basic);
        
            if (Input.GetKeyDown (Keymap.kmSpawn.Scout))
                Spawn ((int)type.scout);

            if (Input.GetKeyDown (Keymap.kmSpawn.Soldier))
                Spawn ((int)type.soldier);

            if (Input.GetKeyDown (Keymap.kmSpawn.Tank))
                Spawn ((int)type.tank);

            if (Input.GetKeyDown (Keymap.kmSpawn.Enemy))
                Spawn ((int)type.enemy);
        }
    }

    void Spawn (int index)
    {
        int cost = characters[index].Value;

        if (Horde.BrainPoints >= cost && (Horde.CurrentValue + cost) <= maxValue) {
            Horde.BrainPoints -= cost;
            Debug.Log ("BP: " + Horde.BrainPoints + ", Cost: " + cost + ", Value:" + Horde.CurrentValue + "/" + maxValue);

            if (characters[index].GetComponent<Mob>().Alliance == 0) {
                ObjectPool.Spawn(characters[index], zombieSpawn.transform.position);
            } else {
                ObjectPool.Spawn(characters[index], humanSpawn.transform.position);
            }

            if (characters[index].Alliance == 0)
                Horde.CurrentValue += cost;

            // cooldown = 2.0f;
        } else {
            Debug.Log ("Cannot spawn character. BP: " + Horde.BrainPoints + ", Cost: " + cost + ", Value:" + Horde.CurrentValue + "/" + maxValue);
        }
    }
}
