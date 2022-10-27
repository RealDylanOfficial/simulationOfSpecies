using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    // Start is called before the first frame update


    private Transform Transform;
    void Start()
    {
        

        Transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void setWater(float waterLevel)
    {
        if (waterLevel == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            float y = waterLevel;
            
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            gameObject.SetActive(true);
        }
    }
}
