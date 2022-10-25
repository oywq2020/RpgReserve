using UnityEngine;
namespace _core.Scripts.Abstract
{
    public interface IDamageable
    {
        //simple take hit
        void TakeHit(float damage);
        //detailed take hit
        void TakeHit (float damage, Vector3 hitPoint, Vector3 hitDirection);
    }
}