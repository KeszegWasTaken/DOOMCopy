public interface ITakeDamage
{
    int Health {get; set;}
    void WeakPointDestroyed();
    void takeDamage(int damage, string source);
}
