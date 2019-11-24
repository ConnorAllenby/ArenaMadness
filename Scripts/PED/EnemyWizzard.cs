using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HumanBaseClass;
using UnityEngine.AI;


public class EnemyWizzard : MonoBehaviour
{

    Transform player;
    public float m_FollowMovespeed;
    float currentSpeed;
    public float m_CurrentHealth;
    Animator enemyAnimator;

    public GameObject BombGO;

    public float distanceBoundry;
    float m_distanceToPlayer;
    bool BombLobAreaChosen = false;
    public float probabilityOfLob;
    public float standStillDuration;
    float standStillTimer;

    Vector3 playerLastPos;
    // Start is called before the first frame update

    private void Awake()
    {
        enemyAnimator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        Vector3 playerLastPos = player.transform.position;
        Die();
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
        if (currentSpeed > 0)
        {

            enemyAnimator.SetBool("Walk", true);
            standStillTimer = 0;
        }
        else
        {


            standStillTimer = standStillTimer + 1 * Time.deltaTime;
            if (standStillTimer >= standStillDuration)
            {

                BombLob();
            }
            enemyAnimator.SetBool("Walk", false);
        }
    }

    public void TakeDamage(float damage)
    {
        m_CurrentHealth -= damage;
        Debug.Log("DamageTaken " + damage);
    }

    public void Die()
    {
        if(m_CurrentHealth<= 0)
        {
            Destroy(gameObject,0.2f);
        }
    }

    public void MoveTowardsPlayer()
    {
        transform.LookAt(player);
        if (m_distanceToPlayer >= distanceBoundry)
        {
            GetComponent<NavMeshAgent>().destination = player.transform.position;
            GetComponent<NavMeshAgent>().speed = m_FollowMovespeed;
            currentSpeed = GetComponent<NavMeshAgent>().speed;
        }
        else
        {
            GetComponent<NavMeshAgent>().speed = 0f;
            currentSpeed = GetComponent<NavMeshAgent>().speed;


        }
    }

    public void BombLob()
    {
        
        Vector3 BombPlacement = new Vector3(0, playerLastPos.y + 0.1f, 0);
        if (!BombLobAreaChosen)
        {
            if (Random.Range(0f, 100f) > probabilityOfLob)
            {
                Instantiate(BombGO, BombPlacement, player.rotation);
                BombLobAreaChosen = true;
            }
        }
    }


}
