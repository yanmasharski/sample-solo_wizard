public interface IDamageDealer
{
    int Damage { get; }

    /// <summary>
    /// Called when the damage is dealt to the target.
    /// </summary>
    void DamageDealed();
}