using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    private void Awake()
    {
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);

            GM = this;
        }
        else if (GM != this)
        {

            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
