using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class AudioOptions : MonoBehaviour
{
    public AudioMixer MusicMixer;
    public AudioMixer SoundsMixer;
    public TextMeshProUGUI MusicVolumeValueText;
    public TextMeshProUGUI SoundsVolumeValueText;
    
    public void SetMusicVolume(float SliderValue)
    {
        var sliderTextValue = SliderValue * 5; //To show 100 95 90 etc. Instead of 20 19 18
        MusicVolumeValueText.text = sliderTextValue.ToString(CultureInfo.CurrentCulture);

        MusicMixer.SetFloat("MusicVolumeParam", AdjustVolumeTodB(SliderValue));
    }

    public void SetSoundsVolume(float SliderValue)
    {
        var sliderTextValue = SliderValue * 5; //To show 100 95 90 etc. Instead of 20 19 18
        SoundsVolumeValueText.text = sliderTextValue.ToString(CultureInfo.CurrentCulture);

        SoundsMixer.SetFloat("SoundsVolumeParam", AdjustVolumeTodB(SliderValue));
    }

    private float AdjustVolumeTodB(float volume)
    {
        if (volume >= 20)
            return 0;

        if (volume <= 0)
            return -80;

        return (20 - volume) * -1.5f;
    }
}