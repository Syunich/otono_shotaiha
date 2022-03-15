using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SyunichTool;
using UnityEngine.UI;
using DG.Tweening;
public class GameManager : SingletonMonovehavior<GameManager>
{
    protected override bool IsDestroyOnLoad => true;
    private List<StageInfoMation> _stageInfoList;
    private float Retry_passedtime;
    public List<float> ScoreList { get; set; }

    [SerializeField] MessageIndicator indicator;
    public int NowStageNum { get; private set; }
    public IReadOnlyList<StageInfoMation> StageInfoList { get => _stageInfoList; }
    public bool CanGoNext { get; set; }

    //initに必要なモジュール
    [SerializeField] AudioSource EnemyAudio;
    [SerializeField] AudioListener Player;

    //ステージに必要なモジュール
    [SerializeField] Sprite[] ClearSprites;
    [SerializeField] Sprite[] FailedSprites;
    [SerializeField] Text AreaText;
    [SerializeField] AudioClip[] EnemyVoices;

    [SerializeField] Canvas GameUICanvas;
    [SerializeField] GameObject FinishCanvas;
    protected override void Awake()
    {
        _stageInfoList = new List<StageInfoMation>();
        ScoreList = new List<float>();    

        GameUICanvas.enabled = false;
        base.Awake();
        StageInit();

        NowStageNum = 0;
    }

    private void Start()
    {
        if (!StaticValues.IsPassMessage)
        {
            indicator.MessageStart();
            AudioManager.Instance.FeedInBGM((float)StaticValues.BGMVolume, 5 , 1);
        }
        else
        {
            GameInit();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (!StaticValues.IsPassMessage)
            {
                IndicateMessage();
            }

            if (Player.gameObject.GetComponent<PlayerController>().CanControll)
            {
                StopGame();
                GameUIManager.Instance.Flash();
            }
        }

        if (CanGoNext)
        {
            CanGoNext = false;
            Next();
        }

        if(Input.GetKey(KeyCode.R) && Player.gameObject.GetComponent<PlayerController>().CanControll)
        {
            Retry_passedtime += Time.deltaTime;
            if (Retry_passedtime > 2.0f)
            {
                Player.gameObject.GetComponent<PlayerController>().CanControll = false;
                GameAllRetry();
            }
        }
        else
        {
            Retry_passedtime = 0;
        }
    }

    public float GetDistance()
    {
        return Vector3.Magnitude(Player.gameObject.transform.position - EnemyAudio.gameObject.transform.position) - 1f;
    }

    public void AddScore()
    {
        if(GetDistance() > 1.5)
        {
            ScoreList.Add(5f);
        }
        else
        {
            ScoreList.Add(GetDistance());
        }
    }

    public void AddMaxScore()
    {
        ScoreList.Add(5f);
    }

    public void GameInit()
    {
        GameUICanvas.enabled = true;
        Player.gameObject.transform.position = Vector3.zero;
        EnemyAudio.gameObject.transform.position = _stageInfoList[NowStageNum].EnemySpownPos;
        EnemyAudio.DOFade((float)StaticValues.SoundVolume, 1).Play();
        EnemyAudio.clip = _stageInfoList[NowStageNum].Voice;
        EnemyAudio.Play();
        Sequence seq = DOTween.Sequence();
        AreaText.text = _stageInfoList[NowStageNum].StageName;
        seq.Append(AreaText.DOFade(1 , 1.0f))
           .AppendInterval(1.0f)
           .Append(AreaText.DOFade(0 , 1.5f))
           .OnComplete(() =>
        Player.gameObject.GetComponent<PlayerController>().CanControll = true).Play();
        GameUICanvas.GetComponent<CanvasGroup>().DOFade(1, 1f).Play();

        AudioManager.Instance.PlaySE(6);
    }

    public void Retry()
    {
        StaticValues.IsPassMessage = true;
        StageInit();
        ScoreList.Clear();
        NowStageNum = 0;
        GameInit();
    }

    private void GameAllRetry()
    {
        GameUICanvas.GetComponent<CanvasGroup>().DOFade(0, 2f)
            .OnComplete(() => Retry()).Play();
    }

    private void StageInit()
    {
        _stageInfoList.Clear();
        _stageInfoList.Add(new StageInfoMation("廃墟入り口", ClearSprites[0], FailedSprites[0] , EnemyVoices[0]));
        _stageInfoList.Add(new StageInfoMation("食堂", ClearSprites[1], FailedSprites[0], EnemyVoices[1]));
        _stageInfoList.Add(new StageInfoMation("中庭", ClearSprites[2], FailedSprites[0], EnemyVoices[2]));
        _stageInfoList.Add(new StageInfoMation("廊下", ClearSprites[3], FailedSprites[0], EnemyVoices[3]));
        _stageInfoList.Add(new StageInfoMation("44番客室", ClearSprites[4], FailedSprites[0], EnemyVoices[4]));
    }

    public void StopGame()
    {
        GameUICanvas.enabled = false;
        EnemyAudio.volume = 0;
        Player.gameObject.GetComponent<PlayerController>().CanControll = false;
        GameUICanvas.GetComponent<CanvasGroup>().alpha = 0;
        GameUIManager.Instance.ResetArrow();
    }

    public void Next()
    {
        NowStageNum++;
        if(NowStageNum == _stageInfoList.Count)
        {
            Instantiate(FinishCanvas);
            AudioManager.Instance.FeedInBGM((float)StaticValues.BGMVolume, 2f , 2);
        }
        else
        {
            GameInit();
        }
    }

    private void IndicateMessage()
    {
        if (!indicator.IsEnd || indicator.CanGoNext) //メッセージが末尾までいってない場合
        {
            if (!indicator.IsEnd && indicator.CanGoNext)
            {
                indicator.MessageReload();
            }
            else if (indicator.IsEnd && indicator.CanGoNext)
                indicator.MessageEnd(() => GameInit());
        }
    }
}
