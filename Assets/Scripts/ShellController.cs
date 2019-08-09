using UnityEngine;
using System.Collections;

public class ShellController : MonoBehaviour
{

    [SerializeField] private float deathTime, fadeTime;

    void OnEnable()
    {
        Invoke("EnableShade", fadeTime);
        Invoke("HideShell", deathTime);
    }

    void EnableShade()
    {
        GetComponent<Animator>().enabled = true;
    }

    void HideShell()
    {
        GetComponent<Animator>().enabled = false;
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
