using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{
    List<Transform> bocatas = new List<Transform>();
    SpaceChipControls spacechipControls;
    private void Start()
    {
        spacechipControls = FindObjectOfType<SpaceChipControls>();
    }
    private void OnTriggerEnter(Collider other)
    {
        spacechipControls.SpaceChipDead();
    }
}
