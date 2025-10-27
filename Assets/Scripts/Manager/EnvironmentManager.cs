using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    private static EnvironmentManager _instance;
    public static EnvironmentManager Instance => _instance;

    [Header("Environment References")]
    [SerializeField] private GameObject _home;
    [SerializeField] private GameObject _getFruit;

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
        return _getFruit;
    }
}
