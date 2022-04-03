using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A 2D representation of a spring
namespace Snowdrama.Spring
{
    public class Spring2D
    {
        SpringCollection springCollection;
        Vector2 value;
        Vector2 target;
        Vector2 velocity;

        int xID;
        int yID;
        public Spring2D(SpringConfiguration config, Vector2 initialValue = new Vector2())
        {
            springCollection = new SpringCollection();
            xID = springCollection.Add(initialValue.x, config);
            yID = springCollection.Add(initialValue.y, config);
        }

        public void Update(float deltaTime)
        {
            springCollection.Update(deltaTime);
            value.x = springCollection.GetValue(xID);
            value.y = springCollection.GetValue(yID);
        }

        public void SetTarget(Vector2 position)
        {
            springCollection.SetTarget(xID, position.x);
            springCollection.SetTarget(yID, position.y);
        }

        public void SetValue(Vector2 position)
        {
            springCollection.SetValue(xID, position.x);
            springCollection.SetValue(yID, position.y);
        }

        public void SetVelocity(Vector2 position)
        {
            springCollection.SetVelocity(xID, position.x);
            springCollection.SetVelocity(yID, position.y);
        }

        public void SetSpringConfig(SpringConfiguration config)
        {
            springCollection.SetSpringConfig(xID, config);
            springCollection.SetSpringConfig(yID, config);
        }
        public Vector3 GetTarget()
        {
            target.x = springCollection.GetTarget(xID);
            target.y = springCollection.GetTarget(yID);
            return value;
        }

        public Vector3 GetValue()
        {
            value.x = springCollection.GetValue(xID);
            value.y = springCollection.GetValue(yID);
            return value;
        }

        public Vector3 GetVelocity()
        {
            velocity.x = springCollection.GetVelocity(xID);
            velocity.y = springCollection.GetVelocity(yID);
            return velocity;
        }
    }
}
