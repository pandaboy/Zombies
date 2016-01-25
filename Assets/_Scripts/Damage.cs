using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour
{
    private GameController _gc;
    private Zombify _zombifier;
    private ParticleSystem _particleSystem;
    private bool isZombie;

    public int health = 100;

    void Start()
    {
        _zombifier = GetComponent<Zombify>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        isZombie = false;
	}

    public void TakeDamage(int amount)
    {
        health -= amount;

        // Check if we need to turn into a zomie
        if (health <= 0 && !isZombie)
        {
            health = 0;

            // turn them into a zombie
            _zombifier.TurnToZombie();

            isZombie = true;
        }

        // spray some blood
        _particleSystem.Play();
    }
}
