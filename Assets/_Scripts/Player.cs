using UnityEngine;
using System;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    // items the player has collected
    private List<GameObject> items;

    // characters/citizens the player has following them
    private List<Citizen> citizens;

    public void AddItem(GameObject item)
    {
        items.Add(item);
    }

    public void AddCitizen(Citizen citizen)
    {
        citizens.Add(citizen);
    }

}