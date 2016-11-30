using UnityEngine;

namespace Assets.Scripts
{
  public class CueController : MonoBehaviour
  {
    private Rigidbody _rigidbody;

    private float _moveHorizontal;
    private float _moveVertical;

    void Start()
    {
      _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      //if (Input.GetKeyDown(KeyCode.Space))
      //{
      //    Debug.Log("Space key was pressed.");
      //}
    }

    void FixedUpdate()
    {
      _moveHorizontal = Input.GetAxis(InputAxes.HorizontalCue);
      _moveHorizontal = Input.GetAxis(InputAxes.RightStickX);

      _moveVertical = Input.GetAxis(InputAxes.VerticalCue);
      _moveVertical = -Input.GetAxis(InputAxes.RightStickY);

      _rigidbody.AddForce(new Vector3(_moveHorizontal, 0, _moveVertical) * 10);
    }

    void OnCollisionEnter(Collision collision)
    {
      Debug.Log(" collected: " + collision.collider.name);
      _rigidbody.velocity = Vector3.zero;
    }

    public void OnGUI()
    {
      if (Event.current.keyCode == KeyCode.Space && Event.current.type == EventType.KeyDown)
      {
        _rigidbody.AddForce(new Vector3(0, 0, 500));
      }
    }
  }
}