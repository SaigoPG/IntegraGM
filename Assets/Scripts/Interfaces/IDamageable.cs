public interface IDamageable
{
    public bool canTakeDamage { get; set; }
    public void TakeDamage(int damage);
}
