using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{
    [SerializeField] List<Transform> bocatas = new List<Transform>();
    SpaceChipControls spacechipControls;
    int index = 0;


    [SerializeField] GameObject globosFirs, globosSecond, nextLevel;
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
                StartCoroutine(PlayerFall(0.5f));

                StartCoroutine(LoadNextLevel(2f));
            }
        }
    }

    IEnumerator ActiveBocata(float waitTime, Transform _bocata)
    {
        yield return new WaitForSeconds(waitTime);
        _bocata.gameObject.SetActive(true);
    }

    IEnumerator PlayerFall(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        spacechipControls.SpaceChipDead();
    }

    IEnumerator LoadNextLevel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        globosFirs.SetActive(false);
        globosSecond.SetActive(true);
        nextLevel.SetActive(true);
        Debug.Log("hello");
    }

}
