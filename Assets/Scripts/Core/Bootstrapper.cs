using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public sealed class Bootstrapper : MonoBehaviour
{
    [Header("필수 시스템 프리팹들 (DontDestroyOnLoad)")]
    [SerializeField] private GameObject[] systemPrefabs;

    [Header("다음 씬 이름")]
    [SerializeField] private string nextScene = "Intro";

    private void Awake()
    {
        foreach (var prefab in systemPrefabs)
        {
            if (!prefab) 
                continue;

            // 프리팹 루트에 붙은 첫 번째 MonoBehaviour 타입으로 체크
            var mb = prefab.GetComponent<MonoBehaviour>();
            if (mb == null)
            {
                // (루트에 스크립트가 없으면 이름 체크로 폴백)
                if (GameObject.Find(prefab.name) == null && GameObject.Find(prefab.name + "(Clone)") == null)
                {
                    Instantiate(prefab);
                }
                continue;
            }

            var type = mb.GetType();

            // 비활성까지 포함해서 씬에 하나라도 있나 확인
            if (Object.FindFirstObjectByType(type, FindObjectsInactive.Include) == null)
            {
                Instantiate(prefab); // 각 시스템 프리팹은 내부에서 DontDestroyOnLoad + 싱글턴 보장
            }
        }

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    private IEnumerator Start()
    {
        yield return null; // 필요 없으면 삭제해도 됨
        SceneManager.LoadScene(nextScene);
    }
}
