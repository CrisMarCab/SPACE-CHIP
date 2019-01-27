using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{
    [SerializeField] List<Globo1script> globos = new List<Globo1script>();
    SpaceChipControls spacechipControls;
    int index = 0;


    [SerializeField] GameObject globosFirs, globosSecond, nextLevel, enemiesToAppear;

    bool firstTime;


    private void Start()
    {
        spacechipControls = FindObjectOfType<SpaceChipControls>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            /*
            foreach (Transform bocata in bocatas)
            {
                StartCoroutine(ActiveBocata(Random.Range(0, 5), bocata));
            }
            */

            if (!firstTime)
            {
                firstTime = true;
                globoColliderEnable();
            }
        }
    }

    private void globoColliderEnable()
    {

        foreach (Globo1script globo in globos)
        {
            globo.transform.gameObject.SetActive(true);
        }

    }

    IEnumerator ActiveBocata(float waitTime, Transform _bocata)
    {
        yield return new WaitForSeconds(waitTime);
        _bocata.gameObject.SetActive(true);
    }

    private void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevel(4f));
    }
    IEnumerator LoadNextLevel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        globosFirs.SetActive(false);
        globosSecond.SetActive(true);
        nextLevel.SetActive(true);

    }

    void OnEnable()
    {
        SpaceShipCollision.OnCollisionDeadly += LoadNextLevel;

    }
    void OnDisable()
    {
        SpaceShipCollision.OnCollisionDeadly -= LoadNextLevel;

    }


}
