using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // 게임매니저가 완성되면 해당 스크립트에서 가져와서 쓸 것
    public bool isGameOver = false; // 임시로 사용하는 게임오버 체크용 bool값


    // UI들을 관리할 스크립트
    // 싱글톤 사용
    private static UiManager m_instance;

    //퍼즈 기능, timeScale 이용할 것.
    private bool pause = false;// 값이 true가 되면 일시정지(timeScale = 0;)

    // 시간 표시 기능
    private float time; // 타이머 오브젝트에 표시될 진행시간 변수
    [Header("혹시나 남은 시간으로 쓸 경우 사용")]
    [Tooltip("만약 사용한다면 초단위로 적어야 합니다")]
    [SerializeField]private float limitTime; // 타이머 오브젝트에 표시될 남은시간 변수
    private int minTime; // 분 표시
    private int secTime; // 초 표시
    private int bestMinTime; // 최대 시간 저장 - 분
    private int bestSecTime; // 최대 시간 저장 - 초

    //스코어 표시 기능
    private int score; // 표시될 스코어 변수
    private int bestScore; // 저장될 최대 점수

    [Header("Player Hp")]
    public Slider playerHpSlider; // ui에 적용할 슬라이더 오브젝트
    private PlayerController playerController; // 가져올 hp를 구현한 스크립트

    [Header("UI 오브젝트")] // 기본적으로 보일 ui 오브젝트 삽입
    public Image pauseImage; // 일시정지 상태일 때 표시될 반투명한 이미지
    public Text timeText; // 타이머 변수를 나타낼 텍스트 오브젝트
    public Text scoreText; // 점수를 표시할 텍스트 오브젝트
    public Text bestScoreText; // 최고 점수 표시 텍스트

    [Header("결과창 오브젝트")] // 결과창에서 보일 ui 오브젝트 삽입
    public GameObject resultObj;
    public Text resultText;
    public Text scoreResult;
    public Text bestScoreResult;
    public Text timeResult;
    public Text bestTimeText;

    [Header("최고기록창")]
    public GameObject bestScoresObj;
    public Text bestScorePause;
    public Text bestTimePause;
    

    // 싱글톤
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
        bestScore = PlayerPrefs.GetInt("BestScore",0); // PlayerPrefs의 BestScore을 가져온다. 없을 경우 0
        bestMinTime = PlayerPrefs.GetInt("BestMinTime", 0); // BestMinTime을 가져온다.
        bestSecTime = PlayerPrefs.GetInt("BestSecTime", 0); // BestSecTime을 가져온다.

        score = 0; // 게임 시작 시 점수를 0으로
        time = 0; // 게임 시작 시 타이머를 0으로
        AddScore(0); // 시작하자마자 점수가 보여지기 위함
    }

    private void Update()
    {

        HpSlider(); // 플레이어의 HP바 연동
        BestScores();
        //게임오버 상태가 아닐 경우
        if (isGameOver != true)
        {
            Timer(); // 타이머 동작
            //  LimitTimer(); // 타이머를 남은 시간으로 쓸 경우를 대비해 만든 것입니다.

            if (Input.GetKeyDown(KeyCode.Escape)) // 키보드 esc를 누를 경우
            { PauseGame(); } // 퍼즈 기능 사용

            
        }

        //게임오버 상태일 경우 결과창 출력, 해당 창은 테스트 버전으로, 다른 방식으로 열릴 수 있음.
        if (isGameOver == true)
        {
            Invoke("GameResult",3); // 3초가 지나면 게임결과창을 동작시킨다.
            return; // 돌아간다
        }

    }

    // 현재 진행된 시간 표시, 이후 if문으로 게임오버가 아닐 경우 추가할 것
    private void Timer() 
    {
        time += Time.deltaTime; // 표시될 변수의 값을 증가시킴

        minTime = (int)time / 60; // 분 표시
        secTime = (int)time % 60; // 초 표시
        timeText.text = minTime.ToString("00") + ":" + secTime.ToString("00");

        BestTime();

        bestTimeText.text = bestMinTime.ToString("00") + ":" + bestSecTime.ToString("00");

    }

    // 남은 시간 표시 기능, 혹시나 써야할 경우를 대비해 미리 만들어둔 것
    private void LimitTimer() 
    {
        limitTime -= Time.deltaTime; // 표시될 변수의 값을 감소

        minTime = (int)limitTime / 60; // 분 표시
        secTime = (int)limitTime % 60; // 초 표시
        timeText.text = minTime.ToString("00") + ":" + secTime.ToString("00");
        

    }

    // Time.timeScale을 이용한 게임 일시정지 기능 구현
    public void PauseGame() 
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

    // 지정한 _score값만큼 점수 변수값 증가하는 기능
    public void AddScore(int _score) 
    {
        score += _score; // 지정한 int _score값만큼 score 변수값 증가
        scoreText.text = score.ToString(); // 점수 텍스트의 텍스트를 score 수치로 변경
        
        BestScore(); // 현재 점수가 저장된 최고점수보다 높을 시 최고점수 갱신

        bestScoreText.text = bestScore.ToString(); // 최고점수 텍스트 갱신
    }

    // 현재 점수가 저장된 최고점수보다 높을 시 최고점수 갱신
    private void BestScore() 
    {
        if(score > bestScore) // score의 값이 bestScore보다 높을 경우
        {
            PlayerPrefs.SetInt("BestScore",score); // 최고값 갱신
            bestScore = PlayerPrefs.GetInt("BestScore", 0); // 갱신된 값을 다시 bestScore에 넣어줌
        }
    }

    // 게임 재시작 기능, 버튼의 OnClick에 해당하는 씬 이름을 '정확하게' 적을 것. 
    // 게임의 볼륨이 짧아서 적는 방식으로 했습니다만 이거 적는다고 더 길어짐...
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // sceneName을 변경할 씬 이름에 맞게 적어줘야 함.
    }

    // 가장 큰 생존시간을 저장
    private void BestTime()
    {
 
        if(minTime > bestMinTime) // 현재 분이 기록된 최고 분보다 크면
        {
            PlayerPrefs.SetInt("BestSecTime", 0); // 기록된 최고 초를 0으로 변경
            PlayerPrefs.SetInt("BestMinTime", minTime); // 현재 분을 최고 분으로 저장
            bestMinTime = PlayerPrefs.GetInt("BestMinTime", 0); // 최고 분 변수를 기록된 최고 분으로 바꾼다
            bestSecTime = PlayerPrefs.GetInt("BestSecTime",0); // 최고 초 변수를 기록된 0으로 바꾼다
        }

        if (minTime == bestMinTime && secTime > bestSecTime ) // 분이 최고 분이랑 같고 초가 최고 초보다 크면
        {

            PlayerPrefs.SetInt("BestSecTime", secTime); // 현재 초를 최고 초로 저장
            bestSecTime = PlayerPrefs.GetInt("BestSecTime", 0); // 최고 초 변수를 기록된 최고 초로 변경
        }


    }


    // 클릭 시 게임 종료 기능
    public void GameExit()
    {
        Application.Quit(); // 종료
    }

    // 게임 결과창에 나올 텍스트
    public void GameResult() 
    {
        resultObj.SetActive(true); // 게임 오버 시 등장할 UI 오브젝트를 활성화
        scoreResult.text = "최종 점수 : " + score.ToString(); // 최종 스코어 텍스트
        bestScoreResult.text = "최고 점수 : " + bestScore.ToString(); // 최종 베스트 스코어 텍스트
        timeResult.text = "진행 시간 : " + timeText.text; // 게임오버 시 시간 텍스트

        if(isGameOver == true) // 게임오버 상태일 경우 탈출 실패, 아닐 경우 탈출 성공 텍스트가 나오도록 함
        { resultText.text = "탈출 실패.."; }
        else { resultText.text = "탈출 성공!!"; }

    }

    // 플레이어 HP바 구현
    private void HpSlider()
    {
        //지정해둔 변수 playerController를 게임오브젝트 Player의 스크립트 PlayerController로 지정
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        //이후 플레이어 hp 슬라이더의 value 값을 playerController의 현재 hp/ 최대hp로 설정
        playerHpSlider.value = playerController.currentHealth / playerController.maxHealth;

      
    }


    private void BestScores()
    {
        bestScorePause.text = "최고 점수 :  " + bestScore;
        bestTimePause.text = "생존 시간 :  " + bestMinTime.ToString("00") + ":" + bestSecTime.ToString("00");


        if (bestScoresObj.activeSelf == true && Input.anyKeyDown)
        {
            bestScoresObj.SetActive(false);



        }


    }
}
