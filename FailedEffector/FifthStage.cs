using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FifthStage : MonoBehaviour
{
    [SerializeField] Image HideImage;
    [SerializeField] Image Wghost_left;
    [SerializeField] Image Wghost_right;
    [SerializeField] Image Blue_right;
    [SerializeField] Image hands;
    [SerializeField] Image Redeye;
    [SerializeField] Image[] Bloods;

    private void Start()
    {
        var seq = DOTween.Sequence();
        seq.Append(HideImage.DOFade(0.8f, 1))
            .AppendInterval(0.5f)
            .AppendCallback(() => { PlaystagefiveAudio(); AudioManager.Instance.PlaySE(12); AudioManager.Instance.PlaySE(5); })
            .Append(HideImage.DOFade(0, 0))
            .Append(HideImage.DOFade(0.8f, 1))
            .Join(Bloods[0].DOFade(1, 0.2f))
            .Join(Wghost_left.DOFade(1, 0.2f))
             .AppendCallback(() => { PlaystagefiveAudio(); })
            .Append(HideImage.DOFade(0, 0))
            .Append(HideImage.DOFade(0.8f, 1))
            .Join(Bloods[1].DOFade(1, 0.2f))
            .Join(Wghost_right.DOFade(1, 0.2f))
             .AppendCallback(() => { PlaystagefiveAudio(); AudioManager.Instance.PlaySE(12); })
            .Append(HideImage.DOFade(0, 0))
            .Append(HideImage.DOFade(0.8f, 1))
            .Join(Bloods[2].DOFade(1, 0.2f))
            .Join(Blue_right.DOFade(1, 0.2f))
            .Join(hands.DOFade(1 , 1f))
            .Append(HideImage.DOFade(1 , 2.5f))
            .Join(Redeye.DOFade(1 , 1.5f))
            .AppendInterval(2.0f)
            .OnComplete(() => { GameManager.Instance.CanGoNext = true; Destroy(this.gameObject); })
            .Play();
    }

    private void PlaystagefiveAudio()
    {
        AudioManager.Instance.PlaySE(11);
        AudioManager.Instance.PlaySE(13);
    }
}
