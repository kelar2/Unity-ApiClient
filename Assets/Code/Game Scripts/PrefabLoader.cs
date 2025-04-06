using UnityEngine;
using UnityEngine.UIElements;

public class PrefabLoader : MonoBehaviour
{
    public GameObject canvas;
    public GameObject Prefab1;
    public GameObject Prefab2;
    public GameObject Prefab3;
    public GameObject Prefab4;


    public void LoadPrefab(Object2D object2D)
    {
        GameObject usedPrefab;
        Vector3 position;

        switch (object2D.prefabId)
        {
            case "Bush Prefab":
                usedPrefab = Prefab1;
                break;
            case "Flower Grid Prefab":
                usedPrefab = Prefab2;
                break;
            case "Flower Pot Prefab":
                usedPrefab = Prefab3;
                break;
            case "Flower Prefab":
                usedPrefab = Prefab4;
                break;
            default:
                throw new System.NotImplementedException("No prefab found for prefabId : " + object2D.prefabId);
        }
        position.x = object2D.positionX;
        position.y = object2D.positionY;
        position.z = 0;

        GameObject newObject = Instantiate(usedPrefab, position, Quaternion.identity);
        Draggable draggable = newObject.GetComponent<Draggable>();
        draggable.canvas = canvas;
        draggable.isDragging = false;
        Debug.Log("Item spawned");
    }
}