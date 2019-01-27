using System.Collections.Generic;
using UnityEngine;

public class Globo1script : MonoBehaviour
{
    float rotation, direction, timer;
    Vector3 position;

    List<Collider> colliders = new List<Collider>();
    // Start is called before the first frame update
    void Start()
    {
        rotation = Random.Range(-1, 1);
        direction = Random.Range(-0.05f, 0.05f);
        foreach (Collider col in GetComponentsInChildren<Collider>())
        {
            colliders.Add(col);
        }

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            direction = -direction;
            timer = 0;
        }
        transform.Rotate(Vector3.up * rotation, Space.World);
        position = transform.position;

        transform.position = position + new Vector3(0, direction, 0);
    }

    public void tagstatus(string _status)
    {
        transform.tag = _status;
    }
}
