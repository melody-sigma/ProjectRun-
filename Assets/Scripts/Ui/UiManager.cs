using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // UI���� ������ ��ũ��Ʈ
    // �̱��� ���
    private static UiManager m_instance;

    //���� ���, timeScale �̿��� ��.
    private bool pause = false;// ���� true�� �Ǹ� �Ͻ�����(timeScale = 0;)

    // �ð� ǥ�� ���
    private float time; // Ÿ�̸� ������Ʈ�� ǥ�õ� ����ð� ����
    [Header("Ȥ�ó� ���� �ð����� �� ��� ���")]
    [SerializeField]private float limitTime; // Ÿ�̸� ������Ʈ�� ǥ�õ� �����ð� ����
    private int minTime; // �� ǥ��
    private int secTime; // �� ǥ��


    [Header("UI ������Ʈ")]
    public Image pauseImage; // �Ͻ����� ������ �� ǥ�õ� �������� �̹���
    public Text timeText; // Ÿ�̸� ������ ��Ÿ�� �ؽ�Ʈ ������Ʈ


    public static UiManager instance // �̱���
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
      //  LimitTimer(); // Ÿ�̸Ӹ� ���� �ð����� �� ��츦 ����� ���� ���Դϴ�.
    }

    private void Timer() // ���� ����� �ð� ǥ��, ���� if������ ���ӿ����� �ƴ� ��� �߰��� ��.
    {
        time += Time.deltaTime; // ǥ�õ� ������ ���� ������Ŵ

        minTime = (int)time / 60; // �� ǥ��
        secTime = (int)time % 60; // �� ǥ��
        timeText.text = minTime.ToString("00") + ":" + secTime.ToString("00");

    }

    private void LimitTimer() // ���� �ð� ǥ��
    {
        limitTime -= Time.deltaTime; // ǥ�õ� ������ ���� ����

        minTime = (int)limitTime / 60; // �� ǥ��
        secTime = (int)limitTime % 60; // �� ǥ��
        timeText.text = minTime.ToString("00") + ":" + secTime.ToString("00");
        

    }


    public void PauseGame() // Time.timeScale�� �̿��� ���� �Ͻ����� ��� ����
    {
        if (pause == true) // ���� pause�� true�� ��� 
        {       
            Time.timeScale = 1; // �Ͻ������� Ǯ��
            pause = false; // pause�� false�� �����ϸ�
            pauseImage.gameObject.SetActive(false); // �Ͻ����� �̹����� ��Ȱ��ȭ
        }
        else // ���� false�� ���
        { 
            Time.timeScale = 0; // �ð��� 0���� �����
            pause = true; // pause�� true�� �����ϸ�
            pauseImage.gameObject.SetActive(true); // �Ͻ����� �̹����� Ȱ��ȭ
        }

    }


}
