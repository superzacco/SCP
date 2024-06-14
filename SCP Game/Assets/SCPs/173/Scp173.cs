using UnityEngine;
using UnityEngine.AI;

public class Scp173 : MonoBehaviour
{
    [SerializeField] bool looking;

    GameObject thelittlescpguy;
    Blinking blinkScript;
    public Rigidbody rb;

    public int moveSpeed;


    [Header("Player & Agent")]
    [SerializeField] NavMeshAgent agent;
    GameObject player;


    public void Start()
    {
        player = GameObject.FindWithTag("Player");
        blinkScript = player.GetComponent<Blinking>();
        thelittlescpguy = gameObject;

        agent.speed = moveSpeed;
    }

    private bool LookingAtHim(GameObject thelittlescpguy)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if (GeometryUtility.TestPlanesAABB(planes , thelittlescpguy.GetComponentInChildren<Collider>().bounds))
            return true;
        else
            return false;
    }

    public void FixedUpdate()
    {
        Vector3 towardPlayer = gameObject.transform.position - player.transform.position;

        if (!LookingAtHim(thelittlescpguy) || blinkScript.blinking) 
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.ResetPath();
        }        
    }
}
