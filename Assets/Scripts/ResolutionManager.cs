using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    // 드롭다운 옵션과 1:1로 매핑되는 고유 해상도 목록
    private readonly List<Resolution> uniqueResolutions = new();

    private void Start()
    {
        BuildResolutionOptions();
    }

    private void BuildResolutionOptions()
    {
        resolutionDropdown.ClearOptions();
        uniqueResolutions.Clear();

        var seen = new HashSet<string>();
        var optionTexts = new List<string>();

        // 정렬(가로→세로→주사율)해서 보기 좋게
        var all = new List<Resolution>(Screen.resolutions);
        all.Sort((a, b) =>
        {
            int c = a.width.CompareTo(b.width);
            if (c != 0) return c;
            c = a.height.CompareTo(b.height);
            if (c != 0) return c;
            return a.refreshRateRatio.value.CompareTo(b.refreshRateRatio.value);
        });

        // (width,height) 기준으로 한 번씩만 추가
        foreach (var r in all)
        {
            string key = $"{r.width}x{r.height}";
            if (seen.Add(key))
            {
                uniqueResolutions.Add(r);
                optionTexts.Add($"{r.width} x {r.height}");
            }
        }

        resolutionDropdown.AddOptions(optionTexts);

        // 현재 해상도와 일치하는 옵션 선택
        int current = uniqueResolutions.FindIndex(r =>
            r.width == Screen.currentResolution.width &&
            r.height == Screen.currentResolution.height);

        if (current < 0) current = Mathf.Clamp(uniqueResolutions.Count - 1, 0, int.MaxValue);

        resolutionDropdown.value = current;
        resolutionDropdown.RefreshShownValue();

        // 토글 초기값
        fullScreenToggle.isOn = Screen.fullScreenMode != FullScreenMode.Windowed;
    }

    public void SetResolution(int dropdownIndex)
    {
        var r = uniqueResolutions[Mathf.Clamp(dropdownIndex, 0, uniqueResolutions.Count - 1)];
        var mode = fullScreenToggle.isOn ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        Screen.SetResolution(r.width, r.height, mode);

        // 확인용 로그(렌더링 크기)
        Debug.Log($"SetResolution => {Screen.width}x{Screen.height}, mode={Screen.fullScreenMode}");
    }

    public void SetFullscreen(bool isFullscreen)
    {
        var r = uniqueResolutions[resolutionDropdown.value];
        var mode = isFullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        Screen.SetResolution(r.width, r.height, mode);
    }
}
