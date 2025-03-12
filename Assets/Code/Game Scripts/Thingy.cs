using UnityEngine;

public class Thingy : MonoBehaviour
{
    public Application application;

    public async void LoadPrefabsInWorld()
    {
        await application.ReadObject2Ds();
    }
}
