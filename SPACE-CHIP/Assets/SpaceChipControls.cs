using Cinemachine;
using System.Collections;
using UnityEngine;

public class SpaceChipControls : MonoBehaviour
{
    public Rigidbody2D rigid;
    public ConstantForce2D force;
    float turn = 100, timer;
    Vector2 mouseOld, oldDirection;
    Vector2 positionCentered;

    public float clampMinusX, clampSumX, timeTurboin, timerTurbo = 2f, zPosition;

    [SerializeField]
    public bool turbo, disabled, readyToControl;
    Vector3 oldPosition;

    public Animator anim;

    //Feedback
    ParticleSystem particleSystem0;
    [SerializeField]
    CinemachineVirtualCamera spaceCamera, startCamera;


    //SFX
    public AudioSource launchLoop, launchExplosion, loadLoop, loadedExplosion, collision, loopFalling, backgroundMusic;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        force = GetComponent<ConstantForce2D>();
        mouseOld = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
        anim = transform.GetComponentInChildren<Animator>();
        particleSystem0 = transform.Find("Ship").GetComponentInChildren<ParticleSystem>();
        spaceCamera = FindObjectOfType<CinemachineVirtualCamera>();
        foreach (AudioSource audio in transform.Find("SFX").GetComponents<AudioSource>())
        {
            if (audio.clip.name == "Espacio mantenido despegue")
            {
                launchLoop = audio;
            }
            if (audio.clip.name == "Suelte  espaciodespegue")
            {
                launchExplosion = audio;
            }
            if (audio.clip.name == "Espacio mantenido")
            {
                loadLoop = audio;
            }
            if (audio.clip.name == "Soltar espacio")
            {
                loadedExplosion = audio;
            }
            if (audio.clip.name == "colision")
            {
                collision = audio;
            }
            if (audio.clip.name == "Loop cayendo")
            {
                loopFalling = audio;
            }
            if (audio.clip.name == "Comet")
            {
                backgroundMusic = audio;
            }

        }
    }
    void Start()
    {
        timerTurbo = 2f;
    }

    private void Update()
    {
        if (timerTurbo > 1.5f && !disabled)
        {
            NoVisualFeedback();

            if (Input.GetKeyDown("space"))
            {
                if (!backgroundMusic.isPlaying)
                {
                    backgroundMusic.Play();
                }

                timeTurboin = 0;
                //First launch
                if (!readyToControl)
                {
                    launchLoop.Play();
                }

                //REst
                else
                {
                    loadLoop.Play();
                }
            }
            if (Input.GetKey("space"))
            {
                //First launch
                if (!readyToControl)
                {
                    vibrationLoop();
                    timeTurboin += 48f * Time.deltaTime;
                }
                //REst
                else
                {
                    timeTurboin += 24f * Time.deltaTime;
                    force.relativeForce = new Vector2(0, Mathf.Lerp(force.relativeForce.y, 0, 0.5f * Time.deltaTime));

                }
            }
            if (Input.GetKeyUp("space"))
            {
                StopVibration();
                turbo = true;
                //First launch

                if (!readyToControl)
                {
                    force.relativeForce = new Vector2(0, Mathf.Clamp(timeTurboin, 0, 36));
                    rigid.AddRelativeForce(new Vector2(0, 40), ForceMode2D.Impulse);
                    StartCameraDisable();

                    launchLoop.Stop();
                    launchExplosion.Play();
                }
                //REst
                else
                {
                    force.relativeForce = new Vector2(0, Mathf.Clamp(timeTurboin, 0, 26));
                    rigid.AddRelativeForce(new Vector2(0, 10), ForceMode2D.Impulse);
                    loadedExplosion.Play();
                    loadLoop.Stop();
                }
                timerTurbo = 0f;

                // resets gravity
                rigid.gravityScale = 0.5f;
                readyToControl = true;

                vibration();
            }
        }
        else if (timerTurbo < 1.5f)
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
            force.relativeForce = new Vector2(0, Mathf.Lerp(force.relativeForce.y, 0, 0.05f * Time.deltaTime));
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
        if (!disabled)
        {
            anim.SetBool("Control", false);
            StartCoroutine(ShipComeback(2F));
            StutterShip();
            if (!collision.isPlaying)
            {
                collision.Play();
            }
        }
    }

    public void SpaceChipDead()
    {
        anim.SetBool("Control", false);
        anim.SetTrigger("GetHit");

        loopFalling.Play();
        loopFalling.volume = 0.2f;
        DisableShip();

    }

    public void ResetChip()
    {
        force.relativeForce = new Vector2(0, 0);
        readyToControl = false;
    }

    IEnumerator ShipComeback(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        anim.SetBool("Control", true);
        disabled = false;
        rigid.gravityScale = 1f;
        StartCoroutine(ReadyToControl(0.5F));
    }
    private void ShipDead()
    {
        disabled = false;
        anim.SetBool("Control", true);

        rigid.gravityScale = 0f;
        loopFalling.Stop();
        loopFalling.volume = 0;

    }


    IEnumerator ReadyToControl(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        rigid.gravityScale = 0.5f;
    }


    private void StutterShip()
    {
        if (!disabled)
        {
            disabled = true;

            //Gravity increased
            rigid.gravityScale = 8f;
            anim.SetTrigger("GetHit");
        }
    }
    private void DisableShip()
    {
        if (!disabled)
        {
            disabled = true;

            //Gravity increased
            rigid.gravityScale = 40f;
            anim.SetTrigger("GetHit");
        }
    }

    void OnEnable()
    {
        SpaceShipCollision.OnCollision += SpaceChipHitted;
        SpaceShipCollision.OnCollisionDeadly += SpaceChipDead;

        SpaceShipCollision.home += ShipDead;

    }
    void OnDisable()
    {
        SpaceShipCollision.OnCollision -= SpaceChipHitted;
        SpaceShipCollision.OnCollisionDeadly -= SpaceChipDead;
        SpaceShipCollision.home -= ShipDead;


    }
}
