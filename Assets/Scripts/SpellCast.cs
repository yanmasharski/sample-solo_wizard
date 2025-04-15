using System;
using UnityEngine;

[Serializable]
public class SpellCast : IDamageDealer
{
    [SerializeField] private int damage = 10;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private bool emitFromCollision = false;

    public int Damage => damage;

    public void DamageDealed()
    {
        // TODO: Implement cooldown
    }

    public void Cast(Vector3 startPosition, Vector3 direction, LayerMask layerMask)
    {
        var hits = new RaycastHit[10];
#if DEBUG
        Debug.DrawRay(startPosition, direction * 10f, Color.red, 5f);
#endif
        Physics.SyncTransforms();
        // Debug.Break();
        if (Physics.SphereCastNonAlloc(startPosition, 0.2f, direction, hits, 10f, layerMask) == 0)
        {
            if (!emitFromCollision)
            {
                particleSystem.Emit(100);
            }

            return;
        }

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
