using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{
    [SerializeField] List<Transform> bocatas = new List<Transform>();
    SpaceChipControls spacechipControls;
    int index = 0;
    GameObject limit, nextLevel;


    private void Start()
    {
        spacechipControls = FindObjectOfType<SpaceChipControls>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(PlayerFall(0.5f));
            /*
            foreach (Transform bocata in bocatas)
            {
                StartCoroutine(ActiveBocata(Random.Range(0, 5), bocata));
            }
            */
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

}
