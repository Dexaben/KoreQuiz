using UnityEngine;
using UnityEngine.UI;
public class basarimUyari : MonoBehaviour {
    public System.Collections.Generic.List<Sprite> basarimImages = new System.Collections.Generic.List<Sprite>();
    public string currentBasarim;
    public string currentBasarimText;
    private Text basarimText;
    private Image basarimImage;
	void Start () {
        basarimImage = this.gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Image>();
        basarimText = this.gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        if (!basarimImage)
            basarimImage = this.gameObject.GetComponent<Image>();
        if (!basarimText)
            basarimText = this.gameObject.GetComponent<Text>();
        basarimImages.AddRange(Resources.LoadAll<Sprite>("basarimResimleri"));
        basarimText.text = currentBasarimText;
        for(int i = 0;i<basarimImages.Count;i++)
        {
            if(basarimImages[i].name == currentBasarim)
            {
                basarimImage.sprite = basarimImages[i];
                return;
            }         
        }
       
    }
}
