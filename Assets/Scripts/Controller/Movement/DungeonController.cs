using UnityEngine;

public class DungeonController : TopdownController
{
    private Camera _camera;

    protected override void Start()
    {
        base.Start();
        _camera = Camera.main;
    }

    protected override void GetRotateAction()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = _camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        if (lookDirection.magnitude < .3f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }
}
