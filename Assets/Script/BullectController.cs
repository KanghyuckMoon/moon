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
        if (collision.gameObject.tag == "Enemy")
        {
            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<EnemyController>().Onhit();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
