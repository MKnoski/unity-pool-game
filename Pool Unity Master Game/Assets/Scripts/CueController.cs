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

        private void Start()
        {
            this._rigidbody = GetComponent<Rigidbody>();
      this._audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //  Debug.Log("Space key was pressed.");
            //}
        }

        private void FixedUpdate()
        {
      if (Input.GetAxis(InputAxes.HorizontalCue) != 0)
      {
        this._moveHorizontal = Input.GetAxis(InputAxes.HorizontalCue);
      }
      if (Input.GetAxis(InputAxes.RightStickX) != 0)
      {
        this._moveHorizontal = Input.GetAxis(InputAxes.RightStickX);
      }

      if (Input.GetAxis(InputAxes.RotateHorizontalCue) != 0)
      {
        this._rotateHorizontal = Input.GetAxis(InputAxes.RotateHorizontalCue);
      }
      if (Input.GetAxis(InputAxes.DirectionPadX) != 0)
      {
        this._rotateHorizontal = Input.GetAxis(InputAxes.DirectionPadX);
      }

      if (Input.GetAxis(InputAxes.VerticalCue) != 0)
      {
        this._moveVertical = Input.GetAxis(InputAxes.VerticalCue);
      }
      if (Input.GetAxis(InputAxes.RightStickY) != 0)
      {
        this._moveVertical = -Input.GetAxis(InputAxes.RightStickY);
      }

      if (Input.GetAxis(InputAxes.RotateVerticalCue) != 0)
      {
        this._rotateVertical = Input.GetAxis(InputAxes.RotateVerticalCue);
      }
      if (Input.GetAxis(InputAxes.DirectionPadY) != 0)
      {
        this._rotateVertical = Input.GetAxis(InputAxes.DirectionPadY);
      }

            this._moveUpDown = Input.GetAxis(InputAxes.HeightCue);

            this._rigidbody.transform.Translate(new Vector3(this._moveHorizontal, this._moveUpDown, this._moveVertical) * (float)0.01);
            this._rigidbody.transform.RotateAround(this._rigidbody.position + transform.forward, Vector3.up, this._rotateHorizontal);
            this._rigidbody.transform.RotateAround(this._rigidbody.position + transform.forward, this.transform.right, this._rotateVertical);
      //_rigidbody.AddForce(new Vector3(_moveHorizontal, 0, _moveVertical) * 10);

      _moveHorizontal = _moveVertical = _rotateHorizontal = _rotateVertical = _moveUpDown = 0f;
        }

        private void OnDrawGizmos()
        {
            if (ShowAxes && this._rigidbody != null)
            {
                Gizmos.DrawLine(this._rigidbody.position + transform.forward + this.transform.right,
          this._rigidbody.position + transform.forward - this.transform.right);
                Gizmos.DrawLine(this._rigidbody.position + transform.forward + Vector3.up,
          this._rigidbody.position + transform.forward - Vector3.up);
                Gizmos.DrawLine(this._rigidbody.position, this._rigidbody.position + this._rigidbody.transform.forward * 10);
            }
        }

        private void LateUpdate()
        {
            //if (Input.GetKeyDown(KeyCode.R))
            //{
            //    this._rigidbody.AddForce(this._rigidbody.transform.forward*500);
            //}

            if (Input.GetButtonDown(InputAxes.HitCue))
            {
                this._rigidbody.AddForce(this._rigidbody.transform.forward * 500);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.transform.parent.name == "Balls")
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }

            this._rigidbody.velocity = Vector3.zero;
        }

        private void OnGUI() // działa tylko przy statycznej kamerze
        {
            if ((Event.current.keyCode == KeyCode.Space) && (Event.current.type == EventType.KeyDown))
                this._rigidbody.AddForce(new Vector3(0, 0, 500));
        }
    }
}