using System;
using System.Collections;
using System.Collections.Generic;
using _core.Scripts.Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;

interface IWeapon
{
    int SpellCount { get; }
    Func<UniTask> Spell { get; set; }
    AudioSource WeaponAudioSource { get; set; }
}

public class Weapon : MonoBehaviour, IWeapon
{
    public GameObject normalSpellPreb;
    public GameObject StrengthenSpellPreb;
    public float cD = 1;
    public int maxSpellCharge = 3;
    
    public AudioSource WeaponAudioSource { get; set; }

    private Transform _shootPoint;
    private bool _canShoot = true;

    public int SpellCount { get; set; } = 0;

    //spell delegation
    public Func<UniTask> Spell { get; set; }

    private void Start()
    {
        _shootPoint = transform.Find("ShootPoint").transform;
        AsyncFunction();
        //Attribute initiate
        WeaponAudioSource = GetComponent<AudioSource>();
    }

    private async void AsyncFunction()
    {
        while (true)
        {
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
            if (Input.GetMouseButton(0))
            {
                if (_canShoot)
                {
                    if (SpellCount == maxSpellCharge)
                    {
                        var tmempRotaion = transform.rotation;
                        //Shoot red ball
                        if (Spell != null)
                        {
                            await Spell.Invoke();
                        }

                        //wait for this Spell completed than shoot the SpellBall
                        var ball = Instantiate(StrengthenSpellPreb, _shootPoint.position, tmempRotaion)
                            .GetComponent<SpellController>();
                        SpellCount = 0;
                    }
                    else
                    {
                        var tmempRotaion = transform.rotation;
                        //Shoot bule ball
                        if (Spell != null)
                        {
                            await Spell.Invoke();
                        }

                        //wait for this Spell completed than shoot the SpellBall
                        var ball = Instantiate(normalSpellPreb, _shootPoint.position, tmempRotaion)
                            .GetComponent<SpellController>();
                        SpellCount++;
                    }
                    _canShoot = false;
                    CoolDown();
                }
            }
        }
    }

    async void ListenAndShoot()
    {
    }

    async void CoolDown()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(cD));
        _canShoot = true;
    }
}