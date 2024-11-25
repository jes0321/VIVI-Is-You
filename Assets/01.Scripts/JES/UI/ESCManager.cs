using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ESCManager : MonoBehaviour
{
    [SerializeField] private GameObject ESCMenu;
    [SerializeField] private Slider Master, SFX, BGM;
    [SerializeField] private AudioMixer _audioMixer;
    
    private bool _isEscOpen = false;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscOnOff(_isEscOpen);
        }
    }

    private void EscOnOff(bool value)
    {
        _isEscOpen = !value;
        ESCMenu.SetActive(_isEscOpen);
        Time.timeScale = _isEscOpen ? 0f : 1f;
    }
    public void OffBtnClick()
    {
        EscOnOff(_isEscOpen);
    }
    public void ExitBtnClick()
    {
        EscOnOff(_isEscOpen);
        SceneManager.LoadScene(SceneName.LobbyScene);
    }
    
}
