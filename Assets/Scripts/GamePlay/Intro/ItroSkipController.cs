using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroSkipController : MonoBehaviour
{
    [Header("UI References")]
    public Image skipFillImage;  // Radial Filled Image
    public Text skipText;       // "Hold Any Key to Skip"

    [Header("Skip (Hold) Settings")]
    [SerializeField] private float holdTime = 3f;  // 홀드 필요 시간
    [SerializeField] private float drainSpeed = 2f;  // 뗐을 때 게이지 감소 배율

    [Header("Auto Proceed Settings")]
    // [TODO] 나중에 전체 연출 토탈 시간으로 변경
    private float autoProceedDelay = 15f; // 전체 연출 길이(초)
    private string nextScene = "MainMenu";

    private float currentTime;   // 홀드 게이지 타이머
    private float elapsed;       // 전체 경과 시간(입력과 무관)
    private bool skipped;       // 중복 로드 방지

    private void Reset() { skipFillImage = GetComponent<Image>(); }

    private void Update()
    {
        if (skipped) return;

        // ⬇︎ 전체 경과 시간은 입력과 무관하게 항상 증가
        elapsed += Time.unscaledDeltaTime;

        // ⬇︎ 홀드 게이지 로직
        bool holding = Input.anyKey;

        if (holding) 
            currentTime += Time.unscaledDeltaTime;
        else 
            currentTime -= Time.unscaledDeltaTime * drainSpeed;

        currentTime = Mathf.Clamp(currentTime, 0f, holdTime);

        // UI 반영
        if (skipFillImage) 
            skipFillImage.fillAmount = currentTime / holdTime;

        if (skipText)
        {
            if (currentTime > 0.1f)
            {
                float a = Mathf.PingPong(Time.unscaledTime * 2f, 1f);
                var c = skipText.color; c.a = a; skipText.color = c;
                skipText.enabled = true;
            }
            else skipText.enabled = false;
        }

        // 1) 홀드로 스킵
        if (currentTime >= holdTime && !skipped)
        {
            skipped = true;
            SceneManager.LoadScene(nextScene);
            return;
        }

        // 2) 연출 길이 경과로 자동 진행 (입력과 무관)
        if (elapsed >= autoProceedDelay && !skipped)
        {
            skipped = true;
            SceneManager.LoadScene(nextScene);
            return;
        }
    }
}
