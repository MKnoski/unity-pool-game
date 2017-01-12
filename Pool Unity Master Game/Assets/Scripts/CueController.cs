using UnityEngine;

namespace Assets.Scripts
{
  public class CueController : MonoBehaviour
  {
    private AudioSource _audioSource;
    private Rigidbody _rigidbody;

    private float _moveHorizontal;
    private float _moveVertical;
    private float _rotateHorizontal;
    private float _rotateVertical;
    private float _moveUpDown;

    public bool ShowAxes = true;
	public float Power = 500;

    private void Start()
    {
      _rigidbody = GetComponent<Rigidbody>();
      _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void FixedUpdate()
    {
      if (Input.GetAxis(InputAxes.HorizontalCue) != 0)
      {
        _moveHorizontal = Input.GetAxis(InputAxes.HorizontalCue);
      }
      if (Input.GetAxis(InputAxes.DirectionPadX) != 0)
      {
        _moveHorizontal = Input.GetAxis(InputAxes.DirectionPadX);
      }

      if (Input.GetAxis(InputAxes.RotateHorizontalCue) != 0)
      {
        _rotateHorizontal = Input.GetAxis(InputAxes.RotateHorizontalCue);
      }
      if (Input.GetAxis(InputAxes.RightStickX) != 0)
      {
        _rotateHorizontal = Input.GetAxis(InputAxes.RightStickX);
      }

      if (Input.GetAxis(InputAxes.VerticalCue) != 0)
      {
        _moveVertical = Input.GetAxis(InputAxes.VerticalCue);
      }
      if (Input.GetAxis(InputAxes.DirectionPadY) != 0)
      {
        _moveVertical = -Input.GetAxis(InputAxes.DirectionPadY);
      }

      if (Input.GetAxis(InputAxes.RotateVerticalCue) != 0)
      {
        _rotateVertical = Input.GetAxis(InputAxes.RotateVerticalCue);
      }
      if (Input.GetAxis(InputAxes.RightStickY) != 0)
      {
        _rotateVertical = Input.GetAxis(InputAxes.RightStickY);
      }

      _moveUpDown = Input.GetAxis(InputAxes.HeightCue);

      _rigidbody.transform.Translate(new Vector3(_moveHorizontal, _moveUpDown, _moveVertical) * (float)0.01,  Space.World);
      _rigidbody.transform.RotateAround(_rigidbody.position + transform.forward, Vector3.up, _rotateHorizontal);
      _rigidbody.transform.RotateAround(_rigidbody.position + transform.forward, transform.right, _rotateVertical);

      _moveHorizontal = _moveVertical = _rotateHorizontal = _rotateVertical = _moveUpDown = 0f;
    }

    private void OnDrawGizmos()
    {
      if (ShowAxes && _rigidbody != null)
      {
        Gizmos.DrawLine(_rigidbody.position + transform.forward + transform.right,
          _rigidbody.position + transform.forward - transform.right);
        Gizmos.DrawLine(_rigidbody.position + transform.forward + Vector3.up,
          _rigidbody.position + transform.forward - Vector3.up);
        Gizmos.DrawLine(_rigidbody.position, _rigidbody.position + _rigidbody.transform.forward * 10);
      }
    }

    private void LateUpdate()
    {
      if (Input.GetButtonDown(InputAxes.HitCue))
      {
        _rigidbody.AddForce(_rigidbody.transform.forward * Power);
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

    private void OnGUI() // działa tylko przy statycznej kamerze
    {
      if ((Event.current.keyCode == KeyCode.Space) && (Event.current.type == EventType.KeyDown))
      {
        _rigidbody.AddForce(new Vector3(0, 0, Power));
      }
    }
  }
}