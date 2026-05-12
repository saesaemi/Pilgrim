using UnityEngine;

public abstract class TrapBase : MonoBehaviour
{
    [SerializeField] protected bool isActive = true;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive) return;
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            TriggerTrap(player);
        }
    }

    protected virtual void TriggerTrap(PlayerController player)
    {
        player.Die();
    }

    public virtual void Activate() => isActive = true;
    public virtual void Deactivate() => isActive = false;
}
