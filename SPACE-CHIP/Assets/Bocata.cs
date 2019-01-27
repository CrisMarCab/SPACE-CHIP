using UnityEngine;
using UnityEngine.UI;

public class Bocata : MonoBehaviour
{
    Text text;
    SpriteRenderer sprite;

    [SerializeField]
    string boacata;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.Find("Canvas").GetComponentInChildren<Text>();
        sprite = transform.GetComponent<SpriteRenderer>();
        text.enabled = false;
        sprite.enabled = false;

    }

    public void texting(string _text)
    {
        text.text = _text; ;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            text.enabled = true;
            sprite.enabled = true;
        }
    }
}
