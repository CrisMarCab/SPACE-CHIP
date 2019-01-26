using UnityEngine;

public class ManibelaRotation : MonoBehaviour
{
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        rigid.MoveRotation(rigid.rotation + (-horizontal * 200 * Time.deltaTime));

    }
}
