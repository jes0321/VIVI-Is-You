using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnEnable : MonoBehaviour
{
    private TextMeshProUGUI _text;
    [SerializeField] private GameObject _image;
    private int _btnNum, _currentStage;
    private bool IsTimeEnd = false;
    private string _effectName = "Unlock";
    private string _stage => "Stage" + _btnNum.ToString();

    private WaitForSeconds _sleep;

    public void Initialized(int btnStage, int currentStage, bool isFirst)
    {
        _sleep = new WaitForSeconds(0.1f);
        _text = GetComponentInChildren<TextMeshProUGUI>();

        _btnNum = btnStage;
        _currentStage = currentStage;
        _text.text = btnStage.ToString();

        _image.SetActive(btnStage > currentStage);

        if (btnStage == currentStage && isFirst)
        {
            StartCoroutine(UnlockStage());
        }
        StartCoroutine(Timer());
    }

    public void EnterStage()
    {
        if (_currentStage >= _btnNum && IsTimeEnd)
        {
            Debug.Log("¿‘¿Â");
            SceneManager.LoadScene(_stage);
        }
    }

    private IEnumerator UnlockStage()
    {
        EffectPlayer effectPlayer = null;
        Vector3 myWorldPos = Camera.main.ScreenToWorldPoint(transform.position);
        yield return new WaitForSeconds(1f);
        effectPlayer = PoolManager.Instance.Pop(_effectName + 1) as EffectPlayer;

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
