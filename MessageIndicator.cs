using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class MessageIndicator : MonoBehaviour
{
    [SerializeField ,TextArea(1, 5)] string[] Messages;
    [SerializeField] Text messagetext;
    private MessageAppier _appier;

    private bool _canGoNect;
    public bool CanGoNext { get => _canGoNect; }
    public bool IsEnd { get => _appier.IsEnd; }

    private void Awake()
    {
        _appier = new MessageAppier(Messages);
        _canGoNect = false;
    }

    public void MessageStart()
    {
        messagetext.text = _appier.Next();
        messagetext.DOFade(1, 1.0f)
                   .OnComplete(() => _canGoNect = true).Play();
    }

    public void MessageReload()
    {
        _canGoNect = false;
        
        Sequence seq = DOTween.Sequence();
        seq.Append(messagetext.DOFade(0, 1.0f))
            .AppendCallback(() =>
            { messagetext.text = _appier.Next();
                if(IsEnd)
                {
                    AudioManager.Instance.BGMsource.volume = 0;
                }
            })
            .Append(messagetext.DOFade(1, 1.0f))
            .OnComplete(() => _canGoNect = true)
            .Play();
    }

    public void MessageEnd(Action action)
    {
        _canGoNect = false;
        Sequence seq = DOTween.Sequence();

        seq.Append(messagetext.DOFade(0, 1.5f))
            .AppendInterval(1.0f)
           .OnComplete(() => action()).Play();
    }
}