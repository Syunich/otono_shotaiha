using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volsceneManager : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] AudioSource EnemyAudio;

    private bool CanHideAttention;
    private void Awake()
    {
        slider.value = StaticValues.SoundVolume ?? 0.5f;
        StaticValues.SoundVolume = slider.value;
        EnemyAudio.volume = slider.value;

        slider.onValueChanged.AddListener(x => 
        { 
            StaticValues.SoundVolume = x;
            EnemyAudio.volume = x;
        });
    }


}
