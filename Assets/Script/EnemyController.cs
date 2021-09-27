using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyController : BaseCharacterController
{


    [SerializeField]
    private ZombieType type;
    [SerializeField]
    private int maxhp = 5;
    private int hp = 5;
    public Slider hpHUD;


    protected override void Start()
    {
        base.Start();
        speed = 10;
        jumpForce = 3;
        gravitySpeed = 9.8f;
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
        controller.enabled = true;
        AddHUD();
        SetHp();
    }

    private void OnDisable()
    {
        
    }

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }
    public void Onhit()
    {
        hp--;
        if(hp <= 0)
        {
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
        UpdataHUDPosition();
    }

    public void UpdataHUDPosition()
    {
        UIManager.Instance.UpdateHUDPanel(hpHUD,transform.position + transform.up * 2);
    }
}
