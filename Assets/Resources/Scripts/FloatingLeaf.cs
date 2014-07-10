﻿using UnityEngine;
using System.Collections;

public class FloatingLeaf : MonoBehaviour {

  public float waterHeight = 0.0f;
  public float transitionWidth = 0.2f;
  public float waterForce = 0.1f;
  public float waterTorque = 0.002f;
  public float waterDrag = 5.0f;
  public float waterAngularDrag = 10.0f;

  public Vector3 waterCurrentVelocity;
  public float waterCurrentForce = 0.1f;

  private float air_drag_;
  private float air_angular_drag_;

  void Start() {
    air_drag_ = rigidbody.drag;
    air_angular_drag_ = rigidbody.angularDrag;
  }

  void FixedUpdate() {
    float distanceFromSurface = transform.position.y - waterHeight;
    if (distanceFromSurface >= 0) {
      rigidbody.drag = air_drag_;
      rigidbody.angularDrag = air_angular_drag_;
      return;
    }

    rigidbody.drag = waterDrag;
    rigidbody.drag = waterAngularDrag;

    float transition = Mathf.Clamp(-distanceFromSurface / transitionWidth, 0, 1);
    rigidbody.AddForce(new Vector3(0, waterForce * transition, 0));

    if (Vector3.Dot(transform.up, Vector3.up) >= 0) {
      Vector3 torque_vector = Vector3.Cross(transform.up, Vector3.up);
      rigidbody.AddTorque((1 - transition) * waterTorque * torque_vector);
    }
    else {
      Vector3 torque_vector = Vector3.Cross(-transform.up, Vector3.up);
      rigidbody.AddTorque((1 - transition) * waterTorque * torque_vector);
    }

    // Current.
    Vector3 delta_current = waterCurrentVelocity - rigidbody.velocity;
    delta_current.y = 0;
    rigidbody.AddForce(waterCurrentForce * delta_current);
  }
}