using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameHandler.Start");
        //Score.ResetHighScore();
        Score.Start();
    }

}
