using UnityEngine;
using Zombies;

public class Citizen : MonoBehaviour
{
    private ZombieGraph _graph = ZombieGraph.Instance;
    private Follow _follow;
    private Actor _thisActor;

    void Start()
    {
        this._follow = GetComponent<Follow>();
        this._thisActor = GetComponent<Actor>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CheckRelationship(other.gameObject);
        }
    }

    private void CheckRelationship(GameObject other)
    {
        Actor otherActor = other.GetComponent<Actor>() as Actor;

        // Get the direct connection from this actor to the other
        RelationshipType type = this._graph
            .GetConnection(this._thisActor, otherActor)
            .Relationship
            .RelationshipType;

        if (type == RelationshipType.TRUST) {
            this._follow.Target = other;
            this._follow.CanFollow = true;
        }

        // Next check the other connections
    }
}
