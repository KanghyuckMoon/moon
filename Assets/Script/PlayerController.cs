using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : BaseCharacterController
{
    public GameObject bulletPref;
    public int currentAmmo;
    public int maxAmmo;
    private bool isReloading = false;
    public CameraController cameraController;
    private Vector2 screenCenterPos;
    public LayerMask mouseColliderLayerMask;
    private Vector3 aimPos;
    public GameObject aimtest;
    public Transform bullectSpawn;
    private int maxHp;
    private int currentHp;

    protected override void Start()
    {
        base.Start();
        maxAmmo = 10;
        SetHp();
        currentAmmo = maxAmmo;
        controller = GetComponent<CharacterController>();
        UpdateUI();
        screenCenterPos = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    private void SetHp()
    {
        maxHp = 20;
        currentHp = maxHp;


    }

    private void UpdateUI()
    {
        UIManager.Instance.ChangeCurrenAmmon(currentAmmo);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPos);
        if(Physics.Raycast(ray, out RaycastHit hit, 999f, mouseColliderLayerMask))
        {
            aimPos = hit.point;
            aimtest.transform.position = hit.point;
        }


        if(Input.GetMouseButtonDown(0) && !isReloading)
        {
            ShotBullet();
        }
    }

    private void FixedUpdate()
    {

        Rotate();
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), moveY, Input.GetAxisRaw("Vertical"));
        moveDir *= speed;
        moveDir = transform.TransformDirection(moveDir);
        PlayerJump();


        moveDir.y = moveY;
        controller.Move(moveDir * Time.deltaTime);
        animator.SetFloat("Speed",controller.velocity.magnitude);
    }

    private void Rotate()
    {
        //transform.eulerAngles = new Vector3(0, Camera.main.transform.rotation.eulerAngles.y, Camera.main.transform.rotation.eulerAngles.z);

        transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, Camera.main.transform.rotation.eulerAngles.z);
    }

    private void ShotBullet()
    {
        currentAmmo--;
        if(currentAmmo <= 0)
        {
            isReloading = true;
            Invoke("Reload", 3f);
        }
        animator.SetTrigger("Shot");
        Vector3 aimDir = (aimPos - bullectSpawn.position).normalized; 
        Instantiate(bulletPref, bullectSpawn.position, Quaternion.LookRotation(aimDir));
        UpdateUI();
    }

    private void Reload()
    {
        isReloading = false;
        currentAmmo = maxAmmo;
        UpdateUI();
    }

    private void PlayerJump()
    {

    }
    
}
