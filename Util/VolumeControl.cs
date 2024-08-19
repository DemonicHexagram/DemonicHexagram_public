using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    void Start()
    {
        // 슬라이더 초기값 설정
        masterVolumeSlider.value = SoundManager.Instance.masterVolume;
        bgmVolumeSlider.value = SoundManager.Instance.bgmVolume;
        sfxVolumeSlider.value = SoundManager.Instance.sfxVolume;

        // 슬라이더 이벤트 연결
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmVolumeSlider.onValueChanged.AddListener(SetBgmVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);
    }

    void SetMasterVolume(float volume)
    {
        SoundManager.Instance.SetMasterVolume(volume);
    }

    void SetBgmVolume(float volume)
    {
        SoundManager.Instance.SetBgmVolume(volume);
    }

    void SetSfxVolume(float volume)
    {
        SoundManager.Instance.SetSfxVolume(volume);
    }
}
