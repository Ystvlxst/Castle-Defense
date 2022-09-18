public interface IDamageable
{
    void TakeDamage(float damage);
    bool Dead { get; }
}