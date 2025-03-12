using UnityEngine;

public class PrefabLoader : MonoBehaviour
{
    GameObject Prefab1;
    GameObject Prefab2;
    GameObject Prefab3;
    GameObject Prefab4;
    GameObject UsedPrefab;

    public void LoadPrefab(GameObject Prefab)
    {
        switch ()
        {
            case 1:
                UsedPrefab = Prefab1;
                break;
            case 2:
                UsedPrefab = Prefab2;
                break;
            case 3:
                UsedPrefab = Prefab3;
                break;
            case 4:
                UsedPrefab = Prefab4;
                break;
        }

    }
    

}
