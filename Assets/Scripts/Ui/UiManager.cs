using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // UI들을 관리할 스크립트
    // 싱글톤 사용
    private static UiManager m_instance;

    //퍼즈 기능, timeScale 이용할 것.
    private bool pause = false;// 값이 true가 되면 일시정지(timeScale = 0;)

    // 시간 표시 기능
    private float time; // 타이머 오브젝트에 표시될 진행시간 변수
    [Header("혹시나 남은 시간으로 쓸 경우 사용")]
    [SerializeField]private float limitTime; // 타이머 오브젝트에 표시될 남은시간 변수
    private int minTime; // 분 표시
    private int secTime; // 초 표시


    [Header("UI 오브젝트")]
    public Image pauseImage; // 일시정지 상태일 때 표시될 반투명한 이미지
    public Text timeText; // 타이머 변수를 나타낼 텍스트 오브젝트


    public static UiManager instance // 싱글톤
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindAnyObjectByType<UiManager>();
            }

            return m_instance;
        }
    }



    private void Update()
    {
        Timer();
      //  LimitTimer(); // 타이머를 남은 시간으로 쓸 경우를 대비해 만든 것입니다.
    }

    private void Timer() // 현재 진행된 시간 표시, 이후 if문으로 게임오버가 아닐 경우 추가할 것.
    {
        time += Time.deltaTime; // 표시될 변수의 값을 증가시킴

        minTime = (int)time / 60; // 분 표시
        secTime = (int)time % 60; // 초 표시
        timeText.text = minTime.ToString("00") + ":" + secTime.ToString("00");

    }

    private void LimitTimer() // 남은 시간 표시
    {
        limitTime -= Time.deltaTime; // 표시될 변수의 값을 감소

        minTime = (int)limitTime / 60; // 분 표시
        secTime = (int)limitTime % 60; // 초 표시
        timeText.text = minTime.ToString("00") + ":" + secTime.ToString("00");
        

    }


    public void PauseGame() // Time.timeScale을 이용한 게임 일시정지 기능 구현
    {
        if (pause == true) // 현재 pause가 true일 경우 
        {       
            Time.timeScale = 1; // 일시정지를 풀고
            pause = false; // pause를 false로 변경하며
            pauseImage.gameObject.SetActive(false); // 일시정지 이미지를 비활성화
        }
        else // 현재 false일 경우
        { 
            Time.timeScale = 0; // 시간을 0으로 만들고
            pause = true; // pause를 true로 변경하며
            pauseImage.gameObject.SetActive(true); // 일시정지 이미지를 활성화
        }

    }


}
