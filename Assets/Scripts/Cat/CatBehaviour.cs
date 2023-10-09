using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class CatBehaviour : MonoBehaviour
{
    public GameObject heartParticles;
    public TextMeshProUGUI scoreText;
    public int score = 0;

    public Image affectionMeter;
    public float affection = 0f;
    public float maxAffection = 10f;

    public AudioSource audioSource;
    public AudioClip pain;

    public enum CatState
    {

    }

    public Transform owner;

    bool wandering = true;
    bool dead = false;
    public Animator animator;
    public float wanderRadius;
    public float wanderTimer;

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

    public void Death()
    {
        if(dead)
        {
            return;
        }
        IEnumerator DeathCoroutine()
        {
            score = (int)(score * 0.5f);
            animator.SetTrigger("Death");
            dead = true;

            yield return new WaitForSeconds(3f);
            dead = false;
            animator.SetTrigger("Idle");

        }
        StartCoroutine(DeathCoroutine());
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

    float lastTouch = 0f;

    public void AddAffection(float rate)
    {
        if(dead)
        {
            return;
        }
        lastTouch = 2f;

        affection += Time.deltaTime * rate;
    }

    void Update()
    {
        scoreText.text = "Score: " + score;
        if(lastTouch > 0f)
        {
            lastTouch -= Time.deltaTime;
        }
        if(affection > 0f)
        {
            affection -= Time.deltaTime * (1f - lastTouch/2f);
        }

        if(lastTouch > 1.9f)
        {
            score += (int)(affection * 5f);
            heartParticles.SetActive(true);
        }
        else
        {
            heartParticles.SetActive(false);
            score -= 1;
        }

        if (affection > maxAffection)
        {
            Death();
            audioSource.PlayOneShot(pain);
            affection = 0f;
        }
        affectionMeter.fillAmount = affection / maxAffection;

        Vector3 lookRotation = (transform.position - previousPosition).normalized;
        if(lookRotation.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRotation), Time.deltaTime * 2f);
        }

        if (!wandering && !dead)
        {
            if(HasReachedDestination())
            {
                animator.SetTrigger("Roll");
                wandering = true;
            }
        }
        float movementSpeed = (transform.position - previousPosition).magnitude * 100f;
        animator.SetFloat("MovementSpeed", movementSpeed);

        if(wandering && !dead)
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