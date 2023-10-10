using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class AIController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private float radius;

    [SerializeField] private LayerMask LayerMask;

    [SerializeField] private Collider[] AroundCollectables;

    [SerializeField] private Transform Target;
    public Transform StackPoint;

    public List<GameObject> Collected = new List<GameObject>();

    [SerializeField] private Animator animator;

    public CollectableType collectableType;

    [SerializeField] private bool CheckCollectable = true;

    [SerializeField] GameObject FirstBridgeLastStep, SecondBridgeLastStep, ThirdBridgeLastStep;
    [SerializeField] Transform FirstGroundCenter, SecondGroundCenter, LastGroundCenter, LastTarget;

    [SerializeField] private bool First = false, Second = true, Third = false;


    void Start()
    {
        CheckCollectableAround();

    }

    // Update is called once per frame
    void Update()
    {
        SetDestination();

        if (First == false)
            GoFirstBridge();

        if (Second == true)
            GoSecondBridge();

        if (Third == true)
            GoThirdBridge();


    }

    void FixedUpdate()
    {
        CheckCollectableAround();
        FindClosestCollectable();
    }
    private void SetDestination()
    {
        agent.destination = Target.position;
        animator.SetBool("_isRunning", true);

    }
    void CheckCollectableAround()
    {
        Collider[] CollectableAround = Physics.OverlapSphere(transform.position, radius, LayerMask);

        AroundCollectables = CollectableAround;

    }
    void FindClosestCollectable()
    {

        if (CheckCollectable == true)
        {
            float lowestDist = Mathf.Infinity;

            for (int i = 0; i < AroundCollectables.Length; i++)
            {
                float dist = Vector3.Distance(transform.position, AroundCollectables[i].transform.position);

                if (dist < lowestDist)
                {
                    lowestDist = dist;
                    Target = AroundCollectables[i].transform;
                }

            }
        }


    }


    private void GoFirstBridge()
    {
        if (FirstBridgeLastStep.GetComponent<MeshRenderer>().enabled == false)
        {
            if (Collected.Count >= Random.Range(6, 12))
            {
                CheckCollectable = false;
                Target = FirstBridgeLastStep.transform;


            }

            if (Collected.Count == 0)
            {
                Target = FirstGroundCenter;

            }


            if (Vector3.Distance(transform.position, FirstGroundCenter.position) < 1)
            {
                CheckCollectable = true;

            }


        }

        if (FirstBridgeLastStep.GetComponent<MeshRenderer>().enabled == true)
        {
            First = true;
            Second = true;
            Target = SecondGroundCenter;

        }

    }

    private void GoSecondBridge()
    {
        if (FirstBridgeLastStep.GetComponent<MeshRenderer>().enabled == true &&
        SecondBridgeLastStep.GetComponent<MeshRenderer>().enabled == false)
        {
            if (Collected.Count >= Random.Range(6, 12))
            {
                CheckCollectable = false;
                Target = SecondBridgeLastStep.transform;
            }

            if (Collected.Count == 0)
            {
                Target = SecondGroundCenter;

            }


            if (Vector3.Distance(transform.position, SecondGroundCenter.position) < 1)
            {
                CheckCollectable = true;

            }

        }

        if (SecondBridgeLastStep.GetComponent<MeshRenderer>().enabled == true)
        {
            Second = false;
            Third = true;
            CheckCollectable = false;
            Target = LastGroundCenter;

        }

    }

    private void GoThirdBridge()
    {
        if (ThirdBridgeLastStep.GetComponent<MeshRenderer>().enabled == false)
        {

            if (Collected.Count >= Random.Range(6, 12))
            {
                CheckCollectable = false;
                Target = ThirdBridgeLastStep.transform;
            }

            if (Collected.Count == 0)
            {
                Target = LastGroundCenter;

            }

            if (Vector3.Distance(transform.position, LastGroundCenter.position) < 1)
            {
                CheckCollectable = true;

            }

           
        }

         if (ThirdBridgeLastStep.GetComponent<MeshRenderer>().enabled == true)
         Target = LastTarget;

    }










    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
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


        }
    }


}
