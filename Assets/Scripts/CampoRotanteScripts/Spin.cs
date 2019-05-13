using UnityEngine;

namespace Campo_Rotante_Scripts
{
    public class Spin : MonoBehaviour
    {
        public float speed;
        public float speedFactor = 1;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.up, speed * Time.deltaTime);
        }

        public void setSpeed(float speed)
        {
            this.speed = speed*speedFactor;
        }
    }
}