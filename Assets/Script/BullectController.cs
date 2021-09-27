using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullectController : MonoBehaviour
{
    private Rigidbody rigid;

    [SerializeField]
    private float power = 5;
    [SerializeField]
    private float maxDistance = 5;
    private Vector3 firstPosition;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        transform.Rotate(90, 0, 0);
    }

    private void Start()
    {
        rigid.AddForce(transform.up * power);
        firstPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(firstPosition,transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Enemy":
                collision.gameObject.GetComponent<EnemyController>().Onhit();
                break;
            case "bullet":
                break;
        }
    }

    private void OnColiderEnter(Collider other)
    {
           
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
