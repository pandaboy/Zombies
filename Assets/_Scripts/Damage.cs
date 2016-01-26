using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour
{
    private GameController _gc;
    private Zombify _zombifier;
    private ParticleSystem _particleSystem;
    public ParticleSystem ParticleSystem
    {
        get
        {
            return _particleSystem;
        }
    }
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
        // spray some blood
        // _particleSystem.Play();

        health -= amount;

        // Check if we need to turn into a zomie
        if (health <= 0 && !isZombie)
        {
            health = 0;

            // turn them into a zombie
            _zombifier.TurnToZombie();

            isZombie = true;
        }
    }
}
