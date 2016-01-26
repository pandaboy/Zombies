namespace Zombies
{
    /// <summary>
    /// Different types of actors that can be stored in the Graph
    /// </summary>
    public enum ActorType
    {
        AUTHORITY, // e.g. Police, Army, Priest (?), Fireman, Politician
        DANGEROUS, // e.g. Thief, Hobo, Alien, Naked
        ITEM,      // e.g. Weapon, Animal
        NEUTRAL    // Everybody else
    }
}
