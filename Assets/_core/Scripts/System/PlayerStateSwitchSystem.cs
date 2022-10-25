using System;
using System.Threading;
using _core.Scripts.Audio;
using Cysharp.Threading.Tasks;
using Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerStateSwitchSystem : IStartable
{
    [Inject] private readonly IPlayer _player;
    [Inject] private readonly AniationSwitchController _animator;
    [Inject] private readonly IWeapon _weapon;

    public void Start()
    {
        //link event
        _player.OnStateChanged += MovementAnimaton;
        _weapon.Spell += StartSpell;
    }

    #region MovementAnimation

    void MovementAnimaton(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Idle:
                _animator._animator.Play("Idle", 0, 1.5f);
                //_animator._animator.SetBool("Idle",true);
                break;
            case PlayerState.Forward:
                _animator._animator.Play("Forward", 0, 1.5f);
                //_animator._animator.SetBool("Forward",true);
                break;
            case PlayerState.Back:
                _animator._animator.Play("Back", 0, 1.5f);
                break;
            case PlayerState.Run:
                _animator._animator.Play("Run", 0, 1.5f);
                break;
            case PlayerState.Jump:
                _animator._animator.Play("Jump", 0, 1.5f);
                break;
            case PlayerState.Right:
                _animator._animator.Play("Right", 0, 1.5f);
                break;
            case PlayerState.Left:
                _animator._animator.Play("Left", 0, 1.5f);
                break;
            case PlayerState.Win:
                _animator._animator.Play("Win", 0,0.1f);
                break;
        }
    }

    #endregion
    #region Weapon
    private async UniTask StartSpell()
    {
        _animator._animator.SetLayerWeight(1, 1);
        _animator._animator.Play("Attack", 1, 0.1f);
        PlayPlayerSound(AudioBox.Instance.playerAttackClips[_weapon.SpellCount]);
        await UniTask.Delay(TimeSpan.FromSeconds(0.3));
       _animator._animator.SetLayerWeight(1, 0.2f); 
       PlayWeaponSound(AudioBox.Instance.effectClips[0]);
    }
    #endregion

    void PlayWeaponSound(AudioClip clip)
    {
        _weapon.WeaponAudioSource.clip = clip;
        _weapon.WeaponAudioSource.PlayScheduled(3);
    }
    void PlayPlayerSound(AudioClip clip)
    {
        _player.PlayerAuiSource.clip = clip;
        _player.PlayerAuiSource.PlayOneShot(clip);
    }
}