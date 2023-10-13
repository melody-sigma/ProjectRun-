using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // 닿으면 데미지를 입는 장애물 기능 스크립트입니다.
    // 기획상 데미지를 입을 오브젝트가 단 하나이며 더 확장될 가능성이 없기 떄문에 간단하게 만들었습니다.
    // 혹시나 더 확장되야 할 경우 공통적으로 쓸수 있는 데미지 스크립트를 추가할 예정입니다.
    // 사실 인터페이스 만들어서 상속하면 편하게 가능한데 지금 제가 건드릴 수 없는 부분이라 ..

    [Header("입힐 데미지 수치 입력")]
    [SerializeField] private int damage;
    [SerializeField] private bool isMove = false;
    [SerializeField] private float moveSpeed = 1f;



    private void Update()
    {
    }




    // 트리거에 닿을 경우 플레이어의 hp가 깎이는 기능
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        //닿은 오브젝트의 이름이 플레이어라면
        if (collision.gameObject.name == "Player")
        {
            //PlayerController의 현재체력을 지정한 damage만큼 깎음
            collision.gameObject.GetComponent<PlayerController>().currentHealth -= damage;

        }
    }




  


}
