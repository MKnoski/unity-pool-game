using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
  public class CueController : MonoBehaviour
  {
    private Rigidbody _rigidbody;

    private float _moveHorizontal;
    private float _moveVertical;
    private float _rotateHorizontal;
    private float _rotateVertical;

    void Start()
    {
      _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      //if (Input.GetKeyDown(KeyCode.Space))
      //{
      //  Debug.Log("Space key was pressed.");
      //}
    }

    void FixedUpdate()
    {
      _moveHorizontal = Input.GetAxis(InputAxes.RightStickX);
      _rotateHorizontal = Input.GetAxis(InputAxes.LeftStickX);
      _rotateHorizontal = Input.GetAxis(InputAxes.HorizontalCue);

      _moveVertical = -Input.GetAxis(InputAxes.RightStickY);
      _rotateVertical = Input.GetAxis(InputAxes.LeftStickY);
      _rotateVertical = Input.GetAxis(InputAxes.VerticalCue);
      _rigidbody.transform.Translate(new Vector3(_moveHorizontal, 0, _moveVertical)*(float)0.01);
      _rigidbody.transform.RotateAround(_rigidbody.position+transform.forward,Vector3.up, _rotateHorizontal );
      _rigidbody.transform.RotateAround(_rigidbody.position+transform.forward, Vector3.right, _rotateVertical);
            //     _rigidbody.AddForce(new Vector3(_moveHorizontal, 0, _moveVertical) * 10);
        }

      void OnDrawGizmos()
      {
            Gizmos.DrawLine(_rigidbody.position + transform.forward + transform.right, _rigidbody.position + transform.forward - transform.right);
            Gizmos.DrawLine(_rigidbody.position + transform.forward + transform.up, _rigidbody.position + transform.forward - transform.up);
            Gizmos.DrawLine(_rigidbody.position, _rigidbody.position + _rigidbody.transform.forward *10);

        }
        void LateUpdate()
    {
      if (Input.GetKeyDown(KeyCode.R))
      {
        _rigidbody.AddForce(_rigidbody.transform.forward * 500);
      }

      if (Input.GetButtonDown(InputAxes.HitCue))
      {
        _rigidbody.AddForce(_rigidbody.transform.forward * 500);
      }

    }

    void OnCollisionEnter(Collision collision)
    {
      Debug.Log(" collected: " + collision.collider.name);
      _rigidbody.velocity = Vector3.zero;
    }

        void OnGUI() // działa tylko przy statycznej kamerze
    {
      if (Event.current.keyCode == KeyCode.Space && Event.current.type == EventType.KeyDown)
      {
        _rigidbody.AddForce(new Vector3(0, 0, 500));
      }
    }
  }
}