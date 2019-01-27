using Cinemachine;
using System.Collections;
using UnityEngine;

public class SpaceChipControls : MonoBehaviour
{
    public Rigidbody2D rigid;
    ConstantForce2D force;
    float turn = 100, timer;
    Vector2 mouseOld, oldDirection;
    Vector2 positionCentered;

    public float clampMinusX, clampSumX, timeTurboin, timerTurbo, zPosition;
    bool turbo, disabled, readyToControl;
    Vector3 oldPosition;

    Animator anim;

    //Feedback
    ParticleSystem particleSystem0;
    [SerializeField]
    CinemachineVirtualCamera spaceCamera, startCamera;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        force = GetComponent<ConstantForce2D>();
        mouseOld = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
        anim = transform.GetComponentInChildren<Animator>();
        particleSystem0 = transform.Find("Ship").GetComponentInChildren<ParticleSystem>();
        spaceCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (timerTurbo > 1.5f && !disabled)
        {
            NoVisualFeedback();

            if (Input.GetKeyDown("space"))
            {
                timeTurboin = 0;
            }
            if (Input.GetKey("space"))
            {
                if (!readyToControl)
                {
                    vibrationLoop();
                    timeTurboin += 48f * Time.deltaTime;
                }
                else
                {
                    timeTurboin += 24f * Time.deltaTime;
                }
            }
            if (Input.GetKeyUp("space"))
            {
                StopVibration();
                turbo = true;
                if (!readyToControl)
                {
                    force.relativeForce = new Vector2(0, Mathf.Clamp(timeTurboin, 0, 36));
                    rigid.AddRelativeForce(new Vector2(0, 40), ForceMode2D.Impulse);
                    StartCameraDisable();
                }
                else
                {
                    force.relativeForce = new Vector2(0, Mathf.Clamp(timeTurboin, 0, 18));
                    rigid.AddRelativeForce(new Vector2(0, 25), ForceMode2D.Impulse);

                }
                timerTurbo = 0f;
                readyToControl = true;

                // resets gravity
                rigid.gravityScale = 0.5f;

                vibration();
            }
        }
        else
        {
            VisualFeedback();
        }
        timerTurbo += Time.deltaTime;
        //Walls
        rigid.position = new Vector2(Mathf.Clamp(rigid.position.x, clampMinusX, clampSumX), rigid.position.y);

    }

    private void FixedUpdate()
    {
        if (readyToControl && !disabled)
        {
            ShipMovement();
        }
    }
    public void StartCameraDisable()
    {
        startCamera.enabled = false;

    }
    private void ShipMovement()
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

        rigid.MoveRotation(rigid.rotation + (-horizontal * 200 * Time.deltaTime));

        if (turbo)
        {
            force.relativeForce = new Vector2(0, Mathf.Lerp(force.relativeForce.y, 0, 0.35f * Time.deltaTime));
            anim.SetFloat("speed", Mathf.Clamp(force.relativeForce.y / 2f, 1f, 3f));
        }

        /*
        //Resets mouse to centeer if needed
        if (mousePosition.x >= Screen.width * 0.95 || mousePosition.y >= Screen.height * 0.95 || mousePosition.x <= Screen.width * 0.05 || mousePosition.y >= Screen.height * 0.05)
        {
            Debug.Log("hello");

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.lockState = CursorLockMode.None;
        }
        */
    }
    private void VisualFeedback()
    {
        particleSystem0.Play();
    }
    private void NoVisualFeedback()
    {
        particleSystem0.Stop();
    }

    private void vibrationLoop()
    {
        spaceCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 3f;
        spaceCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.05f;

    }
    private void vibration()
    {
        spaceCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 3f;
        spaceCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.05f;
        StartCoroutine(vibrationBack(0.2f));
    }
    IEnumerator vibrationBack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StopVibration();
    }
    private void StopVibration()
    {
        spaceCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
        spaceCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
    }


    private void SpaceChipHitted()
    {

        StartCoroutine(ShipComeback(2F));
        DisableShip(4f);
    }

    public void SpaceChipDead()
    {
        StartCoroutine(ShipDead(10F));
        DisableShip(20f);
    }

    public void ResetChip()
    {
        force.relativeForce = new Vector2(0, 0);
        readyToControl = false;
    }

    IEnumerator ShipComeback(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        disabled = false;
        rigid.gravityScale = 0.5f;
        StartCoroutine(ReadyToControl(0.5F));
    }
    IEnumerator ShipDead(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        disabled = false;
        rigid.gravityScale = 0f;
    }


    IEnumerator ReadyToControl(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        rigid.gravityScale = 0.5f;
    }



    private void DisableShip(float _gravity)
    {
        if (!disabled)
        {
            disabled = true;

            //Gravity increased
            rigid.gravityScale = _gravity;
            anim.SetTrigger("GetHit");
        }
    }

    void OnEnable()
    {
        SpaceShipCollision.OnCollision += SpaceChipHitted;
    }
    void OnDisable()
    {
        SpaceShipCollision.OnCollision -= SpaceChipHitted;
    }


}
