using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-direction * Time.deltaTime * 12);

        if (transform.position.x > 35 || transform.position.x < -35)
        {
            direction = -direction;
            Debug.Log(direction);
            Vector3 scale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            transform.localScale = scale;

        }


    }
}
