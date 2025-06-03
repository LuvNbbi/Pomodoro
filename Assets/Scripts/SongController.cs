using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SongController : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> audioClips;

    public TextMeshProUGUI songNameText;
    public TextMeshProUGUI songTimeText;
    public Image PauseButtonImage;
    public Image RepeatButtonImage;
    public Slider progressBar;
    public Slider volumeSlider;

    private int currentTrackIndex = 0;
    private bool isPaused = false;
    private bool isShuffle = false;
    private List<AudioClip> originalOrder;
    private System.Random rng = new System.Random();
    public bool isDragging = false;
    public enum RepeatMode
    {
        None,
        All,
        One
    }
    public RepeatMode repeatMode = RepeatMode.All;

    private string FormatTime(float time)
{
    int minutes = Mathf.FloorToInt(time / 60f);
    int seconds = Mathf.FloorToInt(time % 60f);
    return $"{minutes}:{seconds:D2}";
}

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioClips != null && audioClips.Count > 0)
        {
            originalOrder = new List<AudioClip>(audioClips);
            PlayCurrentTrack();
        }
    }

    void Update()
    {
        if (!audioSource.isPlaying && !isPaused && audioClips.Count > 0)
        {
            if (repeatMode == RepeatMode.One)
            {
                PlayCurrentTrack(); // 현재 곡 반복
            }
            else if (repeatMode == RepeatMode.All)
            {
                PlayNextTrack(); // 리스트 반복
            }
            else if (repeatMode == RepeatMode.None)
            {
                if (currentTrackIndex < audioClips.Count - 1)
                {
                    PlayNextTrack(); // 다음 곡만 재생
                }
            }
        }

        UpdateUI();
    }
    public void SeekTo(float normalizedValue)
{
    if (audioSource.clip != null)
    {
        audioSource.time = normalizedValue * audioSource.clip.length;
    }
}
    public void SetVolume()
    {
        float value = volumeSlider.value;
        audioSource.volume = Mathf.Clamp01(value); // 안전하게 0~1로 제한
    }

    void PlayCurrentTrack()
    {
        audioSource.clip = audioClips[currentTrackIndex];
        audioSource.Play();

        if (songNameText != null)
            songNameText.text = audioClips[currentTrackIndex].name;
    }

    public void PlayNextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % audioClips.Count;
        PlayCurrentTrack();
    }

    public void PlayPreviousTrack()
    {
        currentTrackIndex = (currentTrackIndex - 1 + audioClips.Count) % audioClips.Count;
        PlayCurrentTrack();
    }

    public void PauseOrResume()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            PauseButtonImage.sprite = Addressables.LoadAssetAsync<Sprite>("PlayIcon").WaitForCompletion();
            isPaused = true;
        }
        else
        {
            audioSource.UnPause();
            PauseButtonImage.sprite = Addressables.LoadAssetAsync<Sprite>("PauseIcon").WaitForCompletion();
            isPaused = false;
        }
    }

    public void ToggleShuffle()
    {
        isShuffle = !isShuffle;

        if (isShuffle)
        {
            Shuffle(audioClips);
        }
        else
        {
            audioClips = new List<AudioClip>(originalOrder);
        }

        currentTrackIndex = 0;
        PlayCurrentTrack();
    }
    public void ToggleRepeatMode()
    {
        repeatMode = (RepeatMode)(((int)repeatMode + 1) % 3);
        RepeatButtonImage.sprite = Addressables.LoadAssetAsync<Sprite>(repeatMode.ToString()+"Icon").WaitForCompletion();
        Debug.Log("Repeat mode: " + repeatMode);
    }

    private void Shuffle(List<AudioClip> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            int k = rng.Next(n--);
            var temp = list[n];
            list[n] = list[k];
            list[k] = temp;
        }
    }

private void UpdateUI()
{
    // 프로그레스 바
    if (progressBar != null && audioSource.clip != null && !isDragging)
    {
        progressBar.value = audioSource.time / audioSource.clip.length;
    }

    // 시간 표시
    if (songTimeText != null && audioSource.clip != null)
    {
        string current = FormatTime(audioSource.time);
        string total = FormatTime(audioSource.clip.length);
        songTimeText.text = $"{current} / {total}";
    }
}
}
