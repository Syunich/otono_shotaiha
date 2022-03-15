using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SyunichTool;
using UnityEngine.UI;

public class tuto_PanelController : MonoBehaviour
{
    //文字のやつ
    [SerializeField] CanvasGroup AttentionCG;
    [SerializeField] PlayerController controller;
    [SerializeField] Text spaceText;

    private float time;
    private bool NowHiding;
    private bool CanGoGameScene;
    private void Awake()
    {
        controller.CanControll = false;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            HideAttention();

            if(CanGoGameScene)
            {
                CanGoGameScene = false;
                SceneChanger.Instance.ChangeScene("GameScene", 0);
            }
        }
    }

    private void HideAttention()
    {
        if (!CanHideAttention())
            return;

        NowHiding = true;
        AttentionCG.DOFade(0, 1.0f).OnComplete(
            () =>
            {
                NowHiding = false;
                AttentionCG.blocksRaycasts = false;
                controller.CanControll = true;
                CanGoGameScene = true;
                IndicateSpaceText();
            }).Play() ;
    }

    private bool CanHideAttention()
    {
        if (time < 1.0f) return false;
        if (!AttentionCG.blocksRaycasts) return false;
        if (NowHiding) return false; 

        return true;
    }

    private void IndicateSpaceText()
    {
        spaceText.DOFade(1, 1f).Play();
    }

}
