using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private Rigidbody rb;
    private Animator animator;
    private Vector3 pastPos;
    private float vx;
    private float vz;
    
    private void Start(){
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        pastPos = transform.position;
        vx = 0;
        vz = 0;
    }

    private void Update(){
        Move();
    }

    private void Move(){
        vx = Input.GetAxis("Horizontal");
        vz = Input.GetAxis("Vertical");
        if (vx != 0 || vz != 0){
            animator.SetInteger("legs", 1);
            animator.SetInteger("arms",1);
        }else{
            animator.SetInteger("legs", 0);
            animator.SetInteger("arms",5);
        }
        rb.velocity = new Vector3(-vx * speed, 0, -vz * speed);
        Vector3 diff = transform.position - pastPos;
        float tmp = -diff.x;
        diff.x = diff.z;
        diff.z = tmp;
        if(diff.magnitude > 0.025f){
            transform.rotation = Quaternion.LookRotation(diff);
        }
        pastPos = transform.position;
    }
}
