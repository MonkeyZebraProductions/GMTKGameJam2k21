using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class ShootMin_Projectile : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;

    private float _projectileSpeed;
    private Vector3 _velocity;
    private LayerMask _solidLayerMask = 0;
    private LayerMask _delicateLayerMask = 0;
    private LayerMask _winObjectLayerMask = 0;
    private GameObject _currentRotationObject;
    private GameObject _currentWinObject;
    internal Quaternion ProjectileRotation; //Change this when you want to change the projectile rotation.

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        ProjectileRotation = transform.rotation;
        _projectileSpeed = ProjectilesManager.Instance.ProjectileSpeed;
        _solidLayerMask = ProjectilesManager.Instance.SolidLayerMask;
        _delicateLayerMask = ProjectilesManager.Instance.DelicateLayerMask;
        _winObjectLayerMask = ProjectilesManager.Instance.WinObjectLayerMask;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, ProjectileRotation, 0.5f);

        MoveProjectile();
        CollisionOverlap();
    }

    private void MoveProjectile()
    {
        //Works with any angle.
        _velocity = transform.right * Time.deltaTime * _projectileSpeed;

        transform.position += _velocity;
    }

    private void CollisionOverlap()
    {
        var solidOverlap = Physics2D.OverlapBox(_boxCollider2D.bounds.center, _boxCollider2D.size, transform.rotation.z, _solidLayerMask);
        InteractWithSolids(solidOverlap);

        var delicateOverlap = Physics2D.OverlapBox(_boxCollider2D.bounds.center, _boxCollider2D.size, transform.rotation.z, _delicateLayerMask);
        InteractWithDelicates(delicateOverlap);
        
        InteractWithRotationObjects(solidOverlap);
        
        var winObjectOverlap = Physics2D.OverlapBox(_boxCollider2D.bounds.center, _boxCollider2D.size, transform.rotation.z, _winObjectLayerMask);
        InteractWithWinObject(winObjectOverlap);
    }

    private void InteractWithSolids(Collider2D solidOverlap)
    {
        if (solidOverlap != null)
        {
            Debug.Log("Solid");
            Destroy(gameObject);
        }
    }

    private void InteractWithDelicates(Collider2D delicateOverlap)
    {
        if (delicateOverlap != null)
        {
            Debug.Log("Delicate");
            FindObjectOfType<AudioManager>().Play("BoxSmash");
            Destroy(delicateOverlap.gameObject);
            Destroy(gameObject);
        }
    }

    private void InteractWithRotationObjects(Collider2D rotationObjectOverlap)
    {
        if (rotationObjectOverlap != null)
        {
            if (_currentRotationObject != rotationObjectOverlap.gameObject)
            {
                _currentRotationObject = rotationObjectOverlap.gameObject;
                transform.position = rotationObjectOverlap.bounds.center;

                var zRotation = rotationObjectOverlap.gameObject.GetComponent<RotationObject>().RotationAngle;

                ProjectileRotation = Quaternion.Euler(new Vector3(ProjectileRotation.eulerAngles.x, ProjectileRotation.eulerAngles.y, zRotation));

                Debug.Log("RotationObject");
            }
        }
        else
        {
            _currentRotationObject = null;
        }
        Debug.Log(_currentRotationObject);
    }

    private void InteractWithWinObject(Collider2D winObjectOverlap)
    {
        if (winObjectOverlap != null)
        {
            if (_currentWinObject != winObjectOverlap.gameObject)
            {
                if (ProjectilesManager.Instance.WinBubble != null)
                {
                    _currentWinObject = winObjectOverlap.gameObject;
                    ProjectilesManager.Instance.WinBubble.SetActive(true);

                    ProjectilesManager.Instance.WinBubble.GetComponent<Image>().DOFade(1, 1);

                    ProjectilesManager.Instance.WinBubble.transform.position =
                        Camera.main.WorldToScreenPoint(new Vector3(winObjectOverlap.transform.position.x, winObjectOverlap.bounds.max.y + 1));

                    Debug.Log("WinObject");
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            _currentWinObject = null;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}