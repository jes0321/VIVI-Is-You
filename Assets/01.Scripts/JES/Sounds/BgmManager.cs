using System;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    [SerializeField] private SoundSO _bgmSO;

    private void Awake()
    {
        SoundPlayer player = PoolManager.Instance.Pop("SoundPlayer") as SoundPlayer;
        
        player.PlaySound(_bgmSO);
    }
}
