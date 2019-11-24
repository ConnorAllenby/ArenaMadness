using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HumanBaseClass;
using Abilities.SwordSlash;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour


{
    public GameMode_Wave GameModeRef;
    
    public float m_movementSpeed;
    public float m_jumpHeight;
    public float m_playerRotationSpeed = 4.0f;
    public float m_PlayerCurrentHealth;

    public float RateOfFire;
    private float NextTimeToFire;
    public Image HealthBar;


    public enum weapon {sword,spear,bow};
    weapon m_equipedWeapon;

    public  SwordSlashScript swordSlashreference;
    public GameObject hitBox_SwordSlash;
    public float swordSlashDuration;
    public Animator m_Animator;

    public Vector3 speed;

    public CharacterController myCC;

    float side;
    float forward;


    public List<AnimationClip> playerAnims = new List<AnimationClip>();


    
    public void Awake()
    {
        GameModeRef = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<GameMode_Wave>();
        m_Animator = gameObject.GetComponent<Animator>();
        swordSlashreference = hitBox_SwordSlash.GetComponent<SwordSlashScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        HeightCheck();
    }

    private void FixedUpdate()
    {
        PlayerMovement(this.gameObject);
        LookAtMouse();
    }

    public void TakeDamage(float damage)
    {
        m_PlayerCurrentHealth -= damage;
        Debug.Log("DamageTaken " + damage);
        UpdateHealthUI();

        if(m_PlayerCurrentHealth <= 0)
        {
            Debug.Log("Player Died");
            GameModeRef.RestartGame();
            
            
        }
    }
    public void UpdateHealthUI()
    {
        float healthUIAmount = m_PlayerCurrentHealth / 100f;
        HealthBar.fillAmount = healthUIAmount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Box")
        {
            TakeDamage(10);
        }
    }


    public void LookAtMouse()
    {

        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0.0f;

        if (playerPlane.Raycast(ray, out hitdist))
        {

            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_playerRotationSpeed * Time.deltaTime);
        }
    }

    public void PlayerMovement(GameObject thisGameObject)
    {
        myCC = thisGameObject.GetComponent<CharacterController>();
        // Get input from the axis and multiple my movememnt speed.
        float forward = Input.GetAxis("Vertical") * m_movementSpeed;
        float side = Input.GetAxis("Horizontal") * m_movementSpeed;


        if(side > 0f)
        {
            m_Animator.SetBool("Run", true);
        }
        else if (side < 0f)
        {
            m_Animator.SetBool("Run", true);
        }
        else if(forward > 0f)
        {
            m_Animator.SetBool("Run", true);
        }
        else if (forward < 0f)
        {
            m_Animator.SetBool("Run", true);
        }
        else
        {
            m_Animator.SetBool("Run", false);
        }
        // Create a new vector 3 containing movement in each direction a && jump height.
        speed = new Vector3(-side, m_jumpHeight, -forward);

        //Character controller uses vector3 containing movement axis speed and regulates by Time.Delta time.
        myCC.Move(speed * Time.deltaTime);

        // Character looks in the direction it is moving in.
        //transform.rotation = Quaternion.LookRotation(speed);
    }

    public void PlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (m_equipedWeapon == weapon.sword && Time.time > NextTimeToFire)
            {
                swordSlashreference.SwordSlash();
                StartCoroutine(PlaySwordSlashAnim(1));
                NextTimeToFire = Time.time + RateOfFire;
                Debug.Log(" Next Time To Fire : " + NextTimeToFire);
            }
            else if (m_equipedWeapon == weapon.spear)
            {

            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Dodge here.
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Wizzard")
        {
            TakeDamage(2 * Time.deltaTime);
        }
    }



    public IEnumerator PlaySwordSlashAnim(float duration)
    {
        m_Animator.SetBool("SwordSlash", true);
        yield return new WaitForSeconds(duration);
        m_Animator.SetBool("SwordSlash", false);
    }


    private void HeightCheck()
    {
        Vector3 currentPos = GetPlayerLocation();

        if(currentPos.y > 0.0001)
        {
            this.gameObject.transform.position = new Vector3(currentPos.x, 0.0001f, currentPos.z);

            Debug.Log("Height Change : Height Check Reset");
        }
        else if(currentPos.y > 0)
        {
            this.gameObject.transform.position = new Vector3(currentPos.x, 0.0001f, currentPos.z);
            Debug.Log("Height Change : Fall Check Reset");
        }
        else
        {
            return;
        }
        

    }

    Vector3 GetPlayerLocation()
    {
        Vector3 _PlayerTrans;
        _PlayerTrans = this.gameObject.transform.position;
        return _PlayerTrans;

    }







}
