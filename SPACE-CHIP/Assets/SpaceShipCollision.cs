using UnityEngine;

public class SpaceShipCollision : MonoBehaviour
{

    public delegate void Collision();
    public static event Collision OnCollision;

    public delegate void CollisionDeadly();
    public static event Collision OnCollisionDeadly;


    public delegate void HomeStation();
    public static event HomeStation home;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Finish")
        {
            if (other.tag == "Respawn")
            {
                OnCollisionDeadly();
            }
            if (other.tag == "Player")
            {
                home();
            }
            else
            {
                OnCollision();
            }


        }
    }
}