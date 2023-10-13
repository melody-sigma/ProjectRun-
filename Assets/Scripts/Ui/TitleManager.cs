using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // 타이틀 화면의 기능을 구현하는 장소
    // 게임 스타트, 게임종료, 환경설정 등

    [TextArea]
    public string title;

    // 게임 스타트 기능 구현
    public void GameStart(string mainScene) 
    {
        // 버튼 클릭 시 메인 씬으로 이동하는 기능
        SceneManager.LoadScene(mainScene); // 인스펙터에 적은 글자와 같은 씬으로 이동
    }


    public void GameExit()
    {
        Application.Quit(); // 종료
    }

}
