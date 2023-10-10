using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private GameObject JoystickBg;

    [SerializeField] private Animator animator;

    public CollectableType collectableType;

    public Transform StackPoint;

    [SerializeField] private float speed;


    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private List<GameObject> Collected = new List<GameObject>();

    [SerializeField] private bool ZAxisOpen = true;

    [SerializeField] private Material mat;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    public void Move()
    {


        if (JoystickBg.activeInHierarchy == true)
        {
            
            animator.SetBool("_isRunning", true);
            if (ZAxisOpen == true)
            {
                Vector3 direction = floatingJoystick.Horizontal * Vector3.right + floatingJoystick.Vertical * Vector3.forward;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 10f * Time.deltaTime);

                transform.position += direction * speed * Time.deltaTime;
            }

            else
            {
                Vector3 direction = (floatingJoystick.Horizontal * Vector3.right + floatingJoystick.Vertical * Vector3.forward) * (speed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 10f * Time.deltaTime);

                float zAxis = direction.z;

                zAxis = Mathf.Clamp(zAxis, -1f, 0f);

                direction = new Vector3(direction.x, direction.y, zAxis);

                transform.position += direction;

            }
        }

        else
        {
            animator.SetBool("_isRunning", false);
        }
    }



    public void TakeCollectable(GameObject Collectable)
    {
        Collectable.transform.SetParent(StackPoint);
        Collected.Add(Collectable);
        Collectable.transform.DOLocalJump(StackPoint.localPosition + Vector3.up * Collected.Count * 0.3f, 1, 1, 0.5f);
        Collectable.transform.rotation = StackPoint.rotation;
        Collectable.GetComponent<Collectable>().gameObject.layer = LayerMask.NameToLayer("Null");

    }


    public void SetBridgeStairs(GameObject Step)
    {
        if (Collected.Count > 0 && Step.GetComponent<MeshRenderer>().enabled == false || Collected.Count > 0 && Step.GetComponent<MeshRenderer>().material.color
         != GetComponentInChildren<SkinnedMeshRenderer>().material.color)
        {

            Step.GetComponent<MeshRenderer>().enabled = true;
            Step.GetComponent<MeshRenderer>().material.color = GetComponentInChildren<SkinnedMeshRenderer>().material.color;

            GameObject LastCollectable = StackPoint.transform.GetChild(StackPoint.childCount - 1).gameObject;
            Collected.Remove(LastCollectable);
            Destroy(LastCollectable);
            ZAxisOpen = true;

        }

        if(Collected.Count == 0 && Step.GetComponent<MeshRenderer>().material.color
         != GetComponentInChildren<SkinnedMeshRenderer>().material.color)
         {
            ZAxisOpen = false;
         }


    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            ZAxisOpen = true;
        }
    }

    

}






