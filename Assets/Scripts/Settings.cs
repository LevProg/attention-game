using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixerGroup Mixer;
    public Image MusicTick;
    public Image UITick;

    [SerializeField] private Sprite OffButtonSprite;
    [SerializeField] private Sprite OnButtonSprite;

    void Awake()
    {
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForFixedUpdate();
        StartMaster();
        gameObject.SetActive(false);
    }
    public void StartMaster()
    {
        if (PlayerPrefs.GetInt(key: "Music", defaultValue: 0) == 0)
        {
            OffMusic();
        }
        else
        {
            OnMusic();
        }

        if (PlayerPrefs.GetInt(key: "Sound", defaultValue: 0) == 0)
        {
            OffUI();
        }
        else
        {
            OnUI();
        }
    }
    public void ChouseMusic()
    {
        if (PlayerPrefs.GetInt(key: "Music", defaultValue: 0) == 1)
        {
            OffMusic();
        }
        else
        {
            OnMusic();
        }
    }
    public void ChouseUI()
    {
        if (PlayerPrefs.GetInt(key: "Sound", defaultValue: 0) == 1)
        {
            OffUI();
        }
        else
        {
            OnUI();
        }
    }
    private void OnMusic()
    {
        Mixer.audioMixer.SetFloat("Music", 0);
        MusicTick.sprite = OnButtonSprite;
        PlayerPrefs.SetInt("Music", 1);
        PlayerPrefs.Save();
    }
    private void OffMusic()
    {
        Mixer.audioMixer.SetFloat("Music", -80);
        MusicTick.sprite = OffButtonSprite;
        PlayerPrefs.SetInt("Music", 0);
        PlayerPrefs.Save();
    }
    private void OnUI()
    {
        Mixer.audioMixer.SetFloat("Sound", 0);
        UITick.sprite = OnButtonSprite;
        PlayerPrefs.SetInt("Sound", 1);
        PlayerPrefs.Save();
    }
    private void OffUI()
    {
        Mixer.audioMixer.SetFloat("Sound", -80);
        UITick.sprite = OffButtonSprite;
        PlayerPrefs.SetInt("Sound", 0);
        PlayerPrefs.Save();
    }
    [SerializeField]public void OpenSite()
    {
        Application.OpenURL("http://h91160az.beget.tech/");
    }
    [SerializeField] public void OpenGooglePlay()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=8786055212883898968");
    }
}
