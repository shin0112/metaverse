using System.Collections.Generic;
using UnityEngine;

public class LoopGetFruitBg : MonoBehaviour
{
    [Header("Layers")]
    [SerializeField] private GameObject _backLayerPrefab;
    [SerializeField] private GameObject _middleLayerPrefab;
    [SerializeField] private GameObject _groundLayerPrefab;

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
        ResetLoopScene();
    }

    public void ResetLoopScene()
    {
        Debug.Log("Loop scene reset");

        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>(true);
        obstacleLastPosition = obstacles[0].transform.position;
        _obstacleCount = obstacles.Length;

        foreach (var obstacle in obstacles)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, _obstacleCount);
        }

        foreach (var tag in _bgCounts.Keys)
        {
            GameObject[] bgObjs = GameObject.FindGameObjectsWithTag(tag);
            float offset = 0f;
            foreach (var obj in bgObjs)
            {
                obj.transform.position = new Vector3(offset, obj.transform.position.y, obj.transform.position.z);
                offset += ((Renderer)obj.GetComponent<Renderer>()).bounds.size.x;
            }
        }
    }

    private void Update()
    {
        if (_mainCam == null) return;

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
