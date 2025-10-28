﻿using System.Collections.Generic;
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

    private Vector3[] _initBackgroundPos;
    private Vector3[] _initMiddlePos;
    private Vector3[] _initGroundPos;

    private void Start()
    {
        _mainCam = Camera.main;
        _screenLeft = _mainCam.transform.position.x - 15f;
    }

    private void OnEnable()
    {
        SaveInitialPositions();
        SetupObstacles();
    }

    private void SaveInitialPositions()
    {
        GameObject[] bgList = GameObject.FindGameObjectsWithTag("Background");
        GameObject[] midList = GameObject.FindGameObjectsWithTag("Middle");
        GameObject[] groundList = GameObject.FindGameObjectsWithTag("Ground");

        _initBackgroundPos = new Vector3[bgList.Length];
        _initMiddlePos = new Vector3[midList.Length];
        _initGroundPos = new Vector3[groundList.Length];

        for (int i = 0; i < bgList.Length; i++)
            _initBackgroundPos[i] = bgList[i].transform.position;

        for (int i = 0; i < midList.Length; i++)
            _initMiddlePos[i] = midList[i].transform.position;

        for (int i = 0; i < groundList.Length; i++)
            _initGroundPos[i] = groundList[i].transform.position;

        Debug.Log("배경 초기 위치 저장 완료");
    }

    public void ResetLoopScene()
    {
        Debug.Log("Loop Scene Reset 시작");

        GameObject[] bgList = GameObject.FindGameObjectsWithTag("Background");
        GameObject[] midList = GameObject.FindGameObjectsWithTag("Middle");
        GameObject[] groundList = GameObject.FindGameObjectsWithTag("Ground");

        for (int i = 0; i < bgList.Length && i < _initBackgroundPos.Length; i++)
            bgList[i].transform.position = _initBackgroundPos[i];

        for (int i = 0; i < midList.Length && i < _initMiddlePos.Length; i++)
            midList[i].transform.position = _initMiddlePos[i];

        for (int i = 0; i < groundList.Length && i < _initGroundPos.Length; i++)
            groundList[i].transform.position = _initGroundPos[i];

        // 장애물은 lastPosition만 초기화
        SetupObstacles();

        Debug.Log("Loop Scene Reset 완료");
    }

    private void SetupObstacles()
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
