
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CollectableType
{
    Green,
    Yellow,
    Blue,
    Null,

}

public class SpawnCollectable : MonoBehaviour
{
    [SerializeField] private CollectableType collectableType;

    public Material Mat_Green, Mat_Yellow, Mat_Blue;

    [SerializeField] private GameObject CollectablePrefab;

    [SerializeField] private float SpawnTimer = 5f;

    void Start()
    {
        StartCol();

    }
    void Update()
    {

        if (HaveChild() == false)
        {
            SpawnTimer -= Time.deltaTime;

        }

        if (SpawnTimer <= 0)
        {
            Spawn();
            SpawnTimer = 5;

        }

    }
    public void StartCol()
    {

        GameObject SpawnCOl = transform.GetChild(0).gameObject;
        switch (collectableType)
        {
            case CollectableType.Green:
                SpawnCOl.GetComponent<Collectable>().SetCollectable(Mat_Green, "Green", CollectableType.Green);
                break;
            case CollectableType.Yellow:
                SpawnCOl.GetComponent<Collectable>().SetCollectable(Mat_Yellow, "Yellow", CollectableType.Yellow);

                break;
            case CollectableType.Blue:
                SpawnCOl.GetComponent<Collectable>().SetCollectable(Mat_Blue, "Blue", CollectableType.Blue);

                break;
        }
    }

    public bool HaveChild()
    {
        return transform.childCount > 0 ? true : false;
    }
    public void Spawn()
    {
        GameObject SpawnCOl = Instantiate(CollectablePrefab, transform);

        switch (collectableType)
        {
            case CollectableType.Green:
                SpawnCOl.GetComponent<Collectable>().SetCollectable(Mat_Green, "Green", CollectableType.Green);
                break;
            case CollectableType.Yellow:
                SpawnCOl.GetComponent<Collectable>().SetCollectable(Mat_Yellow, "Yellow", CollectableType.Yellow);

                break;
            case CollectableType.Blue:
                SpawnCOl.GetComponent<Collectable>().SetCollectable(Mat_Blue, "Blue", CollectableType.Blue);

                break;
        }
    }
}

