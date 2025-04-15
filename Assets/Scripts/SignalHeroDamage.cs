public class SignalHeroDamage : ISignal
{
    public readonly int health;
    public readonly int maxHealth;
    public readonly int damage;

    public SignalHeroDamage(int health, int maxHealth, int damage)
    {
        this.health = health;
        this.maxHealth = maxHealth;
        this.damage = damage;
    }

}
