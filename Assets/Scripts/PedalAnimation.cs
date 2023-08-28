using UnityEngine;

public class PedalAnimation : MonoBehaviour
{
    public Sprite[] imagesGas;
    public Sprite[] imagesBrake;
    public GameObject _gas;
    public GameObject _brake;

    private int currentGasIndex = 0;
    private int currentBrakeIndex = 0;
    private SpriteRenderer gasSpriteRenderer;
    private SpriteRenderer brakeSpriteRenderer;
    public DriveCar driveCarObject;
    private bool wasGasPressed = false;
    private bool wasBrakePressed = false;

    private void Start()
    {
        gasSpriteRenderer = _gas.GetComponent<SpriteRenderer>();
        brakeSpriteRenderer = _brake.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        HandlePedal(driveCarObject.GetGasPedalState(), imagesGas, ref currentGasIndex, gasSpriteRenderer, ref wasGasPressed);
        HandlePedal(driveCarObject.GetBrakePedalState(), imagesBrake, ref currentBrakeIndex, brakeSpriteRenderer, ref wasBrakePressed);
    }

    private void HandlePedal(bool isPressed, Sprite[] images, ref int currentIndex, SpriteRenderer spriteRenderer, ref bool wasPressed)
    {
        if (isPressed)
        {
            if (!wasPressed)
            {
                currentIndex = (currentIndex + 1) % images.Length;
                spriteRenderer.sprite = images[currentIndex];
                wasPressed = true;
            }
        }
        else
        {
            currentIndex = 0;
            spriteRenderer.sprite = images[currentIndex];
            wasPressed = false;
        }
    }
}
