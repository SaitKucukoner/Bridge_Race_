using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeStep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
            other.GetComponent<PlayerController>().SetBridgeStairs(gameObject);
            break;

              case "AIPlayer":
            other.GetComponent<AIController>().SetBridgeStairs(gameObject);
            break;
            
        }
    }

    
}
