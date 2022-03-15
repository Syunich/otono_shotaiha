using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Linq;
using SyunichTool;

public class FInishIndicator : MonoBehaviour
{
    [SerializeField] CanvasGroup canvas;
    [SerializeField] Button RankingButton;
    [SerializeField] Button RetryButton;
    [SerializeField] Button TitleButton;
    [SerializeField] Button TwitterButton;
    [SerializeField] Image FeedImage;
    [SerializeField] Text[] StageNameTexts;
    [SerializeField] Text[] ScoreTexts;

    [SerializeField] Text AverageText;
    private bool IsRetryed;
    private bool IsTitled;
    private void Start()
    {
        TextSet();
        canvas.DOFade(1, 2.5f).Play();
        RankingButton.onClick.AddListener(() =>
        {
            if(!SceneManager.GetSceneByName("Ranking").isLoaded)
            {
                naichilab.RankingLoader.Instance.SendScoreAndShowRanking(GameManager.Instance.ScoreList.Average());
            }
        });

        RetryButton.onClick.AddListener(() =>
       {
           if(!IsRetryed && !IsTitled)
           {
               IsRetryed = true;
               Sequence seq = DOTween.Sequence();
               seq.AppendCallback(() => AudioManager.Instance.FeedOutBGM(1.5f))
                  .Append(FeedImage.DOFade(1, 1.5f))
                  .AppendInterval(1.0f)
                  .OnComplete(() => { GameManager.Instance.Retry(); Destroy(this.gameObject); }).Play();

               AudioManager.Instance.PlaySE(1);
           }
       });

        TitleButton.onClick.AddListener(() =>
        {
            if (!IsRetryed && !IsTitled)
            {
                IsTitled = true;
                SceneChanger.Instance.ChangeScene("TitleScene", 0);
            }
        });

        TwitterButton.onClick.AddListener(() => naichilab.UnityRoomTweet.Tweet
        ("otono_shotaiha", $"{GameManager.Instance.ScoreList.Count()- GameManager.Instance.ScoreList.Count(x => x==5)}つの声の正体を暴きました！", "オトノショウタイハ"));
    }

    private void TextSet()
    {
        for(int i = 0; i < GameManager.Instance.ScoreList.Count; i++)
        {
            StageNameTexts[i].text = GameManager.Instance.StageInfoList[i].StageName;
            ScoreTexts[i].text = GameManager.Instance.ScoreList[i].ToString("F2") + "m";
        }
        AverageText.text = GameManager.Instance.ScoreList.Average().ToString("F2") + "m";
    }
}
