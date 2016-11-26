using UnityEngine;

namespace Assets.Scripts
{
  public class FollowinigCameraController : MonoBehaviour
  {
    private const float PositionYMax = 50.0f;
    private const float PositionYMin = -50.0f;

    private const float FieldOfViewMax = 120.0f;
    private const float FieldOfViewMin = 20.0f;

    private float _positionX;
    private float _positionY;

    private float _fieldOfView = 60.0f;

    private Transform _cameraTransform;
    private Vector3 _offset;

    public Transform LookAtObjectTransform;

    public Camera Camera;

    // Use this for initialization
    private void Start()
    {
      _cameraTransform = transform;

      _offset = LookAtObjectTransform.position - _cameraTransform.position;
    }

    // Update is called once per frame
    private void Update()
    {
      _positionX -= Input.GetAxis("Horizontal_Camera");

      _positionY += Input.GetAxis("Vertical_Camera");
      _positionY = Mathf.Clamp(_positionY, PositionYMin, PositionYMax);

      _fieldOfView += Input.GetAxis("Zoom");
      _fieldOfView = Mathf.Clamp(_fieldOfView, FieldOfViewMin, FieldOfViewMax);
    }

    // LateUpdate is called after all Update functions have been called.
    private void LateUpdate()
    {
      var rotationQuaternion = Quaternion.Euler(_positionY, _positionX, 0);
      var rotationQuaternionMultiplyOffSet = rotationQuaternion * _offset;
      _cameraTransform.position = LookAtObjectTransform.position - rotationQuaternionMultiplyOffSet;

      _cameraTransform.LookAt(LookAtObjectTransform.position);

      Camera.fieldOfView = _fieldOfView;
    }
  }
}