using UnityEngine;

public class SpaceShipCollision : MonoBehaviour
{

    public delegate void Collision();
    public static event Collision OnCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Finish")
        {
            OnCollision();
        }
    }
}