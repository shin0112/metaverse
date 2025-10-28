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
            Debug.Log($"���� ȭ�� ��Ȱ��ȭ: {_current.name}");
            _current.SetActive(false);
        }

        Debug.Log($"���ο� ȭ�� Ȱ��ȭ: {bg.name}");
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
            Debug.Log("[EnvironmentManager] LoopGetFruitBg Ȱ��ȭ��");
        }

        return _getFruit;
    }

    public void MakeGetFruitEnvironment()
    {
        Transform existing = _getFruit.transform.Find("Background");

        if (existing != null)
        {
            Debug.Log("���� ��� ����");
            Destroy(existing.gameObject);
        }

        if (_getFruitPrefab != null)
        {
            GameObject newBg = Instantiate(_getFruitPrefab, _getFruit.transform);
            newBg.name = "Background";
            Debug.Log("���ο� ��� ����");
            var lp = FindObjectOfType<LoopGetFruitBg>();
            lp.SetupObstacles();
        }
    }
}
