using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A 2D representation of a spring
namespace Snowdrama.Spring
{
    public class Spring3D
    {
        SpringCollection springCollection;
        Vector3 value;
        Vector3 target;
        Vector3 velocity;

        int xID;
        int yID;
        int zID;
        public Spring3D(SpringConfiguration config, Vector3 initialValue = new Vector3())
        {
            springCollection = new SpringCollection(3);
            xID = springCollection.Add(initialValue.x, config);
            yID = springCollection.Add(initialValue.y, config);
            zID = springCollection.Add(initialValue.z, config);
        }

        public void Update(float deltaTime)
        {
            springCollection.Update(deltaTime);
            value.x = springCollection.GetValue(xID);
            value.y = springCollection.GetValue(yID);
            value.z = springCollection.GetValue(zID);

            target.x = springCollection.GetTarget(xID);
            target.y = springCollection.GetTarget(yID);
            target.z = springCollection.GetTarget(zID);

            velocity.x = springCollection.GetVelocity(xID);
            velocity.y = springCollection.GetVelocity(yID);
            velocity.z = springCollection.GetVelocity(zID);
        }

        public void SetTarget(Vector3 target)
        {
            this.target = target;
            springCollection.SetTarget(xID, target.x);
            springCollection.SetTarget(yID, target.y);
            springCollection.SetTarget(zID, target.z);
        }

        public void SetValue(Vector3 value)
        {
            this.value = value;
            springCollection.SetValue(xID, value.x);
            springCollection.SetValue(yID, value.y);
            springCollection.SetValue(zID, value.z);
        }

        public void SetVelocity(Vector3 velocity)
        {
            this.velocity = velocity;
            springCollection.SetVelocity(xID, velocity.x);
            springCollection.SetVelocity(yID, velocity.y);
            springCollection.SetVelocity(zID, velocity.z);
        }

        public void SetSpringConfig(SpringConfiguration config)
        {
            springCollection.SetSpringConfig(xID, config);
            springCollection.SetSpringConfig(yID, config);
            springCollection.SetSpringConfig(zID, config);
        }

        public Vector3 GetTarget()
        {
            target.x = springCollection.GetTarget(xID);
            target.y = springCollection.GetTarget(yID);
            target.z = springCollection.GetTarget(zID);
            return value;
        }

        public Vector3 GetValue()
        {
            value.x = springCollection.GetValue(xID);
            value.y = springCollection.GetValue(yID);
            value.z = springCollection.GetValue(zID);
            return value;
        }

        public Vector3 GetVelocity()
        {
            velocity.x = springCollection.GetVelocity(xID);
            velocity.y = springCollection.GetVelocity(yID);
            velocity.z = springCollection.GetVelocity(zID);
            return velocity;
        }
    }
}
