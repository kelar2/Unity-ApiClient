using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField UserEmailInputLogin;
    [SerializeField] TMP_InputField UserPasswordInputLogin;
    [SerializeField] TMP_InputField UserEmailInputRegister;
    [SerializeField] TMP_InputField UserPasswordInputRegister;

    private string stringEmailInputLogin;
    private string stringPasswordInputLogin;
    private string stringEmailInputRegister;
    private string stringPasswordInputRegister;

    [SerializeField] TMP_InputField Environment2DName;
    [SerializeField] TMP_InputField Environment2DMaxHeight;
    [SerializeField] TMP_InputField Environment2DMaxLength;

    private string stringEnvironment2DName;
    private int intEnvironment2DMaxHeight;
    private int intEnvironment2DMaxLength;

    public string ReturnLoginPassword()
    {
        return stringPasswordInputLogin;
    }

    public string ReturnLoginEmail()
    {
        return stringEmailInputLogin;
    }


    public string ReturnRegisterPassword()
    {
        return stringPasswordInputRegister;
    }

    public string ReturnRegisterEmail()
    {
        return stringEmailInputRegister;
    }

    public void SetCredentialsLogin()
    {
        stringEmailInputLogin = UserEmailInputLogin.text;
        stringPasswordInputLogin = UserPasswordInputLogin.text;
    }

    public void SetCredentialsRegister()
    {
        stringEmailInputRegister = UserEmailInputRegister.text;
        stringPasswordInputRegister = UserPasswordInputRegister.text;
    }

    public string ReturnEnvironment2DName()
    {
        return stringEnvironment2DName;
    }

    public int ReturnEnvironment2DMaxHeight()
    {
        return intEnvironment2DMaxHeight;
    }

    public int ReturnEnvironment2DMaxLength()
    {
        return intEnvironment2DMaxLength;
    }

    public void SetEnvironment2DCredentials()
    {
        stringEnvironment2DName = Environment2DName.text;
        intEnvironment2DMaxHeight = int.Parse(Environment2DMaxHeight.text);
        intEnvironment2DMaxLength = int.Parse(Environment2DMaxLength.text);
    }
}
