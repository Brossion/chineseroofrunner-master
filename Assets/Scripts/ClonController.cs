using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonController : MonoBehaviour
{
    public GameObject car;

    private float deleteTime = 17.0f;
    private float rightX = -3f;
    private float leftX = 3f;

    SwipeManager playerobject;
    // Start is called before the first frame update
    void Start()
    {
        playerobject = GameObject.Find("Player").GetComponent<SwipeManager>();
        InvokeRepeating("cloning", 2, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void cloning()
    {
        int random = Random.Range(0, 5);
        if (random < 2)
        {
            createClon(car, -12.5f);
        }
    }
    void createClon(GameObject gameObject, float yPos)
    {
        GameObject newClone = Instantiate(gameObject);
        int random = Random.Range(0, 5);
        if (random > 2)
        {
            newClone.transform.position = new Vector3(rightX, yPos, 650);
        }
        else
        {
            newClone.transform.position = new Vector3(leftX, yPos, 670);
        }
        Destroy(newClone, deleteTime);
    }
}
