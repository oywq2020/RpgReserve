using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using _core.Scripts.Architectrue;
using _core.Scripts.Event;
using _core.Scripts.Monster;
using QFramework;
using UnityEngine;
using VContainer;

public class SensorController : MonoBehaviour,IController
{
    private ITrackPlayer _trackPlayer;
    private CancellationTokenSource _tokenSource;
    private void Start()
    {
        _trackPlayer = GetComponentInParent<ITrackPlayer>();
        _trackPlayer.OnDeath += () =>
        {
            //Unregister this function
            Destroy(this);
            _tokenSource.Cancel();
        };
        this.RegisterEvent<OnPlayerDead>(e =>
        {
            Destroy(this);
        });
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            //create token for cancellation this async function
            _tokenSource = new CancellationTokenSource();
            _trackPlayer.TrackPlayer(other.gameObject, _tokenSource.Token);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _tokenSource.Cancel();
        }
    }

    public IArchitecture GetArchitecture()
    {
      return  RpgFramework.Interface;
    }
}
