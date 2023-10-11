using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // ���ӸŴ����� �ϼ��Ǹ� �ش� ��ũ��Ʈ���� �����ͼ� �� ��
    public bool isGameOver = false; // �ӽ÷� ����ϴ� ���ӿ��� üũ�� bool��


    // UI���� ������ ��ũ��Ʈ
    // �̱��� ���
    private static UiManager m_instance;

    //���� ���, timeScale �̿��� ��.
    private bool pause = false;// ���� true�� �Ǹ� �Ͻ�����(timeScale = 0;)

    // �ð� ǥ�� ���
    private float time; // Ÿ�̸� ������Ʈ�� ǥ�õ� ����ð� ����
    [Header("Ȥ�ó� ���� �ð����� �� ��� ���")]
    [Tooltip("���� ����Ѵٸ� �ʴ����� ����� �մϴ�")]
    [SerializeField]private float limitTime; // Ÿ�̸� ������Ʈ�� ǥ�õ� �����ð� ����
    private int minTime; // �� ǥ��
    private int secTime; // �� ǥ��

    //���ھ� ǥ�� ���
    private int score; // ǥ�õ� ���ھ� ����
    private int bestScore; // ����� �ִ� ����


    [Header("UI ������Ʈ")]
    public Image pauseImage; // �Ͻ����� ������ �� ǥ�õ� �������� �̹���
    public Text timeText; // Ÿ�̸� ������ ��Ÿ�� �ؽ�Ʈ ������Ʈ
    public Text scoreText; // ������ ǥ���� �ؽ�Ʈ ������Ʈ
    public Text bestScoreText; // �ְ� ���� ǥ�� �ؽ�Ʈ

    [Header("���â ������Ʈ")]
    public GameObject resultObj;
    public Text resultText;
    public Text scoreResult;
    public Text bestScoreResult;
    public Text timeResult;

    // �̱���
    public static UiManager instance 
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

    private void Awake()
    {
        bestScore = PlayerPrefs.GetInt("BestScore",0); // PlayerPrefs�� BestScore�� �����´�. ���� ��� 0
        score = 0; // ���� ���� �� ������ 0����
        time = 0; // ���� ���� �� Ÿ�̸Ӹ� 0����
        AddScore(0); // �������ڸ��� ������ �������� ����
    }

    private void Update()
    {
        //���ӿ��� ���°� �ƴ� ���
        if (isGameOver != true)
        {
            Timer();
            //  LimitTimer(); // Ÿ�̸Ӹ� ���� �ð����� �� ��츦 ����� ���� ���Դϴ�.

            if (Input.GetKeyDown(KeyCode.Escape)) // Ű���� esc�� ���� ���
            { PauseGame(); } // ���� ��� ���

        }

        //���ӿ��� ������ ��� ���â ���, �ش� â�� �׽�Ʈ ��������, �ٸ� ������� ���� �� ����.
        if (isGameOver == true)
        {
            GameResult();
            return;
        }

    }

    // ���� ����� �ð� ǥ��, ���� if������ ���ӿ����� �ƴ� ��� �߰��� ��
    private void Timer() 
    {
        time += Time.deltaTime; // ǥ�õ� ������ ���� ������Ŵ

        minTime = (int)time / 60; // �� ǥ��
        secTime = (int)time % 60; // �� ǥ��
        timeText.text = minTime.ToString("00") + ":" + secTime.ToString("00");

    }

    // ���� �ð� ǥ�� ���, Ȥ�ó� ����� ��츦 ����� �̸� ������ ��
    private void LimitTimer() 
    {
        limitTime -= Time.deltaTime; // ǥ�õ� ������ ���� ����

        minTime = (int)limitTime / 60; // �� ǥ��
        secTime = (int)limitTime % 60; // �� ǥ��
        timeText.text = minTime.ToString("00") + ":" + secTime.ToString("00");
        

    }

    // Time.timeScale�� �̿��� ���� �Ͻ����� ��� ����
    public void PauseGame() 
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

    // ������ _score����ŭ ���� ������ �����ϴ� ���
    public void AddScore(int _score) 
    {
        score += _score; // ������ int _score����ŭ score ������ ����
        scoreText.text = score.ToString(); // ���� �ؽ�Ʈ�� �ؽ�Ʈ�� score ��ġ�� ����
        
        BestScore(); // ���� ������ ����� �ְ��������� ���� �� �ְ����� ����

        bestScoreText.text = bestScore.ToString(); // �ְ����� �ؽ�Ʈ ����
    }

    // ���� ������ ����� �ְ��������� ���� �� �ְ����� ����
    private void BestScore() 
    {
        if(score > bestScore) // score�� ���� bestScore���� ���� ���
        {
            PlayerPrefs.SetInt("BestScore",score); // �ְ� ����
            bestScore = PlayerPrefs.GetInt("BestScore", 0); // ���ŵ� ���� �ٽ� bestScore�� �־���
        }
    }

    // ���� ����� ���, ��ư�� OnClick�� �ش��ϴ� �� �̸��� '��Ȯ�ϰ�' ���� ��. 
    // ������ ������ ª�Ƽ� ���� ������� �߽��ϴٸ� �̰� ���´ٰ� �� �����...
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // sceneName�� ������ �� �̸��� �°� ������� ��.
    }

    // Ŭ�� �� ���� ���� ���
    public void GameExit()
    {
        Application.Quit(); // ����
    }

    // ���� ���â�� ���� �ؽ�Ʈ
    public void GameResult() 
    {
        resultObj.SetActive(true); // ���� ���� �� ������ UI ������Ʈ�� Ȱ��ȭ
        scoreResult.text = "���� ���� : " + score.ToString(); // ���� ���ھ� �ؽ�Ʈ
        bestScoreResult.text = "�ְ� ���� : " + bestScore.ToString(); // ���� ����Ʈ ���ھ� �ؽ�Ʈ
        timeResult.text = "���� �ð� : " + timeText.text; // ���ӿ��� �� �ð� �ؽ�Ʈ

        if(isGameOver == true) // ���ӿ��� ������ ��� Ż�� ����, �ƴ� ��� Ż�� ���� �ؽ�Ʈ�� �������� ��
        { resultText.text = "Ż�� ����.."; }
        else { resultText.text = "Ż�� ����!!"; }

    }
}
