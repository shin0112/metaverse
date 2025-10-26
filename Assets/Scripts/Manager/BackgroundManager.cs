using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [Header("Background References")]
    [SerializeField] private GameObject _home;

    private GameObject _current;

    private void Start()
    {
        EnterHome();
    }

    public void SetBackground(GameObject bg)
    {
        if (_current != null)
        {
            _current.SetActive(false);
        }

        bg.SetActive(true);
        _current = bg;
    }

    public void EnterHome() => SetBackground(_home);
}
