using UnityEngine;

public class MiniGamePortal : MonoBehaviour
{
    private MiniGameManager _miniGameManager;

    [SerializeField] private int gameType;

    private void Start()
    {
        _miniGameManager = MiniGameManager.Instance;
    }

    private void OnTrigggerEnter2D()
    {

    }
}
