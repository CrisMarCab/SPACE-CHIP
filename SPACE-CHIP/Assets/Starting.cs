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


    // Start is called before the first frame update
    void Start()
    {
        spaceControls = FindObjectOfType<SpaceChipControls>();

        Preparation();
        manibelaRigid = transform.Find("manibelaFinal").GetComponent<Rigidbody2D>();
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

        }
        if (yPosition > 10f)
        {
            shipready = true;
            spaceControls.enabled = true;
        }
    }


    private void Preparation()
    {
        virtualCamera.enabled = true;

        spaceControls.rigid.gravityScale = 0f;
        spaceControls.ResetChip();
        spaceControls.enabled = false;

        //Reset position
        yPosition = 0;
        Vector3 position = new Vector3(0, Mathf.Clamp(yPosition, 0, 11), 0);

        spaceControls.transform.position = position;

        spaceControls.transform.rotation = Quaternion.identity;
        shipready = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (shipready)
        {
            Preparation();
        }
    }
}
