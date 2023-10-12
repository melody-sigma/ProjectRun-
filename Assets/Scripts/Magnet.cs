using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float magnetForce = 20f;
    public float magnetDuration = 7f;

    private bool magnetActive = false;
    private float currentDuration = 0f;
    private Transform playerTransform; // �÷��̾��� ��ġ�� �����ϱ� ���� ����

    private CircleCollider2D circleCollider;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent <SpriteRenderer>();
        DeactivateMagnet(); // ���� ���� �� ��Ȱ��ȭ
    }

    private void Update()
    {
        if (magnetActive)
        {
            currentDuration += Time.deltaTime;

            if (currentDuration >= magnetDuration)
            {
                DeactivateMagnet();
            }
        }

        if (magnetActive && playerTransform != null)
        {
            // ���׳� ��ġ�� �÷��̾� ��ġ�� �̵�
            transform.position = playerTransform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform; // �÷��̾� ��ġ ���� ����
            ActivateMagnet();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (magnetActive && (other.CompareTag("Coin") || other.CompareTag("Fruit")))
        {
            Rigidbody2D itemRigidbody = other.GetComponent<Rigidbody2D>();
            if (itemRigidbody != null)
            {
                Vector3 direction = (transform.position - other.transform.position).normalized;
                itemRigidbody.AddForce(direction * magnetForce);
            }
        }
    }

    private void ActivateMagnet()
    {
        magnetActive = true;
        currentDuration = 0f;
        spriteRenderer.enabled = false; // ���׳��� �����ϰ�
        circleCollider.enabled = true; // ��Ŭ �ݶ��̴� Ȱ��ȭ
    }

    private void DeactivateMagnet()
    {
        magnetActive = false;
        currentDuration = 0f;
        spriteRenderer.enabled = true; // ���׳��� ���̰�
        circleCollider.enabled = false; // ��Ŭ �ݶ��̴� ��Ȱ��ȭ
        playerTransform = null; // �÷��̾� ��ġ ���� ����
        Destroy(gameObject, 2f); // 2�� �Ŀ� �ش� ���׳� �ı�
    }
}