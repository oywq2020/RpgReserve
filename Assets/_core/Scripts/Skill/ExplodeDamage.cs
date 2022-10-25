using System;
using _core.Scripts.Abstract;
using _core.Scripts.Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _core.Scripts.Skill
{
    [RequireComponent(typeof(AudioSource))]
    public class ExplodeDamage : MonoBehaviour
    {
        public float damage = 4;
        //the scope of damage caused by this Explode
        public float damageScope = 16;
        public float ExplodeSpeed = 25;

        private SphereCollider _sphereCollider;

        private void Start()
        {
            _sphereCollider = GetComponent<SphereCollider>();
            Explosion();
            //Play explode audio
            var audioSource = GetComponent<AudioSource>();
            audioSource.clip = AudioBox.Instance.effectClips[1];
            //audioSource.SetScheduledStartTime(AudioSettings.dspTime+1);
            audioSource.PlayOneShot(audioSource.clip);
           
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Damageable"))
            {
                //cause damage to this object
                other.gameObject.GetComponent<IDamageable>().TakeHit(damage);
            }
        }

        private void FixedUpdate()
        {
           
        }

      async  void Explosion()
        {
            while (true)
            {
                await UniTask.Yield();
                if (_sphereCollider?.radius<damageScope)
                {
                    _sphereCollider.radius += Time.deltaTime * ExplodeSpeed;
                }
                else
                {
                    _sphereCollider.enabled = false;
                    DisApparent();
                    break;
                }
              
            }
        }

     async void DisApparent()
     {
         await UniTask.Delay(TimeSpan.FromSeconds(3));
         Destroy(gameObject);
     }
    }
}