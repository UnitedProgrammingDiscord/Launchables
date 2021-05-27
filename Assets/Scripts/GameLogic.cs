using UnityEngine;

public class GameLogic : MonoBehaviour {
  public Player player;
  public Camera cam;
  public int distance;
  public static bool playing;

  private void Start() {
    Init();
  }


  void Init() {
    cam.transform.position = new Vector3(0, 0, -10); // Start position for the camera
    player.gameObject.SetActive(false); // Disable the player
    distance = 0; // Initialize th edistance to zero
    playing = false; // Set the status to "not playing"
  }

  private void Update() {
    if (!playing && Input.GetKeyDown(KeyCode.Space)) { // If we are not playing and we press space we start
      StartGame();
    }
  }

  void StartGame() {
    Debug.Log("Game is started!");
    playing = true;
    player.Init();
  }
}

