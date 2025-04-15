using System;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour, IDamageDealer, IDamageable
{
    public event Action<Enemy> OnKilled;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int damage = 10;
    /// <summary>
    /// Due to the spec armor logic is inversed. Lower value means more armor.
    /// </summary>
    [Range(0, 1)][SerializeField] private float armor = 1;
    [SerializeField] private float speed = 10;
    [SerializeField] private NavMeshAgent agent;

    private float navigationRebuildTime = 1f;
    private int health;
    public int Damage => damage;

    private void Start()
    {
        agent.destination = Hero.instance.position;
        agent.speed = speed;
        health = maxHealth;
    }

    public void Kill()
    {
        OnKilled?.Invoke(this);
        Destroy(gameObject);
    }

    public void DamageDealed()
    {
        Kill();
    }

    public void ReceiveDamage(IDamageDealer damageDealer)
    {
        health -= (int)(damageDealer.Damage * armor);
        if (health <= 0)
        {
            Kill();
        }

        damageDealer.DamageDealed();
    }
    private void Update()
    {
        if (navigationRebuildTime <= 0)
        {
            agent.destination = Hero.instance.position;
        }
        else
        {
            navigationRebuildTime -= Time.deltaTime;
        }

    }
}
