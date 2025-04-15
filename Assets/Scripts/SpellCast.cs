using System;
using UnityEngine;

[Serializable]
public class SpellCast : IDamageDealer
{
    [SerializeField] private int damage = 10;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private bool emitFromCollision = false;
    [SerializeField] private float cooldownDuration = 1f;

    private DateTime cooldownTime = DateTime.MinValue;
    public int Damage => damage;
    public bool IsOnCooldown => cooldownTime > DateTime.Now;

    public void DamageDealed()
    {
    }

    public void Cast(Vector3 startPosition, Vector3 direction, LayerMask layerMask)
    {
        if (IsOnCooldown)
        {
            return;
        }

        cooldownTime = DateTime.Now.AddSeconds(cooldownDuration);
        
        var hits = new RaycastHit[10];
#if DEBUG
        Debug.DrawRay(startPosition, direction * 10f, Color.red, 5f);
#endif
        Physics.SyncTransforms();
        // Debug.Break();
        if (Physics.SphereCastNonAlloc(startPosition, 0.1f, direction, hits, 10f, layerMask) == 0)
        {
            if (!emitFromCollision)
            {
                particleSystem.Emit(100);
            }

            return;
        }

        Array.Sort(hits, (a, b) => {
            if (a.collider == null && b.collider == null) return 0;
            if (a.collider == null) return 1;
            if (b.collider == null) return -1;
            
            return a.distance.CompareTo(b.distance);
        });

        foreach (var hit in hits)
        {
            if (hit.collider == null)
            {
                EmitParticles(startPosition);
                return;
            }

            Debug.DrawLine(startPosition, hit.point, Color.green, 5f);

            var damageable = hit.collider.GetComponentInParent<IDamageable>();
            if (damageable == null)
            {
                EmitParticles(hit.point);
                Debug.LogWarning(hit.collider.name);
                return;
            }

            damageable.ReceiveDamage(this);

            EmitParticles(hit.point);
            return;
        }

        EmitParticles(hits[0].point);

        void EmitParticles(Vector3 position)
        {
            if (emitFromCollision)
            {
                particleSystem.transform.SetParent(null);
                particleSystem.transform.position = position;
                particleSystem.Emit(100);
            }
            else
            {
                particleSystem.Emit(100);
            }
        }
    }

}
