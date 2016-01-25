using UnityEngine;

// turns the character into a zombie
public class Zombify : MonoBehaviour
{
    public GameObject zombieModel;
    public GameObject characterModel;

    public void TurnToZombie()
    {
        Chase chase = GetComponent<Chase>();
        Citizen citizen = GetComponent<Citizen>();
        Player player = GetComponent<Player>();
        ClickToMoveTo ctmt = GetComponent<ClickToMoveTo>();

        // update the tag
        gameObject.tag = "Zombie";
        
        // enable the chase component if present
        if(chase) {
            chase.follow = "NPC,Player";
            chase.enabled = true;
        }

        // disable the citizen component if present
        if(citizen) {
            citizen.enabled = false;
        }

        // disable the player component if present
        if(player) {
            player.enabled = false;
            ctmt.enabled = false;
        }

        // load the zombieModel
        characterModel.SetActive(false);
        zombieModel.SetActive(true);
    }
}
