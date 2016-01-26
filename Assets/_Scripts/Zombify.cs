using UnityEngine;

/// <summary>
/// Will turn an Actor into a zombie (must have a zombieModel and a characterModel)
/// </summary>
public class Zombify : MonoBehaviour
{
    public GameObject zombieModel;      // stores the Zombie Model
    public GameObject characterModel;   // stores the Character Model

    protected Chase         _chase;
    protected Citizen       _citizen;
    protected Player        _player;
    protected ClickToMoveTo _clickToMoveTo;

    void Start()
    {
        _chase         = GetComponent<Chase>();
        _citizen       = GetComponent<Citizen>();
        _player        = GetComponent<Player>();
        _clickToMoveTo = GetComponent<ClickToMoveTo>();
    }

    /// <summary>
    /// Turns an Actor into a Zombie.
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Actor's are turned into Zombies by disabling active pieces and swapping
    /// the actor model to a zombie model
    /// </remarks>
    public void TurnToZombie()
    {

        // update the tag
        gameObject.tag = "Zombie";
        
        // enable the chase component if present
        if (_chase) {
            _chase.follow  = "NPC,Player";
            _chase.enabled = true;
        }

        // disable the citizen component if present
        if (_citizen) {
            _citizen.enabled = false;
        }

        // disable the player component if present
        if (_player) {
            _player.enabled        = false;
            _clickToMoveTo.enabled = false;
        }

        // swap the active models
        characterModel.SetActive(false);
        zombieModel.SetActive(true);
    }
}
