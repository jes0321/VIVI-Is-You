using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnEnable : MonoBehaviour
{
    private TextMeshProUGUI _text;
    [SerializeField] private GameObject _image;
    private int _btnNum, _currentStage;
    private bool IsTimeEnd = true;
    private string _effectName = "Unlock";
    private string _stage => _btnNum.ToString();

    private WaitForSeconds _sleep;

    public void Initialized(int btnStage, int currentStage, bool isFirst)
    {
        _sleep = new WaitForSeconds(0.1f);
        _text = GetComponentInChildren<TextMeshProUGUI>();

        _btnNum = btnStage;
        _currentStage = currentStage;
        _text.text = btnStage.ToString();

        _image.SetActive(btnStage > currentStage);//자물쇠 이미지를 끄고 키기

        if (btnStage == currentStage && isFirst)//최초 클리어고, 방금 열린 스테이지 버튼인지
        {
            StartCoroutine(UnlockStage());//그럼 이제 해금 이펙트 이벤트 실행
            StartCoroutine(Timer());
            IsTimeEnd = false;
        }
    }

    public void EnterStage()
    {
        if (_currentStage >= _btnNum && IsTimeEnd)//현재 스테이지보다 작거나 같으면 입장
        {
            SceneManager.LoadScene(_stage);
        }
    }

    private IEnumerator UnlockStage()
    {
        EffectPlayer effectPlayer = null;
        Vector3 myWorldPos = Camera.main.ScreenToWorldPoint(transform.position);
        yield return new WaitForSeconds(1f);
        try
        {
            effectPlayer = PoolManager.Instance.Pop(_effectName + 1) as EffectPlayer;
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
            yield break;
        }

        effectPlayer.SetPositionAndPlay(myWorldPos);
        yield return _sleep;
        effectPlayer = PoolManager.Instance.Pop(_effectName + 2) as EffectPlayer;
        effectPlayer.SetPositionAndPlay(myWorldPos);
        yield return _sleep;
        effectPlayer = PoolManager.Instance.Pop(_effectName + 3) as EffectPlayer;
        effectPlayer.SetPositionAndPlay(myWorldPos);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1.5f);
        IsTimeEnd = true;
    }
}
