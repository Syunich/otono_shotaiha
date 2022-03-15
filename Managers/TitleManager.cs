using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SyunichTool;

public class TitleManager : MonoBehaviour
{
    private bool IsGoGameScene;
    private void Start()
    {
        AudioInit();
        IsGoGameScene = false;
    }

    private void AudioInit()
    {
        AudioManager.Instance.ChengeBGMvolume(TitleUIManager.Instance.BGMVolume);
        AudioManager.Instance.ChengeSEvolume(TitleUIManager.Instance.SEVolume);
        AudioManager.Instance.FeedInBGM((float)StaticValues.BGMVolume , 1f , 0 );
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !IsGoGameScene)
        {
            IsGoGameScene = true;
            SceneChanger.Instance.ChangeScene("VolumeChangeSscene", 0);
        }
    }
}
