using UnityEngine;

public class GameLogic : MonoBehaviour {
  public Player player;
  public Camera cam;
  public int distance;
  public static bool playing;
  public World world;


  private void Start() {
    Init();
  }


  void Init() {
    cam.transform.position = new Vector3(0, 0, -10); // Start position for the camera
    player.gameObject.SetActive(false); // Disable the player
    distance = 0; // Initialize th edistance to zero
    playing = false; // Set the status to "not playing"
    world.Init(); // Initialize the world
  }

  private void Update() {
    if (!playing && Input.GetKeyDown(KeyCode.Space)) { // If we are not playing and we press space we start
      StartGame();
    }
    if (!playing) return;

    // Calculate the camera position by lerping between current position and player position
    Vector3 cameraPos = Vector3.Lerp(cam.transform.position, player.transform.position, 3 * Time.deltaTime);
    cameraPos.z = -10; // Important!

    // Calculate the zoom of the camera according to the speed of the player
    float zoom = 5 + player.velocity.magnitude * .25f;
    if (zoom < 5.2f) zoom = 5; // Clamp it if too close to the minimum
    // Set the zoom, but use only 2 decimal places to smooth the transitions
    cam.orthographicSize = (int)(100 * Mathf.Lerp(cam.orthographicSize, zoom, Time.deltaTime)) / 100f;

    // Avoid the camera to go too low and show below the ground
    if (cameraPos.y < cam.orthographicSize - 5) cameraPos.y = cam.orthographicSize - 5;

    Vector2 playerPosRelativeToCamera = cam.WorldToViewportPoint(player.transform.position);
    if (playerPosRelativeToCamera.x < .5f && playerPosRelativeToCamera.y < .9f && playerPosRelativeToCamera.y >= 0) {
      // The player is before the center of the screen, do not change the X position of the camera
      cameraPos.x = cam.transform.position.x;
    }

    if (player.velocity.x < 2) {
      // The player is too slow on X axis, do not change the X position of the camera
      cameraPos.x = cam.transform.position.x;
    }

    // Set the calculated position of the camera
    cam.transform.position = cameraPos;

    // Generate (if needed) some other parts of the world
    world.Generate(player.transform.position.x + .5f * cam.orthographicSize);
  }

  void StartGame() {
    playing = true;
    player.Init();
  }


}

