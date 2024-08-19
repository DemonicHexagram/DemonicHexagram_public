using System.Collections;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("#BGM")]
    public AudioClip[] bgmClips;
    public float bgmVolume = 0.5f;
    public float fadeDuration = 10.0f;
    AudioSource bgmPlayer;
    int currentBgmIndex = -1;
    public float bgmInitialVol;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume = 0.5f;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;
    public float sfxInitialVol;

    public float masterVolume;

    protected override void Awake()
    {
        base.Awake();
        if (SoundManager.Instance != null && SoundManager.Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
        Init();
    }

    void Init()
    {
        masterVolume = 1f;
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume * masterVolume;
        PlayBgm(0);

        GameObject sfxObject = new GameObject("SfxObject");
        sfxObject.transform.parent = transform;
        channels = sfxClips.Length;
        sfxPlayers = new AudioSource[channels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume * masterVolume;
        }
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        bgmPlayer.volume = bgmVolume * masterVolume;

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i].volume = sfxVolume * masterVolume;
        }
    }

    public void SetBgmVolume(float volume)
    {
        bgmVolume = volume;
        bgmPlayer.volume = bgmVolume * masterVolume;
        bgmInitialVol = bgmVolume;
    }

    public void SetSfxVolume(float volume)
    {
        sfxVolume = volume;

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i].volume = sfxVolume * masterVolume;
        }
        sfxInitialVol = sfxVolume;
    }

    public void PlayBgm(int index)
    {
        if (index < 0 || index >= bgmClips.Length)
        {
            return;
        }

        if (currentBgmIndex == index && bgmPlayer.isPlaying)
        {
            return;
        }

        if (bgmPlayer.isPlaying)
        {
            StartCoroutine(FadeOutAndChangeBgm(index));
        }
        else
        {
            currentBgmIndex = index;
            bgmPlayer.clip = bgmClips[index];

            StartCoroutine(FadeIn(bgmPlayer, fadeDuration, bgmVolume * masterVolume));
        }
    }

    IEnumerator FadeOutAndChangeBgm(int newIndex)
    {
        yield return StartCoroutine(FadeOut(bgmPlayer, fadeDuration));
        currentBgmIndex = newIndex;
        bgmPlayer.clip = bgmClips[newIndex];

        bgmPlayer.Play();
        yield return StartCoroutine(FadeIn(bgmPlayer, fadeDuration, bgmVolume * masterVolume));
    }


    IEnumerator FadeIn(AudioSource audioSource, float duration, float targetVolume)
    {
        audioSource.volume = 0;
        audioSource.Play();
        float startVolume = audioSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop();
    }

    public void StopBgm()
    {
        StartCoroutine(FadeOut(bgmPlayer, fadeDuration));
        currentBgmIndex = -1;
    }

    public void PlaySfx(SFX sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
