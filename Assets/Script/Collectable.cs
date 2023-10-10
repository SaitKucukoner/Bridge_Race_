using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
   public CollectableType collectableType;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
            if(this.collectableType ==other.GetComponent<PlayerController>().collectableType)
            other.GetComponent<PlayerController>().TakeCollectable(gameObject);
            break;

            case "AIPlayer":
             if(this.collectableType ==other.GetComponent<AIController>().collectableType)
            other.GetComponent<AIController>().TakeCollectable(gameObject);
            break;
        }
    }

    
    

    public void SetCollectable(Material CollectableMat , string CollectableTag , CollectableType _collectableType)
    {
       GetComponent<MeshRenderer>().material = CollectableMat;

       this.tag = CollectableTag;

       this.collectableType = _collectableType;

       transform.GetComponent<Collectable>().gameObject.layer = LayerMask.NameToLayer(_collectableType.ToString());
    }
}
