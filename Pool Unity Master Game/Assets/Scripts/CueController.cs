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
using UnityEngine.UI;

namespace Assets.Scripts
{
  public class CueController : MonoBehaviour
  {
    private BallController _whiteBallController;
    private AudioSource _audioSource;
    private Rigidbody _rigidbody;

    private float _setPower;
    private float _rotateHorizontal;
    private float _rotateVertical;
    private float _power;

    public GameObject WhiteBall;
    public Slider PowerSlider;
    public bool ShowAxes = true;

    private void Start()
    {
      _rigidbody = GetComponent<Rigidbody>();
      _audioSource = GetComponent<AudioSource>();
      _whiteBallController = WhiteBall.GetComponent<BallController>();
    }

    private void FixedUpdate()
    {
      if (Input.GetAxis(InputAxes.DirectionPadX) != 0)
      {
        _rotateHorizontal = Input.GetAxis(InputAxes.DirectionPadX);
      }
      if (Input.GetAxis(InputAxes.DirectionPadY) != 0)
      {
        _rotateVertical = Input.GetAxis(InputAxes.DirectionPadY);
      }
      if (Input.GetAxis(InputAxes.HorizontalCue) != 0)
      {
        _rotateHorizontal = Input.GetAxis(InputAxes.HorizontalCue);
      }
      if (Input.GetAxis(InputAxes.VerticalCue) != 0)
      {
        _rotateVertical = Input.GetAxis(InputAxes.VerticalCue);
      }
      _setPower = Input.GetAxis(InputAxes.PowerCue);

      if ((_power > 0 && _setPower > 0) || (_power < 1 && _setPower < 0))
      {
        _rigidbody.transform.Translate(new Vector3(0, 0, _setPower) * 0.01f);
      }
      if ((_rigidbody.transform.eulerAngles.x > 3 && _rotateVertical < 0) ||
          (_rigidbody.transform.eulerAngles.x < 20 && _rotateVertical > 0))
      {
        _rigidbody.transform.RotateAround(WhiteBall.transform.position, transform.right, _rotateVertical);
      }
      _rigidbody.transform.RotateAround(WhiteBall.transform.position, Vector3.up, _rotateHorizontal);
      _power = (WhiteBall.transform.position - transform.position).magnitude - 1.5f;

      PowerSlider.value = _power;

      _rotateHorizontal = _rotateVertical = 0f;
    }

    private void OnDrawGizmos()
    {
      if (ShowAxes && _rigidbody != null)
      {
        Gizmos.DrawLine(WhiteBall.transform.position + transform.right,
          WhiteBall.transform.position - transform.right);
        Gizmos.DrawLine(WhiteBall.transform.position + Vector3.up,
          WhiteBall.transform.position - Vector3.up);
        Gizmos.DrawLine(_rigidbody.position, _rigidbody.position + _rigidbody.transform.forward * 10);
      }
    }

    private void LateUpdate()
    {
      if (Input.GetButtonDown(InputAxes.HitCue))
      {
        _rigidbody.AddForce(_rigidbody.transform.forward * (_power + 0.25f) * 1000);
      }

      if (Input.GetButtonDown(InputAxes.SetCue))
      {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.transform.rotation = Quaternion.AngleAxis(3.11f, Vector3.right);
        _rigidbody.transform.position = new Vector3(WhiteBall.transform.position.x, 0.4f, WhiteBall.transform.position.z - 1.5f);
      }

      if (_whiteBallController.MovingBalls > 0)
      {
        _rigidbody.transform.position = new Vector3(_rigidbody.position.x, 0.8f, _rigidbody.position.z);
      }
    }

    private void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.transform.parent.name == "Balls")
      {
        if (!_audioSource.isPlaying)
        {
          _audioSource.Play();
        }
      }

      _rigidbody.velocity = Vector3.zero;
    }

    public void OnCollisionStay(Collision collision)
    {
      if (collision.gameObject.name != "Ball_0")
      {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.transform.rotation = Quaternion.AngleAxis(3.11f, Vector3.right);
        _rigidbody.transform.position = new Vector3(WhiteBall.transform.position.x, 0.4f, WhiteBall.transform.position.z - 1.5f);
        _rigidbody.transform.RotateAround(WhiteBall.transform.position, transform.right, 10);
      }
    }

    private void OnGUI()
    {
      if ((Event.current.keyCode == KeyCode.Space) && (Event.current.type == EventType.KeyDown))
      {
        _rigidbody.AddForce(new Vector3(0, 0, 500));
      }
    }
  }
}