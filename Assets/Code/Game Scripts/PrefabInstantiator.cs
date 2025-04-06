using UnityEngine;

public class PrefabInstantiator : MonoBehaviour
{
    public Application application;
    public Vector3 spawnposition;
    public GameObject prefab;
    public GameObject canvas;

    public void CreateInstanceOfPrefab()
    {
        //application = new Application();
        GameObject instanceOfPrefab = Instantiate(prefab, spawnposition, Quaternion.identity);
        Draggable draggable = instanceOfPrefab.GetComponent<Draggable>();
        draggable.canvas = canvas;
        Object2D createdObject = new Object2D();
        createdObject.prefabId = prefab.name;
        createdObject.positionX = spawnposition.x;
        createdObject.positionY = spawnposition.y;
        createdObject.scaleX = prefab.transform.localScale.x;
        createdObject.scaleY = prefab.transform.localScale.y;
        createdObject.rotationZ = prefab.transform.rotation.eulerAngles.z;
        createdObject.sortingLayer = 0;
        application.object2D = createdObject;
    }

    //public void SetPrefab(GameObject SetPrefab)
    //{
    //    GameObject instanceOfPrefab = Instantiate(SetPrefab, spawnposition, Quaternion.identity);
    //    Draggable draggable = instanceOfPrefab.GetComponent<Draggable>();
    //    draggable.canvas = canvas;
    //}
}
