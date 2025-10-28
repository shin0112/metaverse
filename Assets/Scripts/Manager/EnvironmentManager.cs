using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    private static EnvironmentManager _instance;
    public static EnvironmentManager Instance => _instance;

    [Header("Environment References")]
    [SerializeField] private GameObject _home;
    [SerializeField] private GameObject _getFruit;

    [Header("Environment Prefab")]
    [SerializeField] private GameObject _getFruitPrefab;

    private GameObject _current;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        EnterHome();
    }

    public void SetEnvironment(GameObject bg)
    {
        if (_current != null)
        {
            Debug.Log($"기존 화면 비활성화: {_current.name}");
            _current.SetActive(false);
        }

        Debug.Log($"새로운 화면 활성화: {bg.name}");
        bg.SetActive(true);
        _current = bg;
    }

    public void EnterHome()
    {
        SetEnvironment(_home);
    }

    public GameObject EnterGetFruit()
    {
        Debug.Log($"[EnvironmentManager] _getFruit: {_getFruit}, activeSelf={_getFruit.activeSelf}");
        SetEnvironment(_getFruit);
        Debug.Log($"[EnvironmentManager] after SetActive: activeSelf={_getFruit.activeSelf}");

        var loop = FindObjectOfType<LoopGetFruitBg>(true);
        if (loop != null && !loop.gameObject.activeSelf)
        {
            loop.gameObject.SetActive(true);
            Debug.Log("[EnvironmentManager] LoopGetFruitBg 활성화됨");
        }

        return _getFruit;
    }

    public void MakeGetFruitEnvironment()
    {
        Transform existing = _getFruit.transform.Find("Background");

        if (existing != null)
        {
            Debug.Log("기존 배경 삭제");
            Destroy(existing.gameObject);
        }

        if (_getFruitPrefab != null)
        {
            GameObject newBg = Instantiate(_getFruitPrefab, _getFruit.transform);
            newBg.name = "Background";
            Debug.Log("새로운 배경 생성");
            var lp = FindObjectOfType<LoopGetFruitBg>();
            lp.SetupObstacles();
        }
    }
}
