using UnityEngine;
using System.Collections;
using Zombies;

/// <summary>
/// Manages Zombie characters
/// </summary>
public class Zombie : Actor 
{
    public int damageAmount         = 1;
    public float distanceToDamage   = 1.1f;
    public float timeBetweenAttacks = 0.5f;

    protected NavMeshAgent _agent;
    protected bool         _inRange;
    protected Damage       _targetHealth;
    protected float        _timer;

    void Start()
    {
        _agent      = GetComponent<NavMeshAgent>();
        _inRange    = false;
        _timer      = 0f;
    }

    void Update()
    {
        _timer += Time.time;

        if (_timer >= timeBetweenAttacks && _inRange && _targetHealth.health > 0) {
            Attack();
        }
    }

    void Attack()
    {
        _timer = 0f;

        if (_targetHealth.health >= 0) {
            _targetHealth.TakeDamage(damageAmount);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "NPC") {
            _inRange = true;
            _targetHealth = other.gameObject.GetComponent<Damage>();
            _targetHealth.ParticleSystem.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "NPC") {
            _inRange = false;
            _targetHealth.ParticleSystem.Stop();
        }
    }
}
