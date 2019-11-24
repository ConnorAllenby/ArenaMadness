using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{

    Transform m_spawnLocation;
    public List<GameObject> enemyType = new List<GameObject>();
    float spawnDelay;
    public float m_spawn_min;
    public float m_spawn_max;
    float dist;
    public float blockSpawnRadius;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Spawner(spawnDelay, "Wizzard"));
        m_spawnLocation = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(player.transform.position, transform.position);
        spawnDelay = Random.Range(m_spawn_min, m_spawn_max);
    }

    public void SpawnEnemy(string type)
    {
        if (dist >= blockSpawnRadius)
        {
            if (type == "Wizzard")
            {
                GameObject newEnemy = Instantiate(enemyType[0], m_spawnLocation.position, transform.rotation);

            }
        }
        else
        {

        }

    }

    public IEnumerator Spawner(float delay ,string type)
    {
        
        yield return new WaitForSeconds(delay);
        SpawnEnemy(type);
        yield return new WaitForSeconds(delay);
        StartCoroutine(Spawner(spawnDelay, "Wizzard"));

    }



}
