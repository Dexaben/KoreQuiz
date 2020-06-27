
using UnityEngine;
using UnityEngine.UI;

public class rutbeUyari : MonoBehaviour {
    public System.Collections.Generic.List<Sprite> rutbeImages = new System.Collections.Generic.List<Sprite>(3);
    public string currentRutbe;
    private Text RUTBE_TEXT;
    private Image RUTBE_IMAGES;
    void Start () {
        RUTBE_IMAGES = this.gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Image>();
        RUTBE_TEXT = this.gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        if (!RUTBE_IMAGES)
            RUTBE_IMAGES = this.gameObject.GetComponent<Image>();
        if (!RUTBE_TEXT)
            RUTBE_TEXT = this.gameObject.GetComponent<Text>();
        rutbeImages.AddRange(Resources.LoadAll<Sprite>("rutbeResimleri"));
        RUTBE_TEXT.text = "Tebrikler! " +currentRutbe.ToUpper() +" seviyesine ulaştınız.";
        for (int i = 0; i < rutbeImages.Count; i++)
        {
            if (rutbeImages[i].name == currentRutbe)
            {
                RUTBE_IMAGES.sprite = rutbeImages[i];
                return;
            }
        }
    }
}
