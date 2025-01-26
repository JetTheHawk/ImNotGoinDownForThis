public class CharacterBaseState
{
    protected Character character;

    public CharacterBaseState(Character character)
    {
        this.character = character;
    }

    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }
}
