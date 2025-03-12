using UnityEngine;
using UnityEngine.UIElements;

public class PrefabLoader : MonoBehaviour
{
    public GameObject canvas;
    public GameObject Prefab1;
    public GameObject Prefab2;
    public GameObject Prefab3;
    public GameObject Prefab4;
    private GameObject UsedPrefab;
    //Draggable draggable;

    private Vector3 position;

    public void LoadPrefab(Object2D object2D)
    {
        switch (object2D.prefabId)
        {
            case "Bush Prefab":
                UsedPrefab = Prefab1;
                break;
            case "Flower Grid Prefab":
                UsedPrefab = Prefab2;
                break;
            case "Flower Pot Prefab":
                UsedPrefab = Prefab3;
                break;
            case "Flower Prefab":
                UsedPrefab = Prefab4;
                break;
        }
        position.x = object2D.positionX;
        position.y = object2D.positionY;
        position.z = 0;
        //draggable.canvas = canvas;
        //draggable.isDragging = false;
        GameObject newObject = Instantiate(UsedPrefab, position, Quaternion.identity);
        Debug.Log("Item spawned");
    }
}
