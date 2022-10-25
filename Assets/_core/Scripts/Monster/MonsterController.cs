using System;
using System.Threading;
using System.Threading.Tasks;
using _core.Scripts.Abstract;
using _core.Scripts.Architectrue;
using _core.Scripts.Event;
using Cysharp.Threading.Tasks;
using QFramework;
using UnityEngine;
using UnityEngine.AI;

namespace _core.Scripts.Monster
{
    interface ITrackPlayer
    {
        UniTask TrackPlayer(GameObject gameObject, CancellationToken token);
        event Action OnDeath;
    }

     
    public class MonsterController : LiveEnity, ITrackPlayer,IController
    {
        public float damage = 4;
        
        private Animator _animator;
        private NavMeshAgent _pathfinder;
        private bool canAttack = false;
        private bool playerDead = false;
         protected override void Start()
        {
            base.Start();
            _animator = GetComponent<Animator>();
            _pathfinder = GetComponent<NavMeshAgent>();

            this.RegisterEvent<OnPlayerDead>(e =>
            {
                if (_pathfinder)
                {
                    _pathfinder.isStopped = true;
                }
                playerDead = true;
                _animator?.Play("Victory", 0);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
         public async override void Die()
        {
            _animator.Play("Die", 0);
            base.Die();
            //wait for the animation of role
            await UniTask.Delay(TimeSpan.FromSeconds(4));
            //then destroy it
            Destroy(gameObject);
        }

        //Attack
        private void OnCollisionEnter(Collision other)
        {
          
            if (canAttack && other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<IDamageable>().TakeHit(damage);
                
            }
        }
        public override void TakeDamage(float damage)
        {
            _animator.Play("GetHit", 0, 0.3f);
            base.TakeDamage(damage);
        }

        //track player
        async public UniTask TrackPlayer(GameObject gameObject, CancellationToken token)
        {
            _pathfinder.isStopped = false;
            while (true)
            {
                await UniTask.Yield();
                if (!_pathfinder.isStopped)
                {
                    _pathfinder?.SetDestination(gameObject.transform.position);
                }
                if (DestinationXZ(transform.position, gameObject.transform.position) < 2)
                {
                    //Start attack
                    canAttack = true;
                    _animator.Play("Attack", 0, 0.01f);
                    await UniTask.Delay(TimeSpan.FromSeconds(0.8));
                    canAttack = false;
                }
                else
                {
                    if (_animator.GetCurrentAnimatorStateInfo(0)
                        .fullPathHash == Animator.StringToHash("Base Layer.GetHit"))
                    {
                        await Task.Delay(TimeSpan.FromSeconds(0.5));
                        _animator.Play("Run", 0);
                    }
                    else
                    {
                        _animator.Play("Run", 0);
                    }
                }

                if (token.IsCancellationRequested)
                {
                    //cancel this task
                    if (Dead)
                    {
                        _animator.Play("Die", 0, 0.1f);
                    }
                    else
                    {
                        _animator.Play("Idle", 0, 0.1f);
                    }

                    _pathfinder.isStopped = true;
                    break;
                }

                if (playerDead)
                {
                    break;
                }
            }
        }

        float DestinationXZ(Vector3 origin, Vector3 target)
        {
            return Mathf.Sqrt((origin.x - target.x) * (origin.x - target.x) +
                              (origin.z - target.z) * (origin.z - target.z));
        }

        public IArchitecture GetArchitecture()
        {
            return RpgFramework.Interface;
        }
    }
}