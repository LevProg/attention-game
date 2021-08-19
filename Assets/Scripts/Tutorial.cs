using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject FirstTutorPanel;
    [SerializeField] private GameObject SecondTutorPanel;
    [SerializeField] private GameObject ThirdTutorPanel;
    [SerializeField] private GameObject FourthTutorPanel;
    [SerializeField] private GameObject FifthTutorPanel;
    [SerializeField] private GameObject PredictionButton;
    [SerializeField] private GameObject SettingsButton;
    [SerializeField] private GameObject TimerPanel;
    [SerializeField] private GameObject PredictionPanel;
    [SerializeField] private Color PredictionPanelColor;
    void Start()
    {
        if (PlayerPrefs.GetInt("FIRST", 0) == 0)
        {
            PlayerPrefs.SetInt("FIRST", 1);
            FirstTutorPanel.SetActive(true);
        }
    }
    public void NextFromFirstToSecondPanel()
    {
        FirstTutorPanel.SetActive(false);
        SecondTutorPanel.SetActive(true);
        PredictionButton.GetComponent<Animator>().enabled = true;
        PredictionButton.GetComponent<Button>().interactable = false;
        SettingsButton.GetComponent<Button>().interactable = false;
    }

    public void NextFromSecondToThirdPanel()
    {
        SecondTutorPanel.SetActive(false);
        ThirdTutorPanel.SetActive(true);
        PredictionButton.GetComponent<Animator>().enabled = false;
        TimerPanel.GetComponent<Animator>().enabled = true;
    }
    public void NextFromThirdToFourthPanel()
    {
        ThirdTutorPanel.SetActive(false);
        FourthTutorPanel.SetActive(true);
        PredictionPanel.SetActive(true);
        PredictionPanel.GetComponent<Image>().color = PredictionPanelColor;
        Time.timeScale = 0;
    }
    public void NextFromFourthToFifthPanel()
    {
        FourthTutorPanel.SetActive(false);
        FifthTutorPanel.SetActive(true);
    }
    public void Finish()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }

}
