using UnityEngine;

namespace Assets.Scripts
{
  public class CueController : MonoBehaviour
  {
    private Rigidbody _rigidbody;

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
      float moveHorizontal = Input.GetAxis("Horizontal_Cue");
      float moveVertical = Input.GetAxis("Vertical_Cue");

      _rigidbody.AddForce(new Vector3(moveHorizontal, 0, moveVertical) * 10);
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