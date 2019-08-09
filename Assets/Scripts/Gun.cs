using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    public Transform GunTip;
    public Transform ShellEjectionPoint;
    public Rigidbody Shell;

    [SerializeField] private GunType gunType;
    [SerializeField] private float rpm;

    private float secondsBetweenShot;
    private float nextPossibleShootTime;
    private LineRenderer tracer;

    void Start()
    {
        secondsBetweenShot = 60 / rpm;
        if (GetComponent<LineRenderer>())
        {
            tracer = GetComponent<LineRenderer>();
        }
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            Ray ray = new Ray(GunTip.position, GunTip.forward);
            RaycastHit hit;

            float shotDistance = 20f;

            if (Physics.Raycast(ray, out hit, shotDistance))
            {
                shotDistance = hit.distance;
            }
            nextPossibleShootTime = Time.time + secondsBetweenShot;

            GetComponent<AudioSource>().Play();

            if (tracer)
            {
                StartCoroutine(RenderTracer(shotDistance * ray.direction));
            }


            Rigidbody newShell = ObjectPooling.Instance.SpawnFromPool("Shell", ShellEjectionPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            newShell.AddForce(ShellEjectionPoint.forward * Random.Range(150f, 200f) + GunTip.forward * Random.Range(-10f, 10f));
        }
    }

    public void ShootContinuous()
    {
        if (gunType == GunType.Auto)
        {
            Shoot();
        }
    }

    private bool CanShoot()
    {
        bool canShoot = true;

        if (Time.time < nextPossibleShootTime)
        {
            canShoot = false;
        }

        return canShoot;
    }

    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        tracer.enabled = true;
        tracer.SetPosition(0, GunTip.position);
        tracer.SetPosition(1, GunTip.position + hitPoint);
        yield return null;
        tracer.enabled = false;
    }
}

public enum GunType
{
    Semi,
    Burst,
    Auto
}

