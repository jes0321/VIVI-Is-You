using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinAction : MonoSingleton<WinAction>
{
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeDuration = 1f;
    private readonly int _valueHash = Shader.PropertyToID("_Value");
    [SerializeField] private GameObject _winText;

    private void Awake()
    {
        _fadeImage.material = new Material(_fadeImage.material);

        HandleFadeEvent(true);
    }


    public void HandleFadeEvent(bool isFadeIn)
    {
        float fadeValue = isFadeIn ? 1.2f : 0f;
        float startValue = isFadeIn ? 0f : 1.2f;

        _fadeImage.material.SetFloat(_valueHash, startValue);



        var tweenCore = _fadeImage.material.DOFloat(fadeValue, _valueHash, _fadeDuration);

        if (isFadeIn == false)
        {
            tweenCore.OnComplete(() => {
                var tween = _winText.transform.DOScale(new Vector3(0.17f,0.3f,0), 1);
                tween.OnComplete(() => SceneManager.LoadScene(SceneName.LobbyScene));
            });
        }
    }
}
