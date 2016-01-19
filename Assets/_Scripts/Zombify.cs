using UnityEngine;

public class Zombify : MonoBehaviour
{
    public GameObject zombieModel;
    public GameObject characterModel;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Zombie")
        {
            // update tag
            this.tag = "Zombie";

            // update follow tags
            GetComponent<Chase>().follow = "Citizen,Player";

            // load zombieModel
            characterModel.SetActive(false);
            zombieModel.SetActive(true);

            // Add "Chase" script
            GetComponent<Chase>().enabled = true;
            GetComponent<Citizen>().enabled = false;
        }
    }
}
