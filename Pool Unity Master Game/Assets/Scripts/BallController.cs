using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private AudioSource audioSource;

    // Use this for initialization
    private void Start()
    {
        this._rigidbody = GetComponent<Rigidbody>();
        this.audioSource = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.transform.parent.name == "Balls")
        {
            this.audioSource.Play();
        }
    }

    private void LateUpdate()
    {
        if ((this._rigidbody.velocity.magnitude < 0.3) && (this._rigidbody.velocity.magnitude > 0)) { }
        {
            if (this._rigidbody.velocity.magnitude > 0.03)
            {
                this._rigidbody.velocity *= (float)0.9;
            }
            else
            {
                this._rigidbody.velocity = Vector3.zero;
                this._rigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
}