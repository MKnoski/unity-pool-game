using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PocketController : MonoBehaviour
{
    public Text ScoreText;
    public Text WinningText;

    private short score = 0;
    private short NumberOfBalls = 15;

    // Use this for initialization
    void Start()
    {
        this.WinningText.enabled = false;
        this.DisplayScore();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision col)
    {
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
