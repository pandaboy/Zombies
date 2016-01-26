namespace Zombies
{
    /// <summary>
    /// Different types of actors that can be stored in the Graph
    /// </summary>
    public enum ActorType
    {
        AUTHORITY, // e.g. Police, Army, Priest (?), Fireman, Politician
        DANGEROUS, // e.g. Thief, Hobo, Alien, Naked
        WEAPON,    // e.g. Stick
        ANIMAL,    // e.g. Cat, Dog
        FRIENDLY,  // e.g. Friendly actor
        NEUTRAL    // Everybody else
    }
}
