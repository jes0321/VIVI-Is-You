using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnEnable : MonoBehaviour
{
    private TextMeshProUGUI _text;
    [SerializeField] private GameObject _image;
    private int _btnNum, _currentStage;
    private string _stage => "Stage" + _btnNum.ToString();

    public void Initialized(int btnStage, int currentStage)
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();

        _btnNum = btnStage;
        _currentStage = currentStage;
        _text.text = btnStage.ToString();

        _image.SetActive(btnStage > currentStage);
    }

    public void EnterStage()
    {
        if (_currentStage >= _btnNum)
        {
            Debug.Log("¿‘¿Â");
            SceneManager.LoadScene(_stage);
        }
    }

    private IEnumerator UnlockStage()
    {
        yield return new WaitForSeconds(1f);
        //_particle.Play();
        yield return new WaitForSeconds(0.1f);
        //_particle.Play();
        yield return new WaitForSeconds(0.1f);
        //_particle.Play();
    }
}
