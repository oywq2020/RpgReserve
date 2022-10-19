using Cysharp.Threading.Tasks;
using UnityEngine;

interface IPlayerLookAt
{
    //Provide injection interface 
    void LookAt(Vector3 lookPoint);
}

/// <summary>
/// assemble all player physically behaviours 
/// </summary>
public class PlayerController : MonoBehaviour, IPlayerLookAt
{
    public float moveSpeed = 8f;
    public float jumpSpeed = 5f;
    public float gravity = 20f;
    public Crosshairs mCrosshairs;

    private CharacterController _characterController;
    private Vector3 _moveDirection = Vector3.zero;
    private Camera _viewCamera;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _viewCamera = Camera.main;
    }

    private void Update()
    {
        Movement();
        MouseInput();
    }

    // move and jump
    void Movement()
    {
        if (_characterController.isGrounded)
        {
            //move
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            _moveDirection = new Vector3(h, 0, v);
            //correct moveDirection
            Quaternion angleAxis = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up);
            _moveDirection = angleAxis * _moveDirection;
            //jump
            if (Input.GetButton("Jump"))
                _moveDirection.y = jumpSpeed;
        }

        _moveDirection.y -= gravity * Time.deltaTime;
        _characterController.Move(_moveDirection * moveSpeed * Time.deltaTime);
    }

    void MouseInput()
    {
        Ray ray = _viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * 2);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            LookAt(point);
            mCrosshairs.transform.position = point;
            mCrosshairs.DetectTargets(ray);
        }
    }

    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }
}