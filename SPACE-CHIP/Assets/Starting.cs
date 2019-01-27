using Cinemachine;
using UnityEngine;
public class Starting : MonoBehaviour
{
    SpaceChipControls spaceControls;
    [SerializeField]
    CinemachineVirtualCamera virtualCamera;
    bool shipready = false;
    float yPosition;

    Rigidbody2D manibelaRigid;

    AudioSource wind, manibelaLoop, manibelaTope, Narrative1, Narrative2, Narrative3, Narrative4;
    [SerializeField]
    float desiredLoopVolume = 1;

    [SerializeField] GameObject level1, level2, level3, level4;
    bool boolNivel1, boolNivel2, boolNivel3;


    // Start is called before the first frame update
    void Start()
    {
        spaceControls = FindObjectOfType<SpaceChipControls>();

        manibelaRigid = transform.Find("manibelaFinal").GetComponent<Rigidbody2D>();

        foreach (AudioSource audio in GetComponentsInChildren<AudioSource>())
        {
            if (audio.clip.name == "Manivela loop")
            {
                manibelaLoop = audio;
            }
            else if (audio.clip.name == "Tope manivela")
            {
                manibelaTope = audio;
            }
            else if (audio.clip.name == "1Fall")
            {
                Narrative1 = audio;
            }
            else if (audio.clip.name == "2Wind")
            {
                Narrative2 = audio;
            }
            else if (audio.clip.name == "3Cloud")
            {
                Narrative3 = audio;
            }
            else if (audio.clip.name == "4Home")
            {
                Narrative4 = audio;
            }
            else if (audio.clip.name == "Viento ambiente")
            {
                wind = audio;
            }

        }

        Preparation(true);


    }

    // Update is called once per frame
    void Update()
    {
        if (!shipready)
        {

            float horizontal = Input.GetAxis("Horizontal");

            yPosition = spaceControls.transform.position.y;
            yPosition += horizontal * 3 * Time.deltaTime;

            Vector3 position = new Vector3(0, Mathf.Clamp(yPosition, 0, 11), 0);
            spaceControls.transform.position = position;

            //manibela
            manibelaRigid.MoveRotation(manibelaRigid.rotation + (-horizontal * 200 * Time.deltaTime));

            if (Input.GetAxis("Horizontal") != 0)
            {
                manibelaLoop.volume = desiredLoopVolume;
            }
            else
            {
                manibelaLoop.volume = 0;
            }

        }
        if (yPosition > 10f && !shipready)
        {
            manibelaTope.Play();
            manibelaLoop.volume = 0;
            wind.Stop();

            shipready = true;
            spaceControls.enabled = true;
            spaceControls.disabled = false;
            spaceControls.loopFalling.Stop();

        }
    }


    private void Preparation(bool firstfirsttime)
    {
        virtualCamera.enabled = true;
        spaceControls.rigid.gravityScale = 0f;
        spaceControls.ResetChip();
        spaceControls.loopFalling.Stop();
        spaceControls.anim.SetBool("Control", true);
        spaceControls.enabled = false;

        wind.Play();
        //Reset position
        yPosition = 0;
        Vector3 position = new Vector3(0, Mathf.Clamp(yPosition, 0, 11), 0);

        spaceControls.transform.position = position;

        spaceControls.transform.rotation = Quaternion.identity;
        shipready = false;

        if (!firstfirsttime)
        {
            Narration();
        }
    }

    private void Narration()
    {
        if (level1.activeSelf && !level2.activeSelf && !level3.activeSelf)
        {
            if (boolNivel1)
            {
                //play narrative
                Narrative1.Play();
                boolNivel1 = true;
            }
        }

        if (level1.activeSelf && level2.activeSelf && !level3.activeSelf)
        {
            if (!boolNivel2)
            {
                //play narrative
                Narrative2.Play();
                boolNivel2 = true;

            }
        }
        if (level1.activeSelf && level2.activeSelf && level3.activeSelf)
        {
            if (!boolNivel3)
            {
                //play narrative                
                Narrative3.Play();
                boolNivel3 = true;

            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (shipready)
        {
            Preparation(false);
        }
    }
}
