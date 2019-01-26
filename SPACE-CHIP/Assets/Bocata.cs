using UnityEngine;
using UnityEngine.UI;

public class Bocata : MonoBehaviour
{
    Text text;
    [SerializeField]
    string boacata;
    // Start is called before the first frame update
    void Start()
    {
        text = transform.Find("Canvas").GetComponentInChildren<Text>();
        texting(boacata);
    }

    public void texting(string _text)
    {
        text.text = _text; ;
    }
}
