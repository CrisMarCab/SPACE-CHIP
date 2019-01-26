using UnityEngine;

public class SpaceChipControls : MonoBehaviour
{
    Vector2 halfScreen;
    Rigidbody2D rigid;
    ConstantForce2D force;
    float degree, degreeToGo, turn = 100, timer;
    Vector2 mouseOld, oldDirection;
    Vector2 positionCentered;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        force = GetComponent<ConstantForce2D>();
        mouseOld = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);

    }

    private void FixedUpdate()
    {
        Cursor.visible = false;

        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        if (mouseOld != mousePosition)
        {
            timer = 0;
            turn = 100;
            positionCentered = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - mouseOld;
            positionCentered = Vector3.Normalize(positionCentered);

            rigid.MoveRotation(rigid.rotation + (-positionCentered.x * turn * Time.deltaTime));
        }

        else
        {
            timer += Time.deltaTime;
            if (timer < 1)
            {
                turn -= timer * 4;
                rigid.MoveRotation(rigid.rotation + (-positionCentered.x * turn * Time.deltaTime));
            }
        }

        mouseOld = mousePosition;

        float horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        rigid.MoveRotation(rigid.rotation + (-horizontal * 150 * Time.deltaTime));

        force.relativeForce = new Vector2(0, 5 * Vertical);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;

    }
}
