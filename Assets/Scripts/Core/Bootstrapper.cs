using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public sealed class Bootstrapper : MonoBehaviour
{
    [Header("필수 시스템 프리팹들 (DontDestroyOnLoad)")]
    [SerializeField] GameObject[] systemPrefabs;

    [Header("다음 씬 이름")]
    [SerializeField] string nextScene = "Intro";

    void Awake()
    {
        // 중복 방지: Boot씬이 재실행돼도 시스템 2개 생기지 않게
        foreach (var p in systemPrefabs)
        {
            var name = p.name;
            if (GameObject.Find($"{name}(Clone)") == null && GameObject.Find(name) == null)
                Instantiate(p);
        }

        Application.targetFrameRate = 60;   // 필요시
        QualitySettings.vSyncCount = 0;     // 필요시(PC)
    }

    IEnumerator Start()
    {
        // 옵션/세이브 로드 등 초기화 자리
        yield return null;

        // 바로 로드 (로딩 화면 필요하면 아래 비동기 버전 사용)
        SceneManager.LoadScene(nextScene);
    }
}
