using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float magnetForce = 20f;
    public float magnetDuration = 5f;

    public GameObject whiteCircle;

    private bool magnetActive = false;
    private float currentDuration = 0f;
    private Transform playerTransform; // 플레이어의 위치를 추적하기 위한 변수

    private CircleCollider2D circleCollider;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent <SpriteRenderer>();
        //DeactivateMagnet();
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
            // 마그넷 위치를 플레이어 위치로 이동
            transform.position = playerTransform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform; // 플레이어 위치 추적 시작
            ActivateMagnet();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (magnetActive && (other.CompareTag("Coin")))
        {
            Rigidbody2D itemRigidbody = other.GetComponent<Rigidbody2D>();
            if (itemRigidbody != null)
            {
                Vector3 direction = (transform.position - other.transform.position).normalized;
                Vector2 force = direction * magnetForce;
                itemRigidbody.velocity = force;
            }
        }
    }

    private void ActivateMagnet()
    {
        magnetActive = true;
        currentDuration = 0f;
        spriteRenderer.enabled = false; // 마그넷을 투명하게
        circleCollider.enabled = true; // 서클 콜라이더 활성화
        whiteCircle.SetActive(true);
    }

    private void DeactivateMagnet()
    {
        magnetActive = false;
        currentDuration = 0f;
        whiteCircle.SetActive(false);
        Destroy(gameObject);
    }
}