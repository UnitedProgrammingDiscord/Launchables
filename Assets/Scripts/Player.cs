using UnityEngine;

public class Player : MonoBehaviour {
  public Vector3 velocity;
  [Range(0, 360)] public float Angle;
  public float Speed;
  public float Gravity = 1;
  [Range(0, 1)] public float EnergyLoss = .9f;

  public void Init() {
    gameObject.SetActive(true); // Enable the player object
    transform.position = new Vector3(-6.9f, -3.1f, 0); // Reset the position
    velocity = Quaternion.Euler(0, 0, Angle) * Vector2.right * Speed; // Calculate the velocity vector based on angle and speed
  }

  private void Update() {
    transform.position += velocity * Time.deltaTime; // Update the position based on the velocity
    velocity.y -= Gravity * Time.deltaTime; // Gravity applies only to Y axis of the velocity

    if (transform.position.y <= -4.5f) { // -4.5f is our ground, are we below the ground?
      velocity.y *= -1; // Yes, change the sign of the velocity to bounce
      transform.position.Set(transform.position.x, -4.45f, transform.position.z); // Set a minimum vertical value to avoid errors
      velocity *= EnergyLoss; // Reduce the energy (on both axes)
    }
  }




















































  private void OnDrawGizmos() { // This is to show the Arrow gizmo over the player that rpresent the velocity vector. It is not included in the tutorial video
    float thic = .01f;
    if (!Application.isPlaying) {
      Vector2 vel = Quaternion.Euler(0, 0, Angle) * Vector2.right * Speed;
      if ((Vector2)velocity != vel) velocity = (Vector3)vel;
    }
    float z = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
    Vector3 forward = transform.position + velocity * .5f;

    Gizmos.color = Color.yellow;
    Gizmos.DrawLine(transform.position, forward);
    Gizmos.DrawLine(transform.position + Vector3.up * thic, forward + Vector3.up * thic);
    Gizmos.DrawLine(transform.position + Vector3.left * thic, forward + Vector3.left * thic);
    Gizmos.DrawLine(transform.position + (Vector3.left + Vector3.up) * thic, forward + (Vector3.left + Vector3.up) * thic);

    z += 180;
    float dist = .3f;
    Vector3 al = forward + Vector3.right * Mathf.Cos((z + 30) * Mathf.Deg2Rad) * dist + Vector3.up * Mathf.Sin((z + 30) * Mathf.Deg2Rad) * dist;
    Vector3 ar = forward + Vector3.right * Mathf.Cos((z - 30) * Mathf.Deg2Rad) * dist + Vector3.up * Mathf.Sin((z - 30) * Mathf.Deg2Rad) * dist;
    Gizmos.DrawLine(forward, al);
    Gizmos.DrawLine(forward, ar);

  }
}
