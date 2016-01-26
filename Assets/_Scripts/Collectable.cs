using UnityEngine;
using Zombies;

/// <summary>
/// Defines the type of collectable this is, and calls the placeItem()
/// method on the collecting class
/// </summary>
public class Collectable : MonoBehaviour
{
    public CollectableType collectableType;

    public void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.tag == "Player") {
            go.GetComponent<Collect>().placeItem(gameObject, collectableType);
        }
    }
}
