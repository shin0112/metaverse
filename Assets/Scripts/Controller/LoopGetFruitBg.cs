using System.Collections.Generic;
using UnityEngine;

public class LoopGetFruitBg : MonoBehaviour
{
    [Header("Obstacles")]
    [SerializeField] private int _obstacleCount = 0;
    protected Vector3 obstacleLastPosition = Vector3.right * 25f;

    [Header("Settings")]
    [SerializeField] private float _scrollSpeed = 5f;   // 이동 속도 (모든 레이어 동일)
    [SerializeField]
    private Dictionary<string, int> _bgCounts = new()
    {
        { "Background", 8 },
        { "Middle", 4 },
        { "Ground", 4 }
    };
    [SerializeField] private float _spawnOffsetY = 0f;  // 레이어별 높이 보정값

    private Camera _mainCam;
    private float _screenLeft;

    private void Start()
    {
        _mainCam = Camera.main;
        _screenLeft = _mainCam.transform.position.x - 15f;
    }

    private void OnEnable()
    {
        SetupObstacles();

    }

    public void SetupObstacles()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>(true);
        if (obstacles.Length == 0) return;

        // 🔹 lastPosition 초기화
        obstacleLastPosition = Vector3.right * 25f;
        _obstacleCount = obstacles.Length;

        foreach (var obstacle in obstacles)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, _obstacleCount);
        }

        Debug.Log("장애물 재배치 완료");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered: " + collision.name);
        float widthOfBgObject = 0f;

        if (collision.CompareTag("Background") || collision.CompareTag("Middle"))
        {
            widthOfBgObject = ((BoxCollider2D)collision).size.x * 0.998f;
        }
        else if (collision.CompareTag("Ground"))
        {
            widthOfBgObject = ((CompositeCollider2D)collision).bounds.size.x * 0.998f;
        }
        else
        {
            Obstacle obstacle = collision.GetComponent<Obstacle>();
            if (obstacle)
            {
                obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, _obstacleCount);
            }
            return;
        }

        Vector3 pos = collision.transform.position;
        pos.x += widthOfBgObject * _bgCounts[collision.tag];

        collision.transform.position = pos;
        return;
    }
}
