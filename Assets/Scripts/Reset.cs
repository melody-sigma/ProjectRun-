using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    Vector3 isPos;
    // Start is called before the first frame update
    void Start()
    {
        isPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.x <= -42)
        {
            //  gameObject.transform.position = isPos;
            //  gameObject.SetActive(false); 
            Destroy(gameObject);
        }
    }



}
