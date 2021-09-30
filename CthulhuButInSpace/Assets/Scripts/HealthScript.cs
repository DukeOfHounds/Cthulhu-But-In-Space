using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HealthAndDamage
{
    public class HealthScript : MonoBehaviour
    {

        public float health = 1;

        private float maxHealth;
        private float shieldCD = 0f;
        private float shieldTimer = 3f;
        private float healthRechargeRate = .01f;

        private MeshRenderer mr;
        private SphereCollider sc;

        public GameObject FracturedMesh;



        private void Start()
        {
            string tag = gameObject.tag;
            maxHealth = health; // keeps track of max health
       

        }
        private void Update()
        {
            string tag = gameObject.tag;
            if (tag == "Shield" && health < maxHealth) // shield dependent regeneration
            {

                if (shieldCD > 0.0f) // if shield damage cooldown is  not over
                {
                    shieldCD = Mathf.Clamp(shieldCD - Time.deltaTime, 0.0f, shieldTimer); // countdown shield delay
                }
                else
                {
                    if ( health <= 0) // if shield is destryed
                    {
                        health = 0;// resets negative health value
                        changePower();

                    }
                    health += healthRechargeRate;// recharge shield
                }
            }

        }

         private void changePower()
        {
            mr = gameObject.GetComponent<MeshRenderer>();
            sc = gameObject.GetComponent<SphereCollider>();
            mr.enabled = !mr.enabled; // turn shield on/off
            sc.enabled = !sc.enabled; // turn shield on/off
        }
        public void takeDamage(float damage)
        {
            shieldCD = shieldTimer; // resets shield recharge delay
            health -= damage;
            if (health <= 0) // if object has no health
            {
                string tag = gameObject.tag;
                switch (tag)
                {
                    case "Asteroid":
                        //Debug.Log("Boom");
                        Instantiate(FracturedMesh, transform.position, transform.rotation); // creates scrap
                        Destroy(gameObject);
                        break;
                    case "Scrap":
                        Destroy(gameObject);
                        break;
                    case "Jewel":
                        gameObject.SetActive(false);
                        break;
                    case "Player":
                        // unfinished
                        break;
                    case "Shield":
                        changePower();
                        break;
                }
            }
        }
    }
}
