using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HumanBaseClass{
    public class Ped_HUMAN : MonoBehaviour
    {
        public float HEALTH;

        public float m_movementSpeed;
        public float m_jumpHeight;


        

        private  Vector3 speed;

        private Vector3 worldpos;
        private float mouseX;
        private float mouseY;
        private float cameraDif;

        
        private void Awake()
        {
        }
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
        
        public void TAKE_DAMAGE(float damage)
        {
            HEALTH = HEALTH - damage;

            if (HEALTH <= 0)
            {
                HEALTH = 0;
                Destroy(gameObject);
            }
        }

       /* public void PlayerMovement(GameObject thisGameObject)
        {
            myCC = thisGameObject.GetComponent<CharacterController>();
            // Get input from the axis and multiple my movememnt speed.
            float forward = Input.GetAxis("Vertical") * m_movementSpeed;
            float side = Input.GetAxis("Horizontal") * m_movementSpeed;

            // Create a new vector 3 containing movement in each direction a && jump height.
            speed = new Vector3(-side, m_jumpHeight, -forward);

            //Character controller uses vector3 containing movement axis speed and regulates by Time.Delta time.
            myCC.Move(speed * Time.deltaTime);

            // Character looks in the direction it is moving in.
            //transform.rotation = Quaternion.LookRotation(speed);
        }
        */

       


    }
}
