using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
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
        // ������ �ӵ��� �������� �̵��� ��.
        gameObject.transform.position += new Vector3(-1, 0, gameObject.transform.position.z) * moveSpeed * Time.deltaTime;

        if (gameObject.transform.position.x <= -x) // x���� Ư�� ��ġ�� �����ϸ�
        {
            gameObject.transform.position = new Vector3(x, 0, 0); // ������ ������ �̵��Ѵ�.
        }
    }
}
