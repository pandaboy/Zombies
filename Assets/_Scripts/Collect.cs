using UnityEngine;
using Zombies;
using Zombies.Collectables;

public class Collect : MonoBehaviour
{
    // location to place collectables
    public Transform handCollectable;   // carried in hand
    public Transform headCollectable;   // carried overhead

    private ZombieGraph _graph = ZombieGraph.Instance;
    private Relationship trust = new Relationship(Zombies.RelationshipType.TRUST);

    // referecence to player game object
    // private GameObject player;

    public void Start()
    {
        // player = transform.parent.gameObject;
    }

    public void placeItem(GameObject item, CollectableType type)
    {
        // based on the type of collectable (Collectable.TYPE)
        // put in the correct place
        if (type == CollectableType.HAND) {
            item.transform.parent = handCollectable;
        }
        else {
            item.transform.parent = headCollectable;
        }

        // make sure the local position and rotation of the item is reset
        // so that it's location on the character is defined by the
        // orientation of the transform
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;

        // Add a Direct Connection to the item collected
        _graph.AddDirectConnection(new Connection(gameObject, item, trust));

        // update the UI with new relationships
        gameObject.GetComponent<Player>().DisplayRelationships();
        item.GetComponent<Actor>().PrintRelationships();
    }
}
