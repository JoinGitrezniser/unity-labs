using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public TMP_Text text;
    public Image image;

    public void SetText(string _text)
    {
        text.text = _text;
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
