﻿using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class ShootMin_Projectile : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;
    
    private float _projectileSpeed;
    private Vector3 _velocity;
    private LayerMask _solidLayerMask = 0;
    private LayerMask _delicateLayerMask = 0;
    private LayerMask _rotationObjectLayerMask = 0;
    
    internal Quaternion ProjectileRotation; //Change this when you want to change the projectile rotation.

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        ProjectileRotation = transform.rotation;
        _projectileSpeed = ProjectilesManager.Instance.ProjectileSpeed;
        _solidLayerMask = ProjectilesManager.Instance.SolidLayerMask;
        _delicateLayerMask = ProjectilesManager.Instance.DelicateLayerMask;
        _rotationObjectLayerMask = ProjectilesManager.Instance.RotationObjectLayerMask;
    }

    private void Update()
    {
        transform.rotation = ProjectileRotation;
        
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
        
        var rotationObjectOverlap = Physics2D.OverlapBox(_boxCollider2D.bounds.center, _boxCollider2D.size, transform.rotation.z, _rotationObjectLayerMask);
        InteractWithRotationObjects(rotationObjectOverlap);
    }
    
    private void InteractWithSolids(Collider2D solidOverlap)
    {
        if (solidOverlap != null)
        {
            Destroy(gameObject);
        }
    }

    private void InteractWithDelicates(Collider2D delicateOverlap)
    {
        if (delicateOverlap != null)
        {
            Destroy(delicateOverlap.gameObject);
        }
    }
    
    private void InteractWithRotationObjects(Collider2D rotationObjectOverlap)
    {
        if (rotationObjectOverlap != null)
        {
            var zRotation = rotationObjectOverlap.gameObject.GetComponent<RotationObject>().RotationAngle;

            ProjectileRotation = Quaternion.Euler(new Vector3(ProjectileRotation.eulerAngles.x, ProjectileRotation.eulerAngles.y, zRotation));
        }
    }

    //Basically when it gets out of the screen, as there is no other way in the current project that it will go invisible.
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}