using UnityEngine;
using Zombies.Collectables;

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
