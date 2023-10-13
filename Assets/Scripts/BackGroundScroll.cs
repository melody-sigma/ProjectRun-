using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{


    public GameObject pool;
    [Header("초기화될 x좌표 위치 및 이동속도 지정")]

    [SerializeField]private float x = 19.5f; // x축이 -19.5f일때 19.5f로 변경

    [SerializeField]private float moveSpeed = 2f; // 이동속도
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UiManager.instance.isGameOver != true)
        { 
        // 지정된 속도로 왼쪽으로 이동할 것.
        gameObject.transform.position += new Vector3(-1, 0, gameObject.transform.position.z) * moveSpeed * Time.deltaTime;

        if (gameObject.transform.position.x <= -x) // x값이 특정 수치에 도달하면
        {
            gameObject.transform.position = new Vector3(x, 0, 0); // 지정된 값으로 이동한다.
        }

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pool != null && UiManager.instance.isGameClear != true)
        {
            if (gameObject.tag == "Ground" && collision.gameObject.tag == "Pool")
            {
                Debug.Log("접촉");
                // pool.gameObject.GetComponent<GroundPooling>().Pooling();
                pool.gameObject.GetComponent<GroundPooling>().Pooling2();
            }
        }
    }
}
