//////////////////////////////////////////////////////////////////////////
//// GRAFIKA 3D I SYSTEMY MULTIMEDIALNE 1 - LABORATORIUM
//// "Gra w bilard" 
////
//// Autorzy:
//// Maksymilian Knoski, Piotr Danowski, Adam Szady, Konrad Puchalski
//// 
//// Prowadzący:
//// dr inż. Jan Nikodem
//////////////////////////////////////////////////////////////////////////

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

        private void Start()
        {
            _cameraTransform = transform;

            _offset = LookAtObjectTransform.position - _cameraTransform.position;
        }

        private void Update()
        {
            _positionX -= Input.GetAxis(InputAxes.HorizontalCamera);
            _positionX -= Input.GetAxis(InputAxes.LeftStickX);

            _positionY += Input.GetAxis(InputAxes.VerticalCamera);
            _positionY -= Input.GetAxis(InputAxes.LeftStickY);
            _positionY = Mathf.Clamp(_positionY, PositionYMin, PositionYMax);

            _fieldOfView += Input.GetAxis(InputAxes.KeyboardZoom);
            _fieldOfView += Input.GetAxis(InputAxes.JoystickZoom);
            _fieldOfView = Mathf.Clamp(_fieldOfView, FieldOfViewMin, FieldOfViewMax);
        }

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