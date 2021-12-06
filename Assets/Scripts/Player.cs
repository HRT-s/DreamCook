using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Camera myCamera;
    private Rigidbody rb;
    private Animator animator;
    private GameManager gm;
    private float vx,vz;
    private Transform initTransform;
    private Vector3 dir;
    private int state;
    private List<string> recipe;
    private List<GameObject> items;
    public List<string> Recipe{get{return recipe;} set{recipe = value;}}

    public void Reset(){
        transform.position = initTransform.position;
        transform.rotation = initTransform.rotation;
    }

    private void Start(){
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        vx = 0;
        vz = 0;
        initTransform = transform;
        dir = new Vector3(-10,0,transform.position.z);
        state = 0;
        recipe = new List<string>();
        items = new List<GameObject>();
    }

    private void Update(){
        state = gm.State;
        if(state == 1){
            Move();
        }
    }

    private void Move(){
        vx = Input.GetAxis("Horizontal");
        vz = Input.GetAxis("Vertical");
        if (vx != 0 || vz != 0){
            animator.SetInteger("legs", 1);
            animator.SetInteger("arms", 1);
            if(vz == 0){
                dir.x = transform.position.x;
            }else if(Math.Abs(vz) > 0.1){
                dir.x = -1000*vz;
            }
            if(vx == 0){
                dir.z = transform.position.z;
            }else if(Math.Abs(vx) > 0.1){
                dir.z = 1000*vx;
            }
        }else{
            animator.SetInteger("legs", 0);
            animator.SetInteger("arms", 5);
        }
        rb.velocity = new Vector3(-vx * speed, 0, -vz * speed);
        transform.rotation = Quaternion.LookRotation(dir);
        myCamera.transform.rotation = Quaternion.LookRotation(dir);
        myCamera.transform.Rotate(new Vector3(30,-90,0));
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("recipe") && Input.GetMouseButtonDown(0)){
            gm.GetRecipe(other.gameObject.name);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionStay(Collision other) {
        if(other.gameObject.CompareTag("bed") && Input.GetMouseButtonDown(0) && recipe.Count != 0){
            gm.BedTouch();
        }
    }
}
