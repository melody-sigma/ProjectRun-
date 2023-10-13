using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // Ÿ��Ʋ ȭ���� ����� �����ϴ� ���
    // ���� ��ŸƮ, ��������, ȯ�漳�� ��

    [TextArea]
    public string title;

    // ���� ��ŸƮ ��� ����
    public void GameStart(string mainScene) 
    {
        // ��ư Ŭ�� �� ���� ������ �̵��ϴ� ���
        SceneManager.LoadScene(mainScene); // �ν����Ϳ� ���� ���ڿ� ���� ������ �̵�
    }


    public void GameExit()
    {
        Application.Quit(); // ����
    }

}
