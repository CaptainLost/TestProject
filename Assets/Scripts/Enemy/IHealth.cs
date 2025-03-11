public interface IHealth
{
    float MaxHealth { get; }
    float Health { get;  }

    void ReceiveDamage(float damageValue);
}
