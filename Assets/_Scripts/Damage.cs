using UnityEngine;

/// <summary>
/// Manages damage done to the gameObject by another.
/// </summary>
public class Damage : MonoBehaviour
{
    public int health = 100;

    protected GameController _gc;
    protected Zombify        _zombifier;

    protected bool _isZombie;
    public bool IsZombie
    {
        get
        {
            return _isZombie;
        }
    }

    protected ParticleSystem _particleSystem;
    public ParticleSystem ParticleSystem
    {
        get
        {
            return _particleSystem;
        }
    }

    void Start()
    {
        _zombifier      = GetComponent<Zombify>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _isZombie       = false;
	}

    public void TakeDamage(int amount)
    {
        health -= amount;

        // Check if we need to turn into a zomie
        if (health <= 0 && !_isZombie) {
            health = 0;

            // turn them into a zombie
            _zombifier.TurnToZombie();

            _isZombie = true;
        }
    }
}
