using System.Collections.Generic;
using UnityEngine;

public class LoopGetFruitBg : MonoBehaviour
{
    [Header("Layers")]
    [SerializeField] private GameObject _backLayerPrefab;
    [SerializeField] private GameObject _middleLayerPrefab;
    [SerializeField] private GameObject _groundLayerPrefab;

    [Header("Settings")]
    [SerializeField] private float _scrollSpeed = 5f;   // �̵� �ӵ� (��� ���̾� ����)
    [SerializeField]
    private Dictionary<string, int> _bgCounts = new()
    {
        { "Background", 8 },
        { "Middle", 4 },
        { "Ground", 4 }
    };
    [SerializeField] private float _spawnOffsetY = 0f;  // ���̾ ���� ������

    private Camera _mainCam;
    private float _screenLeft;

    private void Start()
    {
        _mainCam = Camera.main;
        _screenLeft = _mainCam.transform.position.x - 15f;
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

        Vector3 pos = collision.transform.position;
        pos.x += widthOfBgObject * _bgCounts[collision.tag];

        collision.transform.position = pos;
        return;
    }
}
