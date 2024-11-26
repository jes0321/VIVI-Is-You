using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoSceneManager : MonoBehaviour
{
    private void Start()
    {
        DOVirtual.DelayedCall(7f,()=>SceneManager.LoadScene("0"));
    }
}