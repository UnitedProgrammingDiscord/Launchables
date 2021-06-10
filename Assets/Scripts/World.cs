using UnityEngine;

public class World : MonoBehaviour {
  public BackgroundPrefab[] GroundPrefabs;
  public BackgroundPrefab[] GrassPrefabs;
  public BackgroundPrefab[] TreesPrefabs;
  public BackgroundPrefab[] MountainPrefabs;

  BackgroundPart lastGround;
  BackgroundPart lastGrass;
  BackgroundPart lastTree;
  BackgroundPart lastMountain;

  public void Init() {
    // Remove all previous objects
    foreach (Transform child in transform) {
      Destroy(child.gameObject);
    }

    // Initialize the first part of the World
    lastGround = PlacePrefab(GroundPrefabs, null, 0);
    lastGrass = PlacePrefab(GrassPrefabs, null, 0);
    lastTree = PlacePrefab(TreesPrefabs, null, 0);
    lastTree = PlacePrefab(TreesPrefabs, lastTree, 0);
    lastTree = PlacePrefab(TreesPrefabs, lastTree, 0);
    lastTree = PlacePrefab(TreesPrefabs, lastTree, 0);
    lastTree = PlacePrefab(TreesPrefabs, lastTree, 0); // We have only one single prefab for trees right now
    lastMountain = PlacePrefab(MountainPrefabs, null, 0); // First mountain
    lastMountain = PlacePrefab(MountainPrefabs, lastMountain, 1); // Second mountain
    lastMountain = PlacePrefab(MountainPrefabs, lastMountain, 2); // Third mountain
    lastMountain = PlacePrefab(MountainPrefabs, lastMountain, 3); // Fourth mountain
    lastMountain = PlacePrefab(MountainPrefabs, lastMountain, 4); // Fifth mountain
  }

  BackgroundPart PlacePrefab(BackgroundPrefab[] possiblePrefabs, BackgroundPart lastItem, int index = -1) {
    if (index == -1)
      index = Random.Range(0, possiblePrefabs.Length); // Get a random one
    BackgroundPrefab prefab = possiblePrefabs[index];
    GameObject prefabInstance = Instantiate(prefab.Prefab, transform); // Create the GameObject as child of the current object
    float XPosition = prefab.Margin;
    if (lastItem != null)
      XPosition += lastItem.HorizontalEnd; // If we have a previous item use its end position
    else
      XPosition -= 20; // If we are the first move the position to the left to avoid visible gaps
    prefabInstance.transform.position = new Vector3(XPosition, prefab.VerticalPos, 0);
    BackgroundPart result = new BackgroundPart { Item = prefabInstance, HorizontalEnd = XPosition + prefab.Width - prefab.Margin };
    return result;
  }

  public void Generate(float horizontalPosition) {
    lastGround = GenerateLayer(horizontalPosition, GroundPrefabs, lastGround);
    lastGrass = GenerateLayer(horizontalPosition, GrassPrefabs, lastGrass);
    lastTree = GenerateLayer(horizontalPosition, TreesPrefabs, lastTree);
    lastMountain = GenerateLayer(horizontalPosition, MountainPrefabs, lastMountain);
  }

  BackgroundPart GenerateLayer(float horizontalPosition, BackgroundPrefab[] possiblePrefabs, BackgroundPart lastItem) {
    // Is the position over the x coordinate of the item?
    if (horizontalPosition > lastItem.Item.transform.position.x) {
      return PlacePrefab(possiblePrefabs, lastItem); // Generate a new item and return it
    }
    return lastItem; // Return the previous item, no generation is needed
  }
}

[System.Serializable] // To have the object visible in the inspector
public class BackgroundPrefab { // Used to define the possible background parts
  public string Name; // What is the name
  public GameObject Prefab; // Reference to the prefab itself
  public float Width; // What is the width of the gameobject?
  public float VerticalPos; // What is the vertical position to place it?
  public float Margin; // Do we have to consider a margin to overlap two items?
}

public class BackgroundPart { // Used to keep track of the parts that are placed in the scene
  public GameObject Item; // Placed object reference
  public float HorizontalEnd; // X coordinate where the item ends
}

