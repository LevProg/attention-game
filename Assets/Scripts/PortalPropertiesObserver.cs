using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalPropertiesObserver : MonoBehaviour
{
    #region fields
    #region events
    public delegate void PlayerDieEventHandler();
    public static event PlayerDieEventHandler PlayerDieEvent;
    #endregion events
    #region prediction text
    [SerializeField] private Text predictionText;

    private string[] sizeTexts = { 
        "★ *** portals look more reliable (+)", "★ Luck is on the side of *** portals today (+)",
        "★ *** size is great for the average person (+)", "★ *** portals are revered by many peoples. (+)",
        "★ The thought flashed through your mind that *** portals are excellent. (+)", "★ *** portals are statistically safer (+)",
        "★ The stars whisper about the transcendence of *** portals (+)", "★ The oracles have always advised *** portals. (+)" };//8

    private string[] colorTexts = { 
        "★ *** is more presentable (+)", "★ Who knew that today is the day of *** (+)",
        "★ *** is ideal for interdimensional travel (+)","★ You were once told that *** is more stable (+)",
        "★ You've always liked the color *** (+)","★ Someone shouted to you that *** is beautiful today (it's not clear how they shouted in a vacuum) (+)",
        "★ You read in a magazine that *** is the color for all time (+)","★ *** (briefly) (+)"};//8

    private string[] mobilityTexts = { 
        "★ *** portals are good for all criteria (+)", "★ According to myths, space prefers *** portals (+)",
        "★ *** portals are amazing. (+)","★ The prediction says: it's worth choosing a *** portal (+)",
        "★ You flipped a coin and it chose a *** portal (+)","★ As a child, your parents told you that a *** portal is the best choice. (+)",
        "★ The stars are favorable to *** portals (+)","★ You were suddenly drawn to *** portals (+)"};//8

    private string[] headTexts = { 
        "★ *** portals look more convincing today (+)", "★ *** portals are magnificent (+)",
        "★ Today is the perfect weather (somewhere) to travel the *** portal (+)", "★ *** portals are excellent today (+)",
        "★ Space prefers *** portals (+)","★ Divination alludes to *** portals (+)",
        "★ *** portals are always a good choice. (+)","★ *** portals are more presentable (+)"};//8

    private string[] weaponTexts = { 
        "★ *** portals look more convincing today (+)", "★ *** portals are magnificent (+)",
        "★ The stars whispered that *** portals are safer today (+)","★ *** portals are statistically safer (+)" ,
        "★  The stars whisper about the transcendence of *** portals (+)","★  Who knew that today is the day of *** portals (+)",
        "★ *** portals are good for all criteria (+)","★ Space prefers *** portals (+)"};//8
    #endregion prediction text
    #region audio
    [SerializeField] private AudioSource successChose;
    [SerializeField] private AudioSource failChose;
    #endregion audio
    #region UI
    [SerializeField] private Text HpText;
    [SerializeField] private Text playerScoreText;
    [SerializeField] private Text timeText;
    [SerializeField] private GameObject losePanel;

    [SerializeField] private GameObject leftPortal;
    [SerializeField] private GameObject rightPortal;
    #endregion UI

    private List<string> alColor = new List<string> { "Blue", "Purple", "Red", "Green" };
    private List<string> allSize = new List<string> { "Small", "Large"};
    private List<string> allHead = new List<string> { "Paradise", "Infernal", "Usual" };
    private List<string> allWeapon = new List<string> { "Unarmed", "Armed" };
    private List<string> allMobility = new List<string> { "Fixed", "Moving" };

    private int leftPortalScore = 0;
    private int rightPortalScore = 0;

    private PortalProperties leftPortalProperties;
    private PortalProperties rightPortalProperties;

    private string _color;
    private string _size;
    private string _mobility;
    private string _head;
    private string _weapon;

    private int playerScore=0;
    private int playerHelth=3;
    private int startTime=60;
    private int time;
    #endregion

    void Start()
    {
        GetPortalsComponent();
        StartCoroutine(WaitAndCreateDayPrediction());
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        time = startTime;
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeText.text = startTime+"/"+time;
            time--;
            if (time <= 0)
            {
                LoseHp();
            }
        }
    }
    IEnumerator WaitAndCreateDayPrediction()
    {
        yield return new WaitWhile(()=>leftPortalProperties.isPortalCreated == false && rightPortalProperties.isPortalCreated == false);
        CreateDayPrediction();
    }
    private void CreateDayPrediction()
    {
        _color = alColor[Random.Range(0, alColor.Count)];
        _size = allSize[Random.Range(0, allSize.Count)];
        _head = allHead[Random.Range(0, allHead.Count)];
        _weapon = allWeapon[Random.Range(0, allWeapon.Count)];
        _mobility = allMobility[Random.Range(0, allMobility.Count)];

        WritePediction();
    }
    private void WritePediction()
    {

        var size = ( _size,  leftPortalProperties.Size, rightPortalProperties.Size,sizeTexts[Random.Range(0, sizeTexts.Length)].Replace("***", _size));
        var color = ( _color,  leftPortalProperties.Color,  rightPortalProperties.Color, colorTexts[Random.Range(0, colorTexts.Length)].Replace("***", _color));
        var mobility = ( _mobility,  leftPortalProperties.Mobility,  rightPortalProperties.Mobility, mobilityTexts[Random.Range(0, mobilityTexts.Length)].Replace("***", _mobility));
        var head = ( _head,  leftPortalProperties.Head,  rightPortalProperties.Head, headTexts[Random.Range(0, headTexts.Length)].Replace("***", _head));
        var weapon = ( _weapon,  leftPortalProperties.Weapon,  rightPortalProperties.Weapon, weaponTexts[Random.Range(0, weaponTexts.Length)].Replace("***", _weapon));

        var propertiesList = new List<(string, string, string, string)> {size,color,mobility,head, weapon};
        //(current prediction,left properties, right properties, prediction text)

        string predict="";
        for(int i=4; i>0; i--)
        {
            int random = Random.Range(0, propertiesList.Count);
            int randomScore = Random.Range(1, 4);
            predict += propertiesList[random].Item4.Replace("+", "+" + randomScore) + "\n";
            if(propertiesList[random].Item1== propertiesList[random].Item2)
            {
                leftPortalScore += randomScore;
            }
            if (propertiesList[random].Item1 == propertiesList[random].Item3)
            {
                rightPortalScore += randomScore;
            }
            propertiesList.Remove(propertiesList[random]);
        }

        predictionText.text = predict;
        Logging();
    }
    public void CheckAnswer(bool isLeft)
    {
        if (isLeft && leftPortalScore >= rightPortalScore)
        {
            successChose.Play();
            GivePlayerScore();
        }
        else if (!isLeft && leftPortalScore <= rightPortalScore)
        {

            successChose.Play();
            GivePlayerScore();
        }
        else 
        {

            failChose.Play();
            LoseHp();
        }


    }
    private void GivePlayerScore()
    {
        time = 60;
        playerScore++;
        playerScoreText.text = "Score: " + playerScore;
        CreateNewLVL();
    }
    private void LoseHp()
    {
        CameraShake.Shake(.7f, .2f);
        CreateNewLVL();
        playerHelth--;
        HpText.text = "HP: " + playerHelth;
        if (playerHelth <= 0)
        {
            ComparePlayerScoreAndRecord();
            losePanel.SetActive(true);
            PlayerDieEvent?.Invoke();
        }
    }
    private void CreateNewLVL()
    {
        startTime -= 5;
        if (startTime <= 15)
        {
            startTime = 15;
            //TODO in future create lvl (in every lvl will add new items)
        }
        time = startTime;
        rightPortalScore = 0;
        leftPortalScore = 0;
        leftPortalProperties.CreatePortal();
        rightPortalProperties.CreatePortal();
        StartCoroutine(WaitAndCreateDayPrediction());
    }

    # region secondary functions
    private void ComparePlayerScoreAndRecord()
    {
        int allScore = PlayerPrefs.GetInt("AllScore", 0);
        int record = PlayerPrefs.GetInt("RecordScore", 0);
        if (playerScore > record)
        {
            record = playerScore;
        }
        allScore += playerScore;
        PlayerPrefs.SetInt("AllScore", allScore);
        PlayerPrefs.SetInt("CurrentScore", playerScore);
        PlayerPrefs.SetInt("RecordScore", record);
    }
    private void GetPortalsComponent()
    {
        leftPortalProperties = leftPortal.GetComponent<PortalProperties>();
        rightPortalProperties = rightPortal.GetComponent<PortalProperties>();
    }
    private void Logging()
    {
        Debug.Log("Left Portal Score: " + leftPortalScore);
        Debug.Log("Right Portal Score: " + rightPortalScore);
    }
    # endregion secondary functions
}
