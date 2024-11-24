using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ESCManager : MonoBehaviour
{
    [SerializeField] private GameObject ESCMenu;
    [SerializeField] private Slider Master, SFX, BGM;
    [SerializeField] private AudioMixer _audioMixer;
    private void Awake()
    {
        BGM.value = DataManger.Instance.saveData.bgmVol;
        SFX.value = DataManger.Instance.saveData.sfxVol;
        Master.value = DataManger.Instance.saveData.MasterVol;
    }

    public void SFXSoundChange(float value)
    {
        _audioMixer.SetFloat("SFXParam", Mathf.Log10(value) * 20);
    }
    public void BGMSoundChange(float value)
    {
        _audioMixer.SetFloat("BGMParam", Mathf.Log10(value) * 20);
    }
    public void MasterSoundChange(float value)
    {
        _audioMixer.SetFloat("MasterParam", Mathf.Log10(value) * 20);
    }
    
}
