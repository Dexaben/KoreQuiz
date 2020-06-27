using UnityEngine;
using System.Text.RegularExpressions;
public class firstenter : MonoBehaviour
{
    Animator mAnimator;
    [SerializeField] Sprite[] music_Images = new Sprite[2];
    [SerializeField] UnityEngine.UI.Image music_buttonImage;
    GameManager gm;
    void Awake()
    {

        if (!PlayerPrefs.HasKey("FirstEnter"))
        {
            this.gameObject.SetActive(true);
        }
        else Destroy(gameObject);
        mAnimator = GetComponent<Animator>();
          gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
          gm.GetComponent<AudioSource>().mute = false;
        music_buttonImage.sprite = music_Images[0];
        
    }
    public void animbool(string boolname)
    {
        mAnimator.SetBool("name", false);
        mAnimator.SetBool("profile", false);
        mAnimator.SetBool("privacy", false);
        mAnimator.SetBool(boolname, true);
    }
    public void Destroy()
    {
        PlayerPrefs.SetString("PrivacyPolicyAccess", "isAccepted");
        PlayerPrefs.SetInt("FirstEnter", 1);
        Destroy(gameObject);
    }
    public void ApplicationqUit()
    {
        Application.Quit();
    }
    public void MusicOnOff()
    {
        if(PlayerPrefs.HasKey("music"))
        {
            if(PlayerPrefs.GetInt("music") == 1)
            {
                    music_buttonImage.sprite = music_Images[1];
                    PlayerPrefs.SetInt("music",0);
                      gm.GetComponent<AudioSource>().mute = true;
            }
            else
            {
                music_buttonImage.sprite = music_Images[0];
                PlayerPrefs.SetInt("music",1);
                  gm.GetComponent<AudioSource>().mute = false;
            } 
    
        }

    }
    public void setProfileImage(int Index)
    {
        PlayerPrefs.SetInt("ProfilePicture", Index);
    }
    public void InputName(UnityEngine.UI.InputField name)
    {
        if (StringValidator(name.text)  && name.text.Length > 5 && name.text.Length < 13)
        {
            PlayerPrefs.SetString("Username", name.text);
            animbool("profile");
        }
        else
        {
            GameObject dogrucevap = Resources.Load("GameObject/Uyari") as GameObject;
            dogrucevap.transform.GetChild(0).GetChild(0).transform.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "Kullanıcı adı geçersiz!\nKullanıcı adı kuralları : Uzunluk 6-12 arası, harfler içermelidir.";
            Destroy(Instantiate(dogrucevap), 2.3f);
        }
    }
    private bool StringValidator(string input)
    {
        string pattern = "[a-z]";
        if (Regex.IsMatch(input, pattern))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
