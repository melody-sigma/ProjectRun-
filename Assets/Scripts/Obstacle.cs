using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // ������ �������� �Դ� ��ֹ� ��� ��ũ��Ʈ�Դϴ�.
    // ��ȹ�� �������� ���� ������Ʈ�� �� �ϳ��̸� �� Ȯ��� ���ɼ��� ���� ������ �����ϰ� ��������ϴ�.
    // Ȥ�ó� �� Ȯ��Ǿ� �� ��� ���������� ���� �ִ� ������ ��ũ��Ʈ�� �߰��� �����Դϴ�.
    // ��� �������̽� ���� ����ϸ� ���ϰ� �����ѵ� ���� ���� �ǵ帱 �� ���� �κ��̶� ..

    [Header("���� ������ ��ġ �Է�")]
    [SerializeField] private int damage;
    [SerializeField] private bool isMove = false;
    [SerializeField] private float moveSpeed = 1f;

    private void Update()
    {
        MoveMonster();
    }



    // Ʈ���ſ� ���� ��� �÷��̾��� hp�� ���̴� ���
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        //���� ������Ʈ�� �̸��� �÷��̾���
        if (collision.gameObject.name == "Player")
        {
            //PlayerController�� ����ü���� ������ damage��ŭ ����
            collision.gameObject.GetComponent<PlayerController>().currentHealth -= damage;

        }
    }



    private void MoveMonster()
    {

        if (isMove) { gameObject.transform.position += new Vector3(-1,0,gameObject.transform.position.z) * moveSpeed * Time.deltaTime; }
        else { return; }

    }


}
