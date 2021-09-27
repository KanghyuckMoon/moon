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

    protected override void Start()
    {
        base.Start();
        maxAmmo = 10;
        currentAmmo = maxAmmo;
        UpdateUI();
    }

    private void UpdateUI()
    {
        UIManager.Instance.ChangeCurrenAmmon(currentAmmo);
    }

    private void Update()
    {
        //
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
        Instantiate(bulletPref, transform.position + transform.forward * 1f + transform.up * 1f, Quaternion.Euler(Camera.main.transform.rotation.x + 90, transform.eulerAngles.y,0));
        UpdateUI();
    }

    private void Reload()
    {
        isReloading = false;
        currentAmmo = maxAmmo;
        UpdateUI();
    }
    
}
