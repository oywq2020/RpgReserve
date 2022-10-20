
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Player
{
    public class AniationSwitchController : MonoBehaviour
    {
        public Animator _animator;
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
    }

}
