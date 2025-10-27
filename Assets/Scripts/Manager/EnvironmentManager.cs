using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    private static EnvironmentManager _instance;
    public static EnvironmentManager Instance => _instance;

    [Header("Environment References")]
    [SerializeField] private GameObject _home;
    [SerializeField] private GameObject _getFruit;

    private GameObject _current;
    private GameObject _currentMiniGameInstancce;

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
            _current.SetActive(false);
        }

        bg.SetActive(true);
        _current = bg;
    }

    public void EnterHome()
    {
        if (_currentMiniGameInstancce != null)
        {
            Destroy(_currentMiniGameInstancce);
            _currentMiniGameInstancce = null;
        }

        SetEnvironment(_home);
    }

    public GameObject EnterGetFruit()
    {
        SetEnvironment(_getFruit);

        _currentMiniGameInstancce = Instantiate(_getFruit, transform);
        return _currentMiniGameInstancce;
    }
}
