using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public class HoleHealth : MonoBehaviour
    {
        public List<GameObject> allHoles;
        public int startingHealth = 100;            // The amount of health the enemy starts the game with.
        public int currentHealth;                   // The current health the enemy has.
        public int scoreValue = 100;                 // The amount added to the player's score when the enemy dies.
        public AudioClip deathClip;                 // The sound to play when the enemy dies.

        
        AudioSource enemyAudio;                     // Reference to the audio source.
        ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
        BoxCollider boxCollider;                    // Reference to the capsule collider.
        bool isDead;                                // Whether the enemy is dead.


        void Awake()
        {
            // Setting up the references.
            enemyAudio = GetComponent<AudioSource>();
            hitParticles = GetComponentInChildren<ParticleSystem>();
            boxCollider = GetComponent<BoxCollider>();

            // Setting the current health when the enemy first spawns.
            currentHealth = startingHealth;
        }


        void Update()
        {
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.parent != null && collision.transform.parent.tag == "Fire")
            {
                TakeDamage(Global.Avatar.Damage, collision.contacts[0].point);
            }
        }

        public void TakeDamage(int amount, Vector3 hitPoint)
        {
            // If the enemy is dead...
            if (isDead)
                // ... no need to take damage so exit the function.
                return;

            // Play the hurt sound effect.
            enemyAudio.Play();

            // Reduce the current health by the amount of damage sustained.
            currentHealth -= amount;

            // Set the position of the particle system to where the hit was sustained.
            hitParticles.transform.position = hitPoint;

            // And play the particles.
            hitParticles.Play();

            // If the current health is less than or equal to zero...
            if (currentHealth <= 0)
            {
                // ... the enemy is dead.
                Death();
            }
        }


        void Death()
        {
            // The enemy is dead.
            isDead = true;

            // Turn the collider into a trigger so shots can pass through it.
            boxCollider.isTrigger = true;

            // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
            enemyAudio.clip = deathClip;
            enemyAudio.Play();

            Global.Game.Score += scoreValue;
            Global.Avatar.EXP += scoreValue;

            allHoles.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}