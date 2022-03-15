using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SyunichTool;
using DG.Tweening;

public class GameUIManager : SingletonMonovehavior<GameUIManager>
{
    protected override bool IsDestroyOnLoad => true;

    [SerializeField , Header("AWSD順")] Image[] ArrowFills;
    [SerializeField] Image FlashImage;
    [SerializeField] Sprite DarkImage;
    [SerializeField] PhotoIndicator PhotoCanvasPrefab;

    private void Start()
    {
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// 
    public void ControllArrows(int x , int z)
    {
        if(x == -1)
        {
            ArrowFills[0].fillAmount = 1;
            ArrowFills[3].fillAmount = 0;
        }
        if(x == 0)
        {
            ArrowFills[0].fillAmount = 0;
            ArrowFills[3].fillAmount = 0;
        }
        if (x == 1)
        {
            ArrowFills[0].fillAmount = 0;
            ArrowFills[3].fillAmount = 1;
        }
        if (z == -1)
        {
            ArrowFills[2].fillAmount = 1;
            ArrowFills[1].fillAmount = 0;
        }
        if (z == 0)
        {
            ArrowFills[2].fillAmount = 0;
            ArrowFills[1].fillAmount = 0;
        }
        if (z == 1)
        {
            ArrowFills[2].fillAmount = 0;
            ArrowFills[1].fillAmount = 1;
        }
    }

    public void ResetArrow()
    {
        for(int i = 0; i < ArrowFills.Length; i++)
        {
            ArrowFills[i].fillAmount = 0;
        }
    }

    public void Flash()
    {
        GameManager.Instance.AddScore();
        var seq = DOTween.Sequence();
        seq.Append(FlashImage.DOFade(1f, 0f))
           .Append(FlashImage.DOFade(0f, 1f).SetEase(Ease.InOutQuart))
           .AppendInterval(1.0f)
           .OnComplete(() =>
           {
               if(GameManager.Instance.GetDistance() > 1.5f)
               {
                   PhotoIndicator.Instantiate(PhotoCanvasPrefab, DarkImage, false);
               }
               else
               {
                   PhotoIndicator.Instantiate(PhotoCanvasPrefab , GameManager.Instance.StageInfoList[GameManager.Instance.NowStageNum].ClearSprite , true);
               }
           }
           ).Play();

        AudioManager.Instance.PlaySE(0);
    }
}
