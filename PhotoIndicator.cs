using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class PhotoIndicator : MonoBehaviour
{
    [SerializeField] Image PictureImage;
    [SerializeField] Image HideImage;
    [SerializeField] CanvasGroup PhotoCG;
    [SerializeField] Transform Phototransform;
    [SerializeField] Text DisTanceText;
    private bool IsSuccesed;
    public static PhotoIndicator Instantiate(PhotoIndicator photoindicator ,  Sprite Photo , bool IsSuccesed)
    {
       var photoind = Instantiate(photoindicator);
        photoind.PictureImage.sprite = Photo;
        photoind.IsSuccesed = IsSuccesed;
        return photoind;
    }

    private void Start()
    {
        IndicatePhoto();
    }
    
    public void IndicatePhoto()
    {
        if(IsSuccesed)
        {
            DisTanceText.text = $"正体との距離:{GameManager.Instance.GetDistance():F2}m";
            AudioManager.Instance.PlaySE(3);
        }
        else
        {
            DisTanceText.text = $"正体との距離:5m(撮影失敗ペナルティ)";
        }

        DisTanceText.DOFade(1, 2.0f).Play();

        Sequence seq = DOTween.Sequence();
        seq.Append(Phototransform.DOLocalMove(Vector3.zero, 2.0f))
            .Join(PhotoCG.DOFade(1, 2.0f))
            .Join(Phototransform.DORotate(new Vector3(0, 0, 10), 1.0f).SetEase(Ease.InCubic))
            .AppendInterval(2.5f)
            .Append(HideImage.DOFade(1 , 1.5f))
            .AppendInterval(2f)
            .OnComplete(() =>
            {
               GameManager.Instance.CanGoNext = true;
               Destroy(gameObject);
            }).Play();
    }
}
