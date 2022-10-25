using System;
using _core.Scripts.Abstract;
using UnityEngine;

public class LiveEnity : MonoBehaviour,IDamageable
{
    public float startingHealth = 12;
    public float Health { get; protected set; }
    protected bool Dead;
    public event Action OnDeath;
    protected virtual void Start()
    {
        Health = startingHealth;
    }
    //simple takeHit
    public virtual void TakeHit(float damage)
    {
        if (!Dead)
        {
            // Do some stuff here with hit var
            TakeDamage(damage);
        }
    }
    
    //detailed takeHit
    public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (!Dead)
        {
            // Do some stuff here with hit var
            TakeDamage(damage);
        }
    }
    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0 && !Dead)
        {
            Die();
        }
    }
    public  virtual void Die()
    {
        Dead = true;
        OnDeath?.Invoke();
    }
}