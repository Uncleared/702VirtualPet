using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CatBehaviour : MonoBehaviour
{

    public enum CatState
    {

    }
    public Transform owner;

    bool wandering = true;
    public Animator animator;
    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;

    public Vector3 previousPosition;
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        timer = wanderTimer;
    }

    private bool HasReachedDestination()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }
        return false;
    }

    public void GoToOwner()
    {
        wandering = false;
        agent.SetDestination(owner.position);
   

    }

    public void Bounce()
    {
        IEnumerator Anim()
        {
            wandering = false;
            animator.SetTrigger("Bounce");
            agent.isStopped = true;
            yield return new WaitForSeconds(1f);
            agent.isStopped = false;
            wandering = true;
        }
        StartCoroutine(Anim());
    }
    void Update()
    {
        Vector3 lookRotation = (transform.position - previousPosition).normalized;
        if(lookRotation.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRotation), Time.deltaTime * 2f);
        }

        if (!wandering)
        {
            if(HasReachedDestination())
            {
                animator.SetTrigger("Roll");
                wandering = true;
            }
        }
        float movementSpeed = (transform.position - previousPosition).magnitude * 100f;
        animator.SetFloat("MovementSpeed", movementSpeed);

        if(wandering)
        {
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(Vector3.zero, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }
      
        previousPosition = transform.position;
    }
    public Vector3 ClampMinMagnitude(Vector3 vector, float minMagnitude)
    {
        if (vector.magnitude < minMagnitude)
        {
            vector = vector.normalized * minMagnitude;
        }
        return vector;
    }


    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection = ClampMinMagnitude(randDirection, dist * 0.5f);

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}