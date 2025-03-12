using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Application : MonoBehaviour
{
    public GameObject RegisterLogin;
    public GameObject WorldsScreen;
    public GameObject CreateWorldScreen;
    public GameObject PlayableWorld;

    public GameObject CreateWorldButton;

    public User user;
    public Environment2D environment2D;
    public Object2D object2D;

    [SerializeField] TextMeshProUGUI World1;
    [SerializeField] TextMeshProUGUI World2;
    [SerializeField] TextMeshProUGUI World3;
    [SerializeField] TextMeshProUGUI World4;
    [SerializeField] TextMeshProUGUI World5;

    [SerializeField] TextMeshProUGUI RegisterErrorMessage;
    [SerializeField] TextMeshProUGUI LoginErrorMessage;
    [SerializeField] TextMeshProUGUI WorldCreateErrorMessage;
    [SerializeField] TextMeshProUGUI MaxWorldsErrorMessage;

    public UserApiClient userApiClient;
    public Environment2DApiClient enviroment2DApiClient;
    public Object2DApiClient object2DApiClient;

    public InputHandler inputHandler;

    public PrefabInstantiator prefabInstantiator;

    public List<Environment2D> environmentList;
    public int buttonNumber;
    public string loadedWorldId;

    #region Login

    public async void Register()
    {
        inputHandler.SetCredentialsRegister();
        user.email = inputHandler.ReturnRegisterEmail();
        user.password = inputHandler.ReturnRegisterPassword();

        if (user.password.Length >= 10)
        {
            IWebRequestReponse webRequestResponse = await userApiClient.Register(user);

            switch (webRequestResponse)
            {
                case WebRequestData<string> dataResponse:
                    Debug.Log("Register succes!");
                    // TODO: Handle succes scenario;
                    RegisterErrorMessage.gameObject.SetActive(false);
                    break;
                case WebRequestError errorResponse:
                    string errorMessage = errorResponse.ErrorMessage;
                    Debug.Log("Register error: " + errorMessage);
                    // TODO: Handle error scenario. Show the errormessage to the user.
                    RegisterErrorMessage.text = "Email Is already in Use";
                    RegisterErrorMessage.gameObject.SetActive(true);
                    break;
                default:
                    throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
            }
        }
        else
        {
            RegisterErrorMessage.text = "Password must be at least 10 characters long";
            RegisterErrorMessage.gameObject.SetActive(true);
        }
    }

    public async void Login()
    {
        inputHandler.SetCredentialsLogin();
        user.email = inputHandler.ReturnLoginEmail();
        user.password = inputHandler.ReturnLoginPassword();

        IWebRequestReponse webRequestResponse = await userApiClient.Login(user);

        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                Debug.Log("Login succes!");
                // TODO: Todo handle succes scenario.
                LoginErrorMessage.gameObject.SetActive(false);
                RegisterLogin.SetActive(false);
                WorldsScreen.SetActive(true);
                ReadEnvironment2Ds();
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Login error: " + errorMessage);
                // TODO: Handle error scenario. Show the errormessage to the user.
                LoginErrorMessage.text = "Invalid Email or Password";
                LoginErrorMessage.gameObject.SetActive(true);
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    #endregion

    #region Environment

    public async void ReadEnvironment2Ds()
    {
        IWebRequestReponse webRequestResponse = await enviroment2DApiClient.ReadEnvironment2Ds();
        
        environmentList = new List<Environment2D>();

        switch (webRequestResponse)
        {
            case WebRequestData<List<Environment2D>> dataResponse:
                List<Environment2D> environment2Ds = dataResponse.Data;
                Debug.Log("List of environment2Ds: ");
                foreach(var environment2D in environment2Ds)
                {
                    Debug.Log(environment2D.id);
                    environmentList.Add(environment2D);
                }

                if(environmentList.Count >= 5)
                {
                    CreateWorldButton.SetActive(false);
                    MaxWorldsErrorMessage.gameObject.SetActive(true);
                }
                else 
                {
                    CreateWorldButton.SetActive(true);
                    MaxWorldsErrorMessage.gameObject.SetActive(false);
                }

                // TODO: Handle succes scenario.
                World1.text = "No world";
                World2.text = "No world";
                World3.text = "No world";
                World4.text = "No world";
                World5.text = "No world";
                int i = 1;
                foreach(Environment2D e in environmentList)
                {
                    switch (i)
                    {
                        case 1:
                            World1.text = e.name;
                            break;
                        case 2:
                            World2.text = e.name;
                            break;
                        case 3:
                            World3.text = e.name;
                            break;
                        case 4:
                            World4.text = e.name;
                            break;
                        case 5:
                            World5.text = e.name;
                            break;
                    }

                    i++;
                }
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Read environment2Ds error: " + errorMessage);
                // TODO: Handle error scenario. Show the errormessage to the user.
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }


    //loads the play screen and readies the environment with objects

    public void LoadEnvironment(int buttonIndex)
    {
        buttonNumber = buttonIndex;
        Environment2D e = environmentList[buttonNumber];
        loadedWorldId = e.id;
        if (loadedWorldId != null)
        {
            WorldsScreen.SetActive(false);
            PlayableWorld.SetActive(true);
            Debug.Log("Loaded world with number " + buttonNumber + " Id: " + loadedWorldId);
            ReadObject2Ds();
        }
    }




    public async void CreateEnvironment2D()
    {
        inputHandler.SetEnvironment2DCredentials();
        environment2D.name = inputHandler.ReturnEnvironment2DName();
        environment2D.maxHeight = inputHandler.ReturnEnvironment2DMaxHeight();
        environment2D.maxLength = inputHandler.ReturnEnvironment2DMaxLength();
        bool isNameDifferent = true;
        foreach (Environment2D e in environmentList)
        {
            if (e.name == environment2D.name)
            {
                isNameDifferent = false;
            }
        }

        if (environment2D.name.Length >= 1 && environment2D.name.Length <= 25 && isNameDifferent)
        {
            IWebRequestReponse webRequestResponse = await enviroment2DApiClient.CreateEnvironment(environment2D);

            switch (webRequestResponse)
            {
                case WebRequestData<Environment2D> dataResponse:
                    //environment2D.id = dataResponse.Data.id;
                    Debug.Log("Create environment2D succes!");
                    CreateWorldScreen.SetActive(false);
                    WorldsScreen.SetActive(true);
                    WorldCreateErrorMessage.gameObject.SetActive(false);
                    ReadEnvironment2Ds();
                    // TODO: Handle succes scenario.

                    break;
                case WebRequestError errorResponse:
                    string errorMessage = errorResponse.ErrorMessage;
                    Debug.Log("Create environment2D error: " + errorMessage);
                    // TODO: Handle error scenario. Show the errormessage to the user.
                    break;
                default:
                    throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
            }
        }
        else
        {
            if(environment2D.name.Length < 1)
            {
                WorldCreateErrorMessage.text = "Name is too short, minimum is 1";
                WorldCreateErrorMessage.gameObject.SetActive(true);
            }
            else if(environment2D.name.Length > 25)
            {
                WorldCreateErrorMessage.text = "Name is too long, maximum is 25";
                WorldCreateErrorMessage.gameObject.SetActive(true);
            }
            else if(!isNameDifferent)
            {
                WorldCreateErrorMessage.text = "Name is already in use, try a different one";
                WorldCreateErrorMessage.gameObject.SetActive(true);
            }
        }
    }

    public async void DeleteEnvironment2D(int buttonindex)
    {
        buttonNumber = buttonindex;
        Environment2D e = environmentList[buttonNumber];
        environment2D.id = e.id;
        IWebRequestReponse webRequestResponse = await enviroment2DApiClient.DeleteEnvironment(environment2D.id);

        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                string responseData = dataResponse.Data;
                // TODO: Handle succes scenario.
                ReadEnvironment2Ds();
                
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Delete environment error: " + errorMessage);
                // TODO: Handle error scenario. Show the errormessage to the user.
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    #endregion Environment

    #region Object2D

    public async void ReadObject2Ds()
    {
        IWebRequestReponse webRequestResponse = await object2DApiClient.ReadObject2Ds(object2D.environmentId);

        switch (webRequestResponse)
        {
            case WebRequestData<List<Object2D>> dataResponse:
                List<Object2D> object2Ds = dataResponse.Data;
                Debug.Log("List of object2Ds: " + object2Ds);
                foreach(Object2D object2D in object2Ds)
                {
                    Debug.Log(object2D.id);
                    //prefabInstantiator.
                }
                
                // TODO: Succes scenario. Show the enviroments in the UI
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Read object2Ds error: " + errorMessage);
                // TODO: Error scenario. Show the errormessage to the user.
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    public async void CreateObject2D(Object2D CreatedObject)
    {
        CreatedObject.environmentId = loadedWorldId;
        IWebRequestReponse webRequestResponse = await object2DApiClient.CreateObject2D(CreatedObject);

        switch (webRequestResponse)
        {
            case WebRequestData<Object2D> dataResponse:
                CreatedObject.id = dataResponse.Data.id;
                // TODO: Handle succes scenario.

                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Create Object2D error: " + errorMessage);
                // TODO: Handle error scenario. Show the errormessage to the user.
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    public async void UpdateObject2D()
    {
        IWebRequestReponse webRequestResponse = await object2DApiClient.UpdateObject2D(object2D);

        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                string responseData = dataResponse.Data;
                // TODO: Handle succes scenario.
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Update object2D error: " + errorMessage);
                // TODO: Handle error scenario. Show the errormessage to the user.
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    #endregion

}
