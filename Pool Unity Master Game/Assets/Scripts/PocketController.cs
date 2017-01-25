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
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
  public class PocketController : MonoBehaviour
  {
    public Text ScoreText;
    public Text WinningText;
    public AudioSource Applause;

    private short score = 0;
    private short NumberOfBalls = 15;
    private AudioSource audioSource;

    void Start()
    {
      this.audioSource = GetComponentInParent<AudioSource>();

      this.WinningText.enabled = false;
      this.DisplayScore();
    }

    void Update()
    {
    }

    private void OnCollisionEnter(Collision col)
    {
      if (!audioSource.isPlaying)
      {
        audioSource.Play();
      }

      if (col.gameObject.name == "Ball_0")
      {
        this.WinningText.text = "Game over - white ball potted";
        this.WinningText.enabled = true;
        return;
      }

      if (col.gameObject.transform.parent.name == "Balls")
      {
        this.score++;
        this.DisplayScore();
      }
    }

    private void DisplayScore()
    {
      this.ScoreText.text = string.Format("Score: {0}/{1}", this.score, this.NumberOfBalls);

      if (this.score == this.NumberOfBalls)
      {
        this.WinningText.enabled = true;
        this.Applause.Play();
      }
    }

    private void LateUpdate()
    {
      if (Input.GetKeyDown(KeyCode.Delete))
      {
        SceneManager.LoadScene(0);
      }
    }
  }
}
