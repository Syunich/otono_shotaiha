using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class fourthStage : MonoBehaviour
{
    [SerializeField] Image HideImage;
    [SerializeField] Image Dog;
    [SerializeField] Sprite closeMouth;
    [SerializeField] Sprite openMouth;
    Transform Dogtr;

    private void Start()
    {
        Dogtr = Dog.transform;

        AudioManager.Instance.PlaySE(14);
        var seq = DOTween.Sequence();
        seq.Append(Dog.DOFade(1, 0.5f))
            .Append(HideImage.DOFade(1, 0.5f))
            .AppendInterval(1.0f)
            .Append(Dogtr.DOScale(new Vector3(3, 3, 1), 0f))
            .Append(Dogtr.DOLocalMoveY(26, 0f))
            .Append(HideImage.DOFade(0, 0f))
            .AppendCallback(() => { AudioManager.Instance.PlaySE(5); AudioManager.Instance.PlaySE(15); })
            .AppendCallback(() => StartCoroutine(PakuPaku()))
            .Join(Dogtr.DOLocalMoveY(2609, 2.0f).SetEase(Ease.InCubic))
            .Join(Dogtr.DOScale(new Vector3(50, 50, 1), 2.0f).SetEase(Ease.InCubic))
            .Join(Dogtr.DOShakeRotation(2.0f, strength: 30, vibrato: 5, 60, false))
            .Join(Dog.DOFade(0, 2.5f))
            .AppendInterval(3.5f)
            .OnComplete(() => { GameManager.Instance.CanGoNext = true; Destroy(this.gameObject); })
            .Play();

    }

    private IEnumerator PakuPaku()
    {
        while(true)
        {
            Dog.sprite = openMouth; 
            yield return new WaitForSeconds(0.1f);
            Dog.sprite = closeMouth;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
