using UnityEngine;
using System.Collections;

public class CharacterSpawn : MonoBehaviour
{
    public enum type { basic, scout, soldier, tank, enemy };

    public GameObject zombieSpawn;          
    public GameObject humanSpawn;
    public Mob[] characters;
    public int maxSpawns;             // Character limit, based on unit's value

    public float cooldown;
    private float timeElapsed;

    private static Component _zombie; // For Alpha turning -- see Mob.Die()

    public static Component GetZombie()
    {
        return _zombie;
    }

    void Start ()
    {
        for (int i = 0; i < characters.Length; i++)
            characters[i].CreatePool();

        timeElapsed = 0;
        _zombie     = characters [0];
    }

    void Update ()
    {
        timeElapsed += Time.deltaTime;
    }

    public void SpawnAfter(int _index, Vector3 _location, bool _free, float _time)
    {
        StartCoroutine(WaitForSpawn(_index, _location, _free, _time));
    }

    IEnumerator WaitForSpawn(int _index, Vector3 _location, bool _free, float _time)
    {
        yield return new WaitForSeconds(_time);
        Spawn(_index, _location, _free);
    }

    public void Spawn ( int index )
    {
        Spawn ( index, zombieSpawn.transform.position, false );
    }

    public void Spawn ( int index, Vector3 location, bool free )
    {
        if ( timeElapsed >= cooldown )
        {
            timeElapsed = 0;

            int cost = characters [index].Value;

            if ( characters[index].Alliance == 0 && Horde.BrainPoints >= cost && Horde.CurrentValue + cost <= maxSpawns )
            {

                if ( ! free ) Horde.BrainPoints -= cost;

                ObjectPool.Spawn (characters [index], location);

                Horde.CurrentValue += cost;
                Horde.ThreatLevel  += cost;
            }
            else if ( characters[index].Alliance != 0 )
            {
                ObjectPool.Spawn (characters [index], humanSpawn.transform.position);
                Humans.CurrentValue += cost;
            }
            else if ( characters[index].Alliance == 0 && Horde.BrainPoints >= cost && Horde.CurrentValue + cost >= maxSpawns)
            {
                Camera.main.GetComponent<HintController>().Add("You have reached the maximum amount of units!");
            } else {
                Camera.main.GetComponent<HintController>().Add("Not enough Brain Points!");
            }
        }
    }
}
