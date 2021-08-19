using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalProperties : MonoBehaviour
{
    [HideInInspector] public string Size;
    [HideInInspector] public string Mobility;
    [HideInInspector] public string Color;
    [HideInInspector] public string Head;
    [HideInInspector] public string Weapon;
    [HideInInspector] public bool isPortalCreated=false;


    #region private fields
    #region sprites
    [SerializeField] private List<Sprite> HeadSprites;
    [SerializeField] private List<Sprite> WeaponSprites;

    [SerializeField] private List<Sprite> blueSprites;
    [SerializeField] private List<Sprite> purpleSprites;
    [SerializeField] private List<Sprite> redSprites;
    [SerializeField] private List<Sprite> greenSprites;

    private Image image;

    private Sprite blueSprite;
    private Sprite purpleSprite;
    private Sprite redSprite;
    private Sprite greenSprite;
    #endregion sprites
    #region child objects
    [SerializeField] private Image headImage;
    [SerializeField] private Image weaponImage;
    #endregion child objects
    private Animator anim;
    private List<float> allSize = new List<float> { .8f, 1.1f };
    private List<string> allColor = new List<string> { "Blue", "Purple", "Red", "Green" };
    private List<string> allHead = new List<string> { "Paradise", "Infernal", "Usual" };
    private List<string> allWeapon = new List<string> { "Unarmed", "Armed" };
    private List<string> allMobility = new List<string> { "Fixed", "Moving" };
    private Dictionary<float, string> sizeDict = new Dictionary<float, string> {{.8f,"Small"},{1.1f, "Large" } };
    #endregion

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
        anim = gameObject.GetComponent<Animator>();
        CreatePortal();
    }

    public void CreatePortal()
    {
        isPortalCreated = false;
        ChoseSize();
        ChoseColor();
        CreateHead();
        CreateWeapon();
        ChoseMobility();
        Logging();
        isPortalCreated = true;
    }
    private void ChoseSize()
    {
        float _size = allSize[Random.Range(0, allSize.Count )];
        Size = sizeDict[_size];
        gameObject.transform.localScale = new Vector3(_size, _size, _size);
    }
    private void ChoseMobility()
    {
        int numberOfMobility = Random.Range(0, allSize.Count);
        Mobility = allMobility[numberOfMobility];
        if (numberOfMobility == 0)
        {
            anim.enabled = false;
        }
        else
        {
            anim.enabled = true;
        }

    }
    private void ChoseColor()
    {
        blueSprite= blueSprites[Random.Range(0, blueSprites.Count)];
        purpleSprite = purpleSprites[Random.Range(0, purpleSprites.Count)];
        redSprite = redSprites[Random.Range(0, redSprites.Count)];
        greenSprite = greenSprites[Random.Range(0, greenSprites.Count)];
        int numberOfcolor = Random.Range(0, 4);
        var possibleSprites = new List<Sprite> { blueSprite, purpleSprite, redSprite, greenSprite };
        Sprite _sprite = possibleSprites[numberOfcolor];
        Color = allColor[numberOfcolor];
        image.sprite = _sprite;
    }
    private void CreateHead()
    {
        int numberOfHead = Random.Range(0, 3);
        Head = allHead[numberOfHead];

        headImage.sprite = HeadSprites[numberOfHead];
    }
    private void CreateWeapon()
    {
        int numberOfWeapon = Random.Range(0, 2);
        Weapon = allWeapon[numberOfWeapon];

        weaponImage.sprite = WeaponSprites[numberOfWeapon];
    }
    private void Logging()
    {
        Debug.Log(gameObject.name+"   "+Weapon+"   "+Head + "   " + Mobility + "   " + Size + "   " + Color);
    }
}
