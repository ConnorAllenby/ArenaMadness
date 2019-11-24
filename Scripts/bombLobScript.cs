using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombLobScript : MonoBehaviour
{
    PlayerController playerscriptref;
    float timer;
    public float bombClock;
    public float damage;
    public bool playerInArea = false;
    // Start is called before the first frame update
    void Start()
    {
        playerscriptref = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        

    }

    // Update is called once per frame
    void Update()
    {
        Detonate();
        timer = timer + 1 * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collison Detected!");
        if(other.gameObject.tag == "Player")
        {
            playerInArea = true;
        }
        else
        {
            playerInArea = false;
        }
    }

    public void startTimer()
    {
        
    }
    public void Detonate()
    {
        if(playerInArea && timer > bombClock)
        {
            playerscriptref.TakeDamage(damage);
            Destroy(gameObject,2);
            // start coroutine containing effects and destoying this game object.
        }
        else if(timer > bombClock)
        {
            Destroy(gameObject);
        }
    }
}
