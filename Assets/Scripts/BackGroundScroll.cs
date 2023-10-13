using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{


    public GameObject pool;
    [Header("�ʱ�ȭ�� x��ǥ ��ġ �� �̵��ӵ� ����")]

    [SerializeField]private float x = 19.5f; // x���� -19.5f�϶� 19.5f�� ����

    [SerializeField]private float moveSpeed = 2f; // �̵��ӵ�
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UiManager.instance.isGameOver != true)
        { 
        // ������ �ӵ��� �������� �̵��� ��.
        gameObject.transform.position += new Vector3(-1, 0, gameObject.transform.position.z) * moveSpeed * Time.deltaTime;

        if (gameObject.transform.position.x <= -x) // x���� Ư�� ��ġ�� �����ϸ�
        {
            gameObject.transform.position = new Vector3(x, 0, 0); // ������ ������ �̵��Ѵ�.
        }

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pool != null && UiManager.instance.isGameClear != true)
        {
            if (gameObject.tag == "Ground" && collision.gameObject.tag == "Pool")
            {
                Debug.Log("����");
                // pool.gameObject.GetComponent<GroundPooling>().Pooling();
                pool.gameObject.GetComponent<GroundPooling>().Pooling2();
            }
        }
    }
}
