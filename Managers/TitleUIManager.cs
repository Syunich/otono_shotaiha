using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SyunichTool;

public class TitleUIManager : SingletonMonovehavior<TitleUIManager>
{
    protected override bool IsDestroyOnLoad => true;
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SESlider;

    public float BGMVolume { get => BGMSlider.value; }
    public float SEVolume { get => SESlider.value; }

    protected override void Awake()
    {
        base.Awake();

        BGMSlider.value = StaticValues.BGMVolume ?? 0.5f;
        SESlider.value = StaticValues.BGMVolume ?? 0.5f;
        StaticValues.BGMVolume = BGMSlider.value;
        StaticValues.SEVolume = SESlider.value;

        BGMSlider.onValueChanged.AddListener(x => { StaticValues.BGMVolume = x; AudioManager.Instance.ChengeBGMvolume(x); });
        SESlider.onValueChanged.AddListener(x => { StaticValues.SEVolume = x; AudioManager.Instance.ChengeSEvolume(x); });
    }
}

