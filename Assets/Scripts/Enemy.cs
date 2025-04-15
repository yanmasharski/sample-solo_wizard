using System;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{

    public event Action<Enemy> OnKilled;
    [SerializeField] private NavMeshAgent agent;

    private float navigationRebuildTime = 1f;

    private void Start()
    {
        agent.destination = Hero.instance.position;
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

    public void Kill()
    {
        OnKilled?.Invoke(this);
        Destroy(gameObject);
    }
}
