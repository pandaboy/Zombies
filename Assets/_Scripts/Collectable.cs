using UnityEngine;
using Zombies.Collectables;

public class Collectable : MonoBehaviour
{
    public CollectableType collectableType;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Collect>().placeItem(gameObject, collectableType);
        }
    }
}
