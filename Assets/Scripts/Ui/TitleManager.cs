using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // Ÿ��Ʋ ȭ���� ����� �����ϴ� ���
    // ���� ��ŸƮ, ��������, ȯ�漳�� ��
    [Header("�̵��� �� �̸�")]
    [SerializeField]private string mainScene; // ���� ���� �� ����� �� �̸��� ������ ����


    // ���� ��ŸƮ ��� ����
    public void GameStart() // ��ư Ŭ�� �� ���� ������ �̵��ϴ� ���
    {

        SceneManager.LoadScene(mainScene); // �ν����Ϳ� ���� ���ڿ� ���� ������ �̵�

    }

    public void GameExit() // Ŭ�� �� ���� ���� ���
    {

        Application.Quit();

    }

}
