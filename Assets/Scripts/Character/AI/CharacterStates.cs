public class CharacterStates
{
    public SearchingState Searching;
    public ChasingState Chasing;
    public AttackingState Attacking;
    public DeathState Death;
    public VictoryState Victory;

    public CharacterStates(Character character)
    {
        Searching = new SearchingState(character);
        Chasing = new ChasingState(character);
        Attacking = new AttackingState(character);
        Death = new DeathState(character);
        Victory = new VictoryState(character);
    }
}
