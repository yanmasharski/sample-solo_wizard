using System;
using UnityEngine;

[Serializable]
public class SpellCast: IDamageDealer
{
    [SerializeField] private int damage = 10;

    public int Damage => damage;

    public void DamageDealed()
    {
        // TODO: Implement cooldown
    }

    public void Cast(Vector3 startPosition, Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(startPosition, direction, out hit, 100f))
        {
            hit.collider.GetComponent<IDamageable>().ReceiveDamage(this);
        }
    }
}
