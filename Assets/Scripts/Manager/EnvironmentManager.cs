using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    private static EnvironmentManager _instance;
    public static EnvironmentManager Instance => _instance;

    [Header("Background References")]
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
            _current.SetActive(false);
        }

        bg.SetActive(true);
        _current = bg;
    }

    public void EnterHome() => SetEnvironment(_home);
    public void EnterGetFruit() => SetEnvironment(_getFruit);
}
