using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPooling : MonoBehaviour
{

    public GameObject[] poolingGround;
    // Start is called before the first frame update
    void Start()
    {

    }




    public void Pooling()
    {
        int i;
        i = Random.Range(0, poolingGround.Length);


        if(poolingGround[i].gameObject.activeSelf == true)
        {
            Debug.Log("Fail!");
            while (poolingGround[i].gameObject.activeSelf == true)
            { i = Random.Range(0, poolingGround.Length); }
            Debug.Log("restart");
        }
        if (poolingGround[i].gameObject.activeSelf == false)
        { 
        poolingGround[i].gameObject.SetActive(true);
        }
        

    }


    public void Pooling2()
    {
        int i;
        i = Random.Range(0, poolingGround.Length);

        Instantiate(poolingGround[i], poolingGround[i].transform.position, poolingGround[i].transform.rotation);


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pool")
        {
            Pooling();
            Debug.Log("Pool");
        }


    }

}
