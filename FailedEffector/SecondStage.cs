using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SecondStage : MonoBehaviour
{
    [SerializeField] Image Hand;
    [SerializeField] Image Face;
    [SerializeField] Image BackGround;
    [SerializeField] Image HideImage;

    void Start()
    {
        Sequence seq = DOTween.Sequence();
        var handpos = Hand.gameObject.transform.localPosition;
        Hand.DOFade(1, 0).Play();
        AudioManager.Instance.PlaySE(8);
     seq.Append(Hand.gameObject.transform.DOLocalMove(handpos + new Vector3(-50, 0, 0), 0.1f))
        .Append(Hand.gameObject.transform.DOLocalMove(handpos + new Vector3(50, 0, 0), 0.1f))
        .Append(Hand.gameObject.transform.DOLocalMove(handpos + new Vector3(-30, 0, 0), 0.1f))
        .Append(Hand.gameObject.transform.DOLocalMove(handpos + new Vector3(30, 0, 0), 0.1f))
        .Append(Hand.gameObject.transform.DOLocalMove(handpos, 0.1f))
        .AppendInterval(1.0f)
        .Append(Face.DOFade(1, 0.1f))
           .AppendCallback(() => { AudioManager.Instance.PlaySE(5); AudioManager.Instance.PlaySE(9); })
           .Append(Face.gameObject.transform.DOScaleX(6 , 2.0f).SetEase(Ease.InExpo))
           .Join(Face.gameObject.transform.DOScaleY(8, 2.0f).SetEase(Ease.InExpo))
           .Join(Face.gameObject.transform.DOLocalMove(new Vector3(-535 , 1437 , 0) , 2.0f).SetEase(Ease.InExpo))
           .Join(Face.gameObject.transform.DOShakeRotation(2.0f, 30, 10, 60, false))
           .Append(HideImage.DOFade(1, 1.5f))
           .AppendInterval(3.0f)
           .OnComplete(() => { GameManager.Instance.CanGoNext = true; Destroy(this.gameObject); }).Play(); 
    }
}
