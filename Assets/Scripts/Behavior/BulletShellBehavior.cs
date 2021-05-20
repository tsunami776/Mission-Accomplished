using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShellBehavior : MonoBehaviour
{
    [SerializeField] private AudioSource[] shellFallFX;

    private void OnEnable()
    {
        GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Random.Range(0f, Config.PHYSICS_SHELL_BOUNCE_FORCE), Random.Range(0f, Config.PHYSICS_SHELL_BOUNCE_FORCE), 0f), ForceMode.Impulse);
        StartCoroutine(ShellExist());
    }

    private void OnCollisionEnter(Collision collision)
    {
        shellFallFX[Random.Range(0, shellFallFX.Length)].Play();
    }

    IEnumerator ShellExist()
    {
        yield return new WaitForSecondsRealtime(Config.TIME_SHELL_LAST);
        Destroy(gameObject);
    }
}
