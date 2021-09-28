using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class EnemyController : BaseCharacterController
{


    [SerializeField]
    private ZombieType type;
    [SerializeField]
    private int maxhp = 5;
    private int hp = 5;
    public Slider hpHUD;
    public NavMeshAgent navMeshAgent;
    public Transform distanceTransform;
    private SphereCollider sphereCollider;
    private bool moving;
    private bool dead;


    protected override void Start()
    {
        base.Start();
        speed = 10;
        jumpForce = 3;
        gravitySpeed = 9.8f;
        sphereCollider = GetComponent<SphereCollider>();
        distanceTransform = FindObjectOfType<PlayerController>().transform;
    }

    private void SetHp()
    {
        switch(type)
        {
            case ZombieType.Basic:
                maxhp = 5;
                hp = 5;
                break;
            case ZombieType.Strong:
                maxhp = 10;
                hp = 10;
                break;
            default:
                maxhp = 5;
                hp = 5;
                break;
        }
    }

    private void OnEnable()
    {
        //controller.enabled = true;
        AddHUD();
        SetHp();
    }

    private void OnDisable()
    {
        
    }

    protected override void Awake()
    {
        base.Awake();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    public void Onhit()
    {
        hp--;
        if(hp == 0)
        {
            dead = true;
            navMeshAgent.isStopped = true;
            controller.enabled = false;
            hpHUD.gameObject.SetActive(false);
            animator.SetTrigger("Dead");
            Invoke("DestroyZombie", 3);
        }
        else
        {
            hpHUD.value = (float)hp / maxhp;
        }
    }

    public void DestroyZombie()
    {
        gameObject.SetActive(false);
        TutorialEnemyGenerator.Instance.Zombiess(type,gameObject);
    }

    private void AddHUD()
    {
        hpHUD = UIManager.Instance.AddEnemyHUD();
    }

    private void LateUpdate()
    {
        if (dead) return;
        if (navMeshAgent.remainingDistance <= 0)
        {
            if(moving)
            {
                moving = true;
                float delay = Random.Range(1, 4);
                Invoke("MoveToRandomPosition", delay);
            }
            
        }
        animator.SetFloat("Speed",navMeshAgent.velocity.magnitude);
        UpdataHUDPosition();
        
    }

    private void MoveToRandomPosition()
    {
        moving = true;
        float redius = 100;
        Vector3 random = Random.insideUnitSphere * redius;
        Vector3 destination = Vector3.zero;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(random, out hit, redius, 1))
        {
            destination = hit.position;
        }
        else
        {
            destination = transform.position - random;
        }
        navMeshAgent.SetDestination(destination);
    }

    public void UpdataHUDPosition()
    {
        UIManager.Instance.UpdateHUDPanel(hpHUD,transform.position + transform.up * 2);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            navMeshAgent.SetDestination(distanceTransform.position);
        }
    }
}
