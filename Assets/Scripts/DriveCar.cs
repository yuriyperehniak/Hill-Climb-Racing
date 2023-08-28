using UnityEngine;

public class DriveCar : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _frontTireRB;
    [SerializeField] private Rigidbody2D _backTireRB;
    [SerializeField] private Rigidbody2D _carRB;
    [SerializeField] private float _speed = 150f;
    [SerializeField] private float _rotationSpeed = 300f;
    [SerializeField] private GameObject _gasButton;
    [SerializeField] private GameObject _brakeButton;
    [SerializeField] private bool _isGasPressed = false;
    [SerializeField] private bool _isBrakePressed = false;
    private int _currentDirection = 0; // -1 для руху назад, 0 для стоянки, 1 для руху вперед

    private void Update()
    {
        _isGasPressed = false;
        _isBrakePressed = false;

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane));
                Collider2D gasButtonCollider = _gasButton.GetComponent<Collider2D>();
                Collider2D brakeButtonCollider = _brakeButton.GetComponent<Collider2D>();

                if (gasButtonCollider.OverlapPoint(touchPos))
                {
                    _isGasPressed = true;
                }
                else if (brakeButtonCollider.OverlapPoint(touchPos))
                {
                    _isBrakePressed = true;
                }
            }
        }

        if (_isGasPressed && !_isBrakePressed)
        {
            _currentDirection = 1;
        }
        else if (!_isGasPressed && _isBrakePressed)
        {
            _currentDirection = -1;
        }
        else
        {
            _currentDirection = 0;
        }

        float moveInput = _currentDirection;
        _frontTireRB.AddTorque(-moveInput * _speed * Time.deltaTime);
        _backTireRB.AddTorque(-moveInput * _speed * Time.deltaTime);
        _carRB.AddTorque(-moveInput * _rotationSpeed * Time.deltaTime);
    }

    public bool GetGasPedalState()
    {
        return _isGasPressed;
    }

    public bool GetBrakePedalState()
    {
        return _isBrakePressed;
    }
}
