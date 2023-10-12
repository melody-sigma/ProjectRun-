using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour {
   public AudioClip deathClip; // 사망시 재생할 오디오 클립
   public float jumpForce = 300f; // 점프 힘

   private int jumpCount = 0; // 누적 점프 횟수
   private bool isGrounded = false; // 바닥에 닿았는지 나타냄
   private bool isDead = false; // 사망 상태

   private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
   private Animator animator; // 사용할 애니메이터 컴포넌트
   private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

    public float maxHealth = 100f;
    public float currentHealth;

    public int score = 0;

    //지속 데미지
    public float ContinuousDamage = 3f;

    //슬라이딩 
    private bool isSliding = false;
    private float slideTimer = 0;
    private float slideDuration = 0.5f;
    private CapsuleCollider2D capsuleCollider;
    private CircleCollider2D circleCollider;

    private void Start() {
        // 초기화
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        currentHealth = maxHealth;

        capsuleCollider = GetComponent<CapsuleCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update() {

        

        // 사망시 입력받지 않음
        if (isDead)
       {
           return;
       }

       // 점프
       if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < 2 && !isSliding)
       {
           jumpCount++;
           playerRigidbody.velocity = Vector2.zero;
           playerRigidbody.AddForce(new Vector2 (0, jumpForce));
           playerAudio.Play();
       }
        
        else if (Input.GetKeyUp(KeyCode.UpArrow) && playerRigidbody.velocity.y >0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }
        
        animator.SetBool("Grounded", isGrounded);


        // 슬라이딩
        if (isGrounded && Input.GetKeyDown(KeyCode.DownArrow))
        {
            isSliding = true;
            slideTimer = 0.0f;
            animator.SetBool("isSliding", true);
        }

        // 슬라이딩 중이면 슬라이딩 지속 시간을 체크
        if (isSliding)
        {
            slideTimer += Time.deltaTime;

            // 슬라이딩 지속 시간이 지나면 슬라이딩 종료
            if (slideTimer >= slideDuration)
            {
                isSliding = false;
                animator.SetBool("isSliding", false); // 슬라이딩 애니메이션 종료
            }

            capsuleCollider.enabled = false;
            circleCollider.enabled = true;
        } else {
            // 슬라이딩 중이 아니면 캡슐 콜라이더를 활성화하고 서클 콜라이더를 비활성화
            capsuleCollider.enabled = true;
            circleCollider.enabled = false;
        }

        // 사망 처리
        if (currentHealth > 0)
        {
            currentHealth -= ContinuousDamage * Time.deltaTime;
        }

        if (currentHealth <=0)
        {
            Die();
        }
    }

    private void Die()
    // 사망 처리
    {
        animator.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerRigidbody.velocity = Vector2.zero;
        isDead = true;
    }
    
    // 회복
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Dead태그 상대와 충돌 시 사망
        if (other.tag == "Dead" && !isDead)
        {
            Die();
        }

        //과일
        if (other.CompareTag("Fruit"))
        {
            Fruit fruit = other.GetComponent<Fruit>();
            if (fruit != null)
            {
                Heal(fruit.heal);
                Destroy(other.gameObject);
            }
        }
    }

    public void GetScore(int amount)
    {
        score += amount;
        //삭제
        Debug.Log("Score: " + score);
        //삭제
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // 바닥에 닿았음을 감지하는 처리
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        // 바닥에서 벗어났음을 감지하는 처리
        isGrounded = false;
    }
}