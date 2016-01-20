using UnityEngine;
using Zombies.Collectables;

public class Collect : MonoBehaviour
{
    // location to place collectables
    public Transform handCollectable;   // carried in hand
    public Transform headCollectable;   // carried overhead

    public void placeItem(GameObject go, CollectableType cType)
    {
        // first reset the items position
        Debug.Log(go.name);

        // based on the type of collectable (Collectable.TYPE)
        // put in the correct place
        if (cType == CollectableType.HAND)
        {
            go.transform.parent = handCollectable;
        }
        else
        {
            go.transform.parent = headCollectable;
        }

        // make sure the local position and rotation of the item is reset
        // so that it's location on the character is defined by the
        // orientation of the transform
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
    }
}
