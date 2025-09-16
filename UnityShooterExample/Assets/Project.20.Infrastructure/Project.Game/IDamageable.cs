#nullable enable
namespace Project.Game {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using UnityEngine;
    using UnityEngine.Framework;

    public interface IDamageable {

        void Damage(DamageInfo info);

    }
    public static class IDamageableExtensions {

        public static bool TryDamage(this GameObject gameObject, DamageInfo info, [NotNullWhen( true )] out IDamageable? damageable) {
            var damageable_ = gameObject.transform.root.GetComponent<IDamageable>();
            if (damageable_ != null) {
                damageable_.Damage( info );
                damageable = damageable_;
                return true;
            }
            damageable = null;
            return false;
        }

    }
    // DamageInfo
    public abstract record DamageInfo(PlayerBase? Player, EntityBase? Entity, float Damage);
    public record HitDamageInfo(PlayerBase? Player, EntityBase? Entity, float Damage, Vector3 Point, Vector3 Direction, Vector3 Original) : DamageInfo( Player, Entity, Damage );
    public record ExplosionDamageInfo(PlayerBase? Player, EntityBase? Entity, float Damage, Vector3 Point, Vector3 Direction, Vector3 Original) : DamageInfo( Player, Entity, Damage );
    public record KillZoneDamageInfo(float Damage) : DamageInfo( null, null, Damage );
}
