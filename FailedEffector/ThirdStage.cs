using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ThirdStage : MonoBehaviour
{
    [SerializeField] Image Hand;
    [SerializeField] Image Face;
    [SerializeField] Image BackGround;
    [SerializeField] Image HideImage;

    void Start()
    {
        Sequence seq = DOTween.Sequence();
        var handpos = Hand.gameObject.transform.localPosition;
        AudioManager.Instance.PlaySE(4);
        seq.Append(Face.DOFade(0.3f, 0.5f))
           .Append(Face.DOFade(0f, 0.5f))
           .AppendInterval(1.0f)
           .Append(Face.DOFade(1, 0))
           .Append(Hand.DOFade(1, 0f))
           .Append(BackGround.DOColor(Color.red, 0f))
           .AppendCallback(() =>
           {
               AudioManager.Instance.PlaySE(5);
               AudioManager.Instance.PlaySE(7);
           })
           .Append(Hand.gameObject.transform.DOLocalMove(handpos + new Vector3(-50, 0, 0), 0.1f))
           .Append(Hand.gameObject.transform.DOLocalMove(handpos + new Vector3(50, 0, 0), 0.1f))
           .Append(Hand.gameObject.transform.DOLocalMove(handpos + new Vector3(-30, 0, 0), 0.1f))
           .Append(Hand.gameObject.transform.DOLocalMove(handpos + new Vector3(30, 0, 0), 0.1f))
           .Append(Hand.gameObject.transform.DOLocalMove(handpos, 0.1f))
           .Append(HideImage.DOFade(1, 2.5f))
           .Join(Face.gameObject.transform.DOShakeRotation(2.5f, 30, 3 , 60, false))
           .AppendInterval(2.5f)
           .OnComplete(() => { GameManager.Instance.CanGoNext = true; Destroy(this.gameObject); }).Play(); 
    }
}
