using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] List<Transform> bocatas = new List<Transform>();
    SpaceChipControls spacechipControls;
    int index = 0;


    [SerializeField] GameObject nextLevel;
    [SerializeField]
    ParticleSystem wind;
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
                StartCoroutine(WindBlow(0.5f));
                StartCoroutine(WindBlow(4f));
                StartCoroutine(WindBlow(8.5f));
                StartCoroutine(PlayerFall(11.5f));
                StartCoroutine(LoadNextLevel(16f));
            }
        }
    }

    IEnumerator WindBlow(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        spacechipControls.force.force = new Vector2(15f, 0);
        wind.Play();

        StartCoroutine(WindBlow2(waitTime * 2));

    }

    IEnumerator WindBlow2(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        wind.Stop();

        spacechipControls.force.force = new Vector2(0f, 0);
    }

    IEnumerator PlayerFall(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        spacechipControls.SpaceChipDead();
    }

    IEnumerator LoadNextLevel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        nextLevel.SetActive(true);
    }
}
