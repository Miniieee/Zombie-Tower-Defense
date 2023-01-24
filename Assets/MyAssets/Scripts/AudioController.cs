using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioMixer mixer;

    public GameObject music;
    public GameObject sound;

    public void MuteMusic()
    {
        mixer.SetFloat("MusicVolume", 0f);
    }

    public void EnableMusic()
    {
        mixer.SetFloat("MusicVolume", 1f);
    }


    public void MuteSound()
    {
        mixer.SetFloat("OtherVolume", 0f);
    }

    public void EnableSound()
    {
        mixer.SetFloat("OtherVolume", 1f);
    }


    public void MusicClick()
    {
        music.SetActive(!music.activeSelf);

        if (music.activeSelf)
        {
            MuteMusic();
        }
        else
        {
            EnableMusic();
        }
    }
    public void SoundClick()
    {
        sound.SetActive(!sound.activeSelf);

        if (!sound.activeSelf)
        {
            MuteSound();
        }
        else
        {
            EnableSound();
        }
    }
}
