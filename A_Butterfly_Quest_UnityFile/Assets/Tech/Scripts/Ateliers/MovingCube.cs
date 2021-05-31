using UnityEngine;

public class MovingCube : MonoBehaviour
{
    Vector3 target;
    bool canMoove = true;
    public float mooveClock;
    private Vector3 rayPos;


    [Header("Parametres Raycast")]
    [Range(-1.5f, 1.5f)]
    public float OffsetY;
    public enum Direction { Front, Left, Back, Right };
    public Direction RayDir;

    private bool CollideWithPlayer;
    private Vector3 dir;

    private void Start()
    {
        target = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 10f * Time.deltaTime);

        if (mooveClock > 0)
        {
            mooveClock -= Time.deltaTime;
            canMoove = false;
        }
        else
        {
            if (!checkDown() && !canMoove)
            {
                Debug.Log(checkDown());
                target = new Vector3(transform.position.x, transform.position.y - 3f, transform.position.z);
            }
            canMoove = true;
        }

        rayPos = new Vector3(transform.position.x, transform.position.y + OffsetY, transform.position.z);

        if (CollideWithPlayer)
        {            
            bool raycast = Physics.Raycast(rayPos, dir, 2.99f);
            if (Character3D .m_instance.clockDash > 0 && canMoove && !raycast && dir == transform.forward && ButterflyTypeSelection.Instance.SelectionTypeValue == 2)
            {
                target = new Vector3(transform.position.x, transform.position.y, transform.position.z) + dir * 3f;
                mooveClock = 1f;
            }
        }
    }


    private bool checkDown()
    {
        bool rayTemp = Physics.Raycast(transform.position, -transform.up, 2.99f);
        return rayTemp;
    }


    private void OnCollisionEnter(Collision hit)
    {
        dir = hit.contacts[0].normal;
        CollideWithPlayer = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        CollideWithPlayer = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 rayPosTemp = new Vector3(transform.position.x, transform.position.y + OffsetY, transform.position.z);
        if (RayDir == Direction.Front)
        {
            Gizmos.DrawRay(rayPosTemp, transform.forward * 4.49f);
        }
        else if (RayDir == Direction.Left)
        {
            Gizmos.DrawRay(rayPosTemp, -transform.right * 4.49f);
        }
        else if (RayDir == Direction.Back)
        {
            Gizmos.DrawRay(rayPosTemp, -transform.forward * 4.49f);
        }
        else if (RayDir == Direction.Right)
        {
            Gizmos.DrawRay(rayPosTemp, transform.right * 4.49f);
        }
    }
}
