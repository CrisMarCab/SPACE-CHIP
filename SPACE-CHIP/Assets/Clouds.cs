using System.Collections;
using UnityEngine;

public class Clouds : MonoBehaviour
{

    [SerializeField]
    Collider timer, deadCollider;
    [SerializeField]
    GameObject clouds;
    bool firstCollision = false, deadalready;
    SpaceChipControls spacechip;
    // Start is called before the first frame update
    void Start()
    {
        spacechip = FindObjectOfType<SpaceChipControls>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void startTimer() { }
    private void OnTriggerEnter(Collider other)
    {
        deadalready = false;

        if (!firstCollision)
        {
            StartCoroutine(dissapearClouds(30f));
            firstCollision = true;
            timer.enabled = false;
        }

        else
        {
            deadead();
        }
    }

    IEnumerator dissapearClouds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (!deadalready)
        {
            clouds.SetActive(false);
            deadCollider.enabled = false;
        }
    }

    private void deadead()
    {
        deadalready = true;
        firstCollision = false;
        spacechip.SpaceChipDead();
        clouds.SetActive(true);
        timer.enabled = true;


    }

}
