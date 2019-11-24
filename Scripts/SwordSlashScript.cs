using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities.SwordSlash
{
    public class SwordSlashScript : MonoBehaviour
    {

        public float Damage;
        public bool enemyInsideCollider = false;
        public GameObject m_currentTarget;
        

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerStay(Collider other)
        {
            Debug.Log("Collision");
            if (other.gameObject.tag == "Wizzard")
            {
                m_currentTarget = other.gameObject;
                Debug.Log("Wizzard");
                enemyInsideCollider = true;

            }
            else
            {
                enemyInsideCollider = false;

            }
        }
        

        public void SwordSlash()
        {
            if (enemyInsideCollider)
            {
                if (m_currentTarget != null)
                {
                    new WaitForSeconds(1f);
                    m_currentTarget.GetComponent<EnemyWizzard>().TakeDamage(Damage);
                }
                else
                {
                    m_currentTarget = null;
                }
            }

        }
    }
}
