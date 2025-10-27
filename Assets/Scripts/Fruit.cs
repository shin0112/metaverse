using UnityEngine;

public class Fruit : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private bool _collected = false;

    private ScoreManager _scoreManager;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _scoreManager = ScoreManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_collected) return;

        if (collision.CompareTag("Drone"))
        {
            _collected = true;

            _scoreManager.AddScore(1);

            SetAlpha(0f);
        }
    }

    public void SetAlpha(float alpha)
    {
        if (_renderer == null)
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        Color c = _renderer.color;
        c.a = alpha;
        _renderer.color = c;
    }

    public void Reactivate()
    {
        _collected = false;
        SetAlpha(1f);
    }
}
