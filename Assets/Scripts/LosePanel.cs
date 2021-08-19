using GooglePlayGames;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LosePanel : MonoBehaviour
{
    private string leaderBoard = "CgkIq_2BkrMKEAIQAA";
    private string adUnitId = "ca-app-pub-9167408154391994/5918030451";
    private InterstitialAd interstitial;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text recordText;
    [SerializeField] private Text allText;
    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
        RequestInterstitial();
        PortalPropertiesObserver.PlayerDieEvent += UpdateText;
        PortalPropertiesObserver.PlayerDieEvent += GameOver;
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(success =>{});
        gameObject.SetActive(false);
    }
    private void RequestInterstitial()
    {
        this.interstitial = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);

    }
    private void GameOver()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

    private void UpdateText()
    {
        if (scoreText!=null)
        {
            scoreText.text = "Yor Score: " + PlayerPrefs.GetInt("CurrentScore", 0);
        }
        if (recordText != null)
        {
            recordText.text = "Yor Record: " + PlayerPrefs.GetInt("RecordScore", 0);
        }
        if (allText != null)
        {
            allText.text = "All Score: " + PlayerPrefs.GetInt("AllScore", 0);
        }
        Social.ReportScore(PlayerPrefs.GetInt("RecordScore", 0), leaderBoard, (bool success) => { });
    }
    #region button functions
    public void Restart()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void OpenLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }
    #endregion button functions
}
