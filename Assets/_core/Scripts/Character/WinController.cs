using System;
using _core.Scripts.Architectrue;
using _core.Scripts.Event;
using QFramework;
using UnityEngine;

namespace _core.Scripts.Character
{
    public class WinController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                RpgFramework.Interface.SendEvent<OnWin>();
            }
        }
        
    }
}