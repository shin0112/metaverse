using UnityEngine;

public class MiniGamePortal : MonoBehaviour
{
    private MiniGameManager _miniGameManager;

    [SerializeField] private int gameType;

    private void Start()
    {
        _miniGameManager = MiniGameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"enter {gameType} protal");
            _miniGameManager.SetEnterZone(true, gameType);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"exit {gameType} protal");
            _miniGameManager.SetEnterZone(false, gameType);
        }
    }
}
