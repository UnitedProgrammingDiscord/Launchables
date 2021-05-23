using UnityEngine;

public class GameLogic : MonoBehaviour {
  public GameObject player;
  public Camera cam;
  public int distance;

  private void Start() {
    Init();
  }


  void Init() {
    cam.transform.position = new Vector3(0, 0, -10);
    player.SetActive(false);
    distance = 0;
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.Space)) {
      StartGame();
    }
  }

  void StartGame() {
    Debug.Log("Game is started!");
  }
}
