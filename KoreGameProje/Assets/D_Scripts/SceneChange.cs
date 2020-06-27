using UnityEngine;
using UnityEngine.UI;
using System.Net.Mail;
public class SceneChange : MonoBehaviour {
    [SerializeField] Sprite[] offon_music;
    [SerializeField] Image musictext;
    [SerializeField] GameObject subject;
    [SerializeField] GameObject message_body;
    private GameManager gm;
    private bool isFocus = false;
    private bool isProcessing = false;
    void Awake()
    {
        Unity.Notifications.Android.AndroidNotificationCenter.CancelAllDisplayedNotifications();
        if (musictext !=null)
            musictext.sprite = offon_music[0];
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
        if(PlayerPrefs.HasKey("music") && musictext !=null)
        {
            if(PlayerPrefs.GetInt("music") == 0)
            {
                musictext.sprite= offon_music[1];
                gm.GetComponent<AudioSource>().mute=true;
            }
            else
            {
                musictext.sprite = offon_music[0];
                gm.GetComponent<AudioSource>().mute = false;
            }
        }
    }
    public void music()
    {
        if (PlayerPrefs.HasKey("music"))
        {
            if (PlayerPrefs.GetInt("music") == 0)
            {
                musictext.sprite = offon_music[0];
                PlayerPrefs.SetInt("music", 1);
                gm.GetComponent<AudioSource>().mute = false;
            }
            else
            {
                musictext.sprite = offon_music[1];
                PlayerPrefs.SetInt("music", 0);
                gm.GetComponent<AudioSource>().mute = true;
            }
        }
        else
        {
            musictext.sprite = offon_music[1];
            PlayerPrefs.SetInt("music", 0);
            gm.GetComponent<AudioSource>().mute = true;
        }
    }

    public void bugReport()
    {
        Application.OpenURL("https://mail.google.com/mail/u/0/?view=cm&fs=1&to=asteroid.gamestdio@gmail.com&su=" + Application.productName + " BUG REPORT&body=&tf=1");
    }
    public void OpenOurWebsite()
    {
        Application.OpenURL("https://alperenhkaral.wixsite.com/bossestudio");
    }
    public void rateGooglePlay()
    {
        Application.OpenURL("market://details?id=" + Application.productName);
    }
    public void share()
    {
        if (!isProcessing)
        {
            StartCoroutine(ShareTextInAnroid());

        }
    }
    void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }
    public System.Collections.IEnumerator ShareTextInAnroid()
    {
        string gameURL = "https://play.google.com/store/apps/details?id=" + Application.identifier;
        Debug.Log(gameURL);
        var shareSubject = "Kore ile alakalı harika bir oyun buldum!";
        var shareMessage = "Hey! Kore ile alakalı harika bir oyun buldum! \nEğer sende bu oyunu denemek istiyorsan hemen indir. " +
                           " \nKOREGAME\n\n" +
                           gameURL;

        isProcessing = true;

        if (!Application.isEditor)
        {
            //Create intent for action send
            AndroidJavaClass intentClass =
                new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject =
                new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>
            ("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

            //put text and subject extra
            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), shareSubject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);


            //call createChooser method of activity class
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity =
                unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser =
                intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share your high score");
            currentActivity.Call("startActivity", chooser);
        }

        yield return new WaitUntil(() => isFocus);
        isProcessing = false;
    }
    public void SceneChangeU(string scenename)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scenename);
       
    }
    public void AppplicationQuit()
    {
        Application.Quit();
    }
    public void paraYok()
    {
        Instantiate(Resources.Load("GameObject/ParaYok") as GameObject);
    }
}
