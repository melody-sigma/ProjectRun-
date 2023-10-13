using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // ���ӸŴ����� �ϼ��Ǹ� �ش� ��ũ��Ʈ���� �����ͼ� �� ��
    public bool isGameOver = false; // �ӽ÷� ����ϴ� ���ӿ��� üũ�� bool��
    public bool isGameClear = false;

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
    private int bestMinTime; // �ִ� �ð� ���� - ��
    private int bestSecTime; // �ִ� �ð� ���� - ��

    //���ھ� ǥ�� ���
    private int score; // ǥ�õ� ���ھ� ����
    private int bestScore; // ����� �ִ� ����

    [Header("Player Hp")]
    public Slider playerHpSlider; // ui�� ������ �����̴� ������Ʈ
    private PlayerController playerController; // ������ hp�� ������ ��ũ��Ʈ

    [Header("UI ������Ʈ")] // �⺻������ ���� ui ������Ʈ ����
    public Image pauseImage; // �Ͻ����� ������ �� ǥ�õ� �������� �̹���
    public Text timeText; // Ÿ�̸� ������ ��Ÿ�� �ؽ�Ʈ ������Ʈ
    public Text scoreText; // ������ ǥ���� �ؽ�Ʈ ������Ʈ
    public Text bestScoreText; // �ְ� ���� ǥ�� �ؽ�Ʈ

    [Header("���â ������Ʈ")] // ���â���� ���� ui ������Ʈ ����
    public GameObject resultObj;
    public Text resultText;
    public Text scoreResult;
    public Text bestScoreResult;
    public Text timeResult;
    public Text bestTimeText;

    [Header("�ְ���â")]
    public GameObject bestScoresObj;
    public Text bestScorePause;
    public Text bestTimePause;
    

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
        bestMinTime = PlayerPrefs.GetInt("BestMinTime", 0); // BestMinTime�� �����´�.
        bestSecTime = PlayerPrefs.GetInt("BestSecTime", 0); // BestSecTime�� �����´�.

        score = 0; // ���� ���� �� ������ 0����
        time = 0; // ���� ���� �� Ÿ�̸Ӹ� 0����
        AddScore(0); // �������ڸ��� ������ �������� ����
    }

    private void Update()
    {
        Clear();
        HpSlider(); // �÷��̾��� HP�� ����
        BestScores();
        //���ӿ��� ���°� �ƴ� ���
        if (isGameOver != true)
        {
            Timer(); // Ÿ�̸� ����
            //  LimitTimer(); // Ÿ�̸Ӹ� ���� �ð����� �� ��츦 ����� ���� ���Դϴ�.

            if (Input.GetKeyDown(KeyCode.Escape)) // Ű���� esc�� ���� ���
            { PauseGame(); } // ���� ��� ���

            
        }

        //���ӿ��� ������ ��� ���â ���, �ش� â�� �׽�Ʈ ��������, �ٸ� ������� ���� �� ����.
        if (isGameOver == true)
        {
            Invoke("GameResult",3); // 3�ʰ� ������ ���Ӱ��â�� ���۽�Ų��.
            return; // ���ư���
        }

    }

    // ���� ����� �ð� ǥ��, ���� if������ ���ӿ����� �ƴ� ��� �߰��� ��
    private void Timer() 
    {
        if (isGameClear != true)
        {
            time += Time.deltaTime; // ǥ�õ� ������ ���� ������Ŵ
        }
        minTime = (int)time / 60; // �� ǥ��
        secTime = (int)time % 60; // �� ǥ��
        timeText.text = minTime.ToString("00") + ":" + secTime.ToString("00");

        BestTime();

        bestTimeText.text = bestMinTime.ToString("00") + ":" + bestSecTime.ToString("00");

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

    // ���� ū �����ð��� ����
    private void BestTime()
    {
 
        if(minTime > bestMinTime) // ���� ���� ��ϵ� �ְ� �к��� ũ��
        {
            PlayerPrefs.SetInt("BestSecTime", 0); // ��ϵ� �ְ� �ʸ� 0���� ����
            PlayerPrefs.SetInt("BestMinTime", minTime); // ���� ���� �ְ� ������ ����
            bestMinTime = PlayerPrefs.GetInt("BestMinTime", 0); // �ְ� �� ������ ��ϵ� �ְ� ������ �ٲ۴�
            bestSecTime = PlayerPrefs.GetInt("BestSecTime",0); // �ְ� �� ������ ��ϵ� 0���� �ٲ۴�
        }

        if (minTime == bestMinTime && secTime > bestSecTime ) // ���� �ְ� ���̶� ���� �ʰ� �ְ� �ʺ��� ũ��
        {

            PlayerPrefs.SetInt("BestSecTime", secTime); // ���� �ʸ� �ְ� �ʷ� ����
            bestSecTime = PlayerPrefs.GetInt("BestSecTime", 0); // �ְ� �� ������ ��ϵ� �ְ� �ʷ� ����
        }


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

    // �÷��̾� HP�� ����
    private void HpSlider()
    {
        //�����ص� ���� playerController�� ���ӿ�����Ʈ Player�� ��ũ��Ʈ PlayerController�� ����
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        //���� �÷��̾� hp �����̴��� value ���� playerController�� ���� hp/ �ִ�hp�� ����
        playerHpSlider.value = playerController.currentHealth / playerController.maxHealth;

      
    }


    private void BestScores()
    {
        bestScorePause.text = "�ְ� ���� :  " + bestScore;
        bestTimePause.text = "���� �ð� :  " + bestMinTime.ToString("00") + ":" + bestSecTime.ToString("00");


        if (bestScoresObj.activeSelf == true && Input.anyKeyDown)
        {
            bestScoresObj.SetActive(false);



        }


    }

    private void Clear()
    {
        if (time >= 180f)
        {
            isGameClear = true;
            GameResult();
        }
    }
}
