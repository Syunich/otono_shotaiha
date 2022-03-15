using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SyunichTool;
using DG.Tweening;

public class AudioManager : SingletonMonovehavior<AudioManager>
{
    [SerializeField] AudioSource BGMSource;
    [SerializeField] AudioSource SESource;
    [SerializeField] AudioClip[] BGMs;
    [SerializeField] AudioClip[] SEs;

    public AudioSource BGMsource
    {
        get { return BGMSource; }
    }

    protected override bool IsDestroyOnLoad => false;

    public void PlaySE(int index)
    {
        try
        {
            SESource.PlayOneShot(SEs[index]);
        }
        catch
        {
            Debug.Log("SE番号" + index + "が存在しません");
        }
    }

    public void PlayBGM(int index)
    {
        try
        {
            BGMSource.clip = BGMs[index];
            BGMSource.Play();
        }
        catch
        {
            Debug.Log("BGM番号" + index + "が存在しません");
        }
    }

    /// <summary>
    /// 以下音量調整用機能
    /// </summary>
    public void ChengeBGMvolume(float value)
    {
        BGMSource.volume = value;
        if (BGMSource.volume < 0) { BGMSource.volume = 0; }
        if (BGMSource.volume > 1) { BGMSource.volume = 1; }
    }

    public void ChengeSEvolume(float value)
    {
        SESource.volume += value;
        if (SESource.volume < 0) { SESource.volume = 0; }
        if (SESource.volume > 1) { SESource.volume = 1; }
    }

    /// <summary>
    /// BGMを消し、なってる時の音量を返す
    /// </summary>
    /// <returns></returns>
    public void FeedOutBGM(float time)
    {
        BGMsource.DOFade(0, time).Play();
    }

    public void FeedInBGM(float volume , float time , int index)
    {
        BGMsource.volume = 0;
        PlayBGM(index);
        BGMsource.DOFade(volume, time).Play();
    }
}

