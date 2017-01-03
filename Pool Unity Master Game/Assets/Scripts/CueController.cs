using UnityEngine;

namespace Assets.Scripts
{
    public class CueController : MonoBehaviour
    {
        private float _moveHorizontal;
        private float _moveVertical;
        private Rigidbody _rigidbody;
        private float _rotateHorizontal;
        private float _rotateVertical;
        private float _moveUpDown;
        private AudioSource audioSource;

        public bool ShowAxes = true;

        private void Start()
        {
            this._rigidbody = GetComponent<Rigidbody>();
            this.audioSource = GetComponent<AudioSource>();
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
            this._moveHorizontal = Input.GetAxis(InputAxes.RightStickX);
            this._moveHorizontal = Input.GetAxis(InputAxes.HorizontalCue);
            this._rotateHorizontal = Input.GetAxis(InputAxes.LeftStickX);
            this._rotateHorizontal = Input.GetAxis(InputAxes.RotateHorizontalCue);

            this._moveVertical = -Input.GetAxis(InputAxes.RightStickY);
            this._moveVertical = Input.GetAxis(InputAxes.VerticalCue);
            this._rotateVertical = Input.GetAxis(InputAxes.LeftStickY);
            this._rotateVertical = Input.GetAxis(InputAxes.RotateVerticalCue);

            this._moveUpDown = Input.GetAxis(InputAxes.HeightCue);

            this._rigidbody.transform.Translate(new Vector3(this._moveHorizontal, this._moveUpDown, this._moveVertical)*(float) 0.01);
            this._rigidbody.transform.RotateAround(this._rigidbody.position + transform.forward, Vector3.up, this._rotateHorizontal);
            this._rigidbody.transform.RotateAround(this._rigidbody.position + transform.forward, this.transform.right, this._rotateVertical);
            //     _rigidbody.AddForce(new Vector3(_moveHorizontal, 0, _moveVertical) * 10);
        }

        private void OnDrawGizmos()
        {
            if (ShowAxes)
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
            if (Input.GetKeyDown(KeyCode.R))
            {
                this._rigidbody.AddForce(this._rigidbody.transform.forward*500);
            }

            if (Input.GetButtonDown(InputAxes.HitCue))
            {
                this._rigidbody.AddForce(this._rigidbody.transform.forward*500);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
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