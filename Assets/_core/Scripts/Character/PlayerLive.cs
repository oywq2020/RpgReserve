using _core.Scripts.Architectrue;
using _core.Scripts.Event;
using Player;
using QFramework;
using UnityEngine;

namespace _core.Scripts.Character
{
    public class PlayerLive : LiveEnity
    {
        private Animator _animator;
        private PlayerController _playerController;
        private Weapon _weapon;
        private GameObject _crosshairs;
        protected override void Start()
        {
            base.Start();
            _animator = GetComponentInChildren<Animator>();
            _playerController = GetComponent<PlayerController>();
            _weapon = GetComponent<Weapon>();
            _crosshairs = transform.Find("Crosshairs").gameObject;
        }

        public override void Die()
        {
            GetComponent<CharacterController>().enabled = false;
            Destroy(_playerController);
            Destroy(_weapon);
            Destroy(_crosshairs);
            Cursor.visible = true;
            _animator.Play("Die");
            //trigger event
            RpgFramework.Interface.SendEvent<OnPlayerDead>();
        }

       
    }
}