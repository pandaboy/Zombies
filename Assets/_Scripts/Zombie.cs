using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour 
{
    public int damageAmount = 1;
    public float distanceToDamage = 1.1f;
    public float timeBetweenAttacks = 0.5f;

    private NavMeshAgent _agent;

    // indicates if we are attacking other character
    private bool inRange;
    private GameObject target;
    private Damage targetHealth;
    private float timer;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        inRange = false;
        timer = 0f;
    }

    void Update()
    {
        timer += Time.time;

        if (timer >= timeBetweenAttacks && inRange && targetHealth.health > 0)
        {
            Attack();
        }
    }

    void Attack()
    {
        timer = 0f;

        if (targetHealth.health > 0)
        {
            targetHealth.TakeDamage(damageAmount);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "NPC")
        {
            inRange = true;
            target = other.gameObject;
            targetHealth = target.GetComponent<Damage>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "NPC")
        {
            inRange = false;
        }
    }
}
