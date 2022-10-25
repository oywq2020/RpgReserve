using System;
using UnityEngine;

namespace _core.Scripts.Audio
{
    public class AudioBox : MonoBehaviour
    {
        public static AudioBox Instance;
        public AudioClip[] playerAttackClips;
        public AudioClip[] playerMoveClips;
        public AudioClip[] monsterClips;
        public AudioClip[] effectClips;
        private void Awake()
        {
            Instance = this;
        }
    }
}