using ProjectEgoSword;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public FrameSettings frameSettings;

    private void Awake()
    {
        Debug.Log($"timeScale: {frameSettings.timeScale}");
        Time.timeScale = frameSettings.timeScale;

        Debug.Log($"targetFrameRate: {frameSettings.targetFPS}");
        Application.targetFrameRate = frameSettings.targetFPS;
    }
}
