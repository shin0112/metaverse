using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float highPosY = -9f;
    public float lowPosY = -13f;

    public float holeSizeMin = 3f;
    public float holeSizeMax = 5f;

    [SerializeField] private Transform _topObject;
    [SerializeField] private Transform _bottomObject;
    [SerializeField] private Fruit _fruit;

    public float widthPadding = 15f;

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        // ���� ũ��
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2f;

        // ���� ��ġ
        _topObject.localPosition = new Vector3(0, halfHoleSize);
        _bottomObject.localPosition = new Vector3(0, -halfHoleSize);

        // ���� ��ġ
        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0f, 0f);
        placePosition.y = Random.Range(lowPosY, highPosY);
        transform.position = placePosition;

        // ���� �߾ӿ� ��ġ
        if (_fruit != null)
        {
            _fruit.transform.localPosition = Vector3.zero;
            _fruit.Reactivate();
        }

        return placePosition;
    }
}
