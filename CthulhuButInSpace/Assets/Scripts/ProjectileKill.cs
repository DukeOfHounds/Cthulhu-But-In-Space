using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HealthAndDamage
{
    public class ProjectileKill : MonoBehaviour
    {
        public GameObject projectile;
        public float timeToKill = 2;
        public GameObject killParticle;
        public float damage = 2;

        // Update is called once per frame
        void Update()
        {
            Destroy(projectile, timeToKill);
           // Destroy(killParticle, timeToKill);

        }


        private void OnCollisionEnter(Collision collision)
        {
            if (projectile.gameObject.tag != "Particle")
            {
               if (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "Scrap" || collision.gameObject.tag == "Jewel" || collision.gameObject.tag == "Shield")
              // if (collision.gameObject.tag != "Player" || collision.gameObject.tag != "Player" || collision.gameObject.tag != "Player")
                {
                    Instantiate(killParticle, collision.gameObject.transform.position, collision.gameObject.transform.rotation);// explosion effect
                    collision.gameObject.GetComponent<HealthScript>().takeDamage(damage); // has collided object take damage
                    if (projectile.gameObject.tag == "Missile")
                    {
                        Destroy(projectile);
                    }

                }
            }
        }

    }
}
