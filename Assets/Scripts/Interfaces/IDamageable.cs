using UnityEngine;

public interface IDamageable
{
    public bool IsFriendly();
    public void TakeDamage(int damage);
}
