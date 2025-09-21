using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroSkipController : MonoBehaviour
{
    public Image skipFillImage; // UI Image (Fill)
    public Text skipText; // UI Text for 'Hold 'AnyKey' to Skip'
    [SerializeField] private float holdTime = 3f; // 몇 초 동안 눌러야 스킵?
    [SerializeField] private float drainSpeed = 2f; // 뗐을 때 감소 속도
    [SerializeField] private string nextScene = "MainMenu";

    private float currentTime;

    void Update()
    {
        bool holding = Input.anyKey; // 아무 키든 누르고 있으면 true

        if (holding)
        {
            currentTime += Time.unscaledDeltaTime; // 누른 동안 게이지 증가

        }
        else
        {
            currentTime -= Time.unscaledDeltaTime * drainSpeed; // 뗐을 땐 서서히 감소
            //skipText.enabled = false;
        }

        // 텍스트 표시/숨김 (연출)
        if (skipText)
            skipText.enabled = currentTime > 0.1f;

        currentTime = Mathf.Clamp(currentTime, 0f, holdTime);

        // UI에 반영
        if (skipFillImage)
            skipFillImage.fillAmount = currentTime / holdTime;

        // 다 찼으면 씬 전환
        if (currentTime >= holdTime)
            SceneManager.LoadScene(nextScene);
    }
}
