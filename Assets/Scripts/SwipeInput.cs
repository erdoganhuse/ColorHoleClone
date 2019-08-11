using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    [SerializeField] private float _horizontalInputMultiplier;
    [SerializeField] private float _verticalInputMultiplier;
    
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    private Vector2 _previousMousePosition;
    
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _previousMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Horizontal = (Input.mousePosition.x - _previousMousePosition.x) * _horizontalInputMultiplier;
            Vertical = (Input.mousePosition.y - _previousMousePosition.y) * _verticalInputMultiplier;

            _previousMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Horizontal = 0f;
            Vertical = 0f;
        }
    }
}