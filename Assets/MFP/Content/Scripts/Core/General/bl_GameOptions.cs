using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class bl_GameOptions : MonoBehaviour {

    public bool isRoom = false;
    public bool ApplyResolutionInStart = false;
    [Space(7)]
    public Text QualityText = null;
    private int CurrentQuality = 0;

    public Text ResolutionText = null;
    private int CurrentRS = 0;

    public Text VolumenText = null;
    private float volume = 1.0f;

    //Keys
    public const string QualityKey = "MFPQuality";
    public const string ResolutionKey = "MFPResolution";
    public const string VolumeKey = "MFPVolume";

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        LoadOptions();
    }

    /// <summary>
    /// 
    /// </summary>
    void LoadOptions()
    {
        CurrentQuality = PlayerPrefs.GetInt(QualityKey, 3);
        CurrentRS = PlayerPrefs.GetInt(ResolutionKey, 0);

        QualityText.text = QualitySettings.names[CurrentQuality];
        ResolutionText.text = Screen.resolutions[CurrentRS].width + " X " + Screen.resolutions[CurrentRS].height;
        if (ApplyResolutionInStart)
        {
            Screen.SetResolution(Screen.resolutions[CurrentRS].width, Screen.resolutions[CurrentRS].height, false);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="forward"></param>
    public void ChangeQuality(bool forward)
    {
        if (forward)
        {
            CurrentQuality = (CurrentQuality + 1) % QualitySettings.names.Length;
        }
        else
        {
            if (CurrentQuality != 0)
            {
                CurrentQuality = (CurrentQuality - 1) % QualitySettings.names.Length;
            }
            else
            {
                CurrentQuality = (QualitySettings.names.Length - 1);
            }
        }
        QualityText.text = QualitySettings.names[CurrentQuality];
        QualitySettings.SetQualityLevel(CurrentQuality);
    }

    /// <summary>
    /// Change resolution of screen
    /// NOTE: this work only in build game, this not work in
    /// Unity Editor.
    /// </summary>
    /// <param name="b"></param>
    public void Resolution(bool b)
    {
        CurrentRS = (b) ? (CurrentRS + 1) % Screen.resolutions.Length : (CurrentRS != 0) ? (CurrentRS - 1) % Screen.resolutions.Length : CurrentRS = (Screen.resolutions.Length - 1);
        ResolutionText.text = Screen.resolutions[CurrentRS].width + " X " + Screen.resolutions[CurrentRS].height;

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="v"></param>
    public void Volumen(float v)
    {
        volume = v;
        //Enabled this line for Apply in runTime
        if (isRoom)
        {
            AudioListener.volume = volume;
        }
        if (VolumenText != null)
        {
            VolumenText.text = (volume * 100).ToString("00") + "%";
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void Apply()
    {
        PlayerPrefs.SetInt(ResolutionKey, CurrentRS);
        PlayerPrefs.SetInt(QualityKey, CurrentQuality);
        PlayerPrefs.SetFloat(VolumeKey, volume);

        Screen.SetResolution(Screen.resolutions[CurrentRS].width, Screen.resolutions[CurrentRS].height, false);
    }
}