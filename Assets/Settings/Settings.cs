using UnityEngine;


namespace ProjectEgoSword
{
    public class Settings : MonoBehaviour
    {
        public FrameSettings frameSettings;
        public PhysicsSettings physicsSettings;

        private void Awake()
        {
            Debug.Log($"timeScale: {frameSettings.timeScale}");
            Time.timeScale = frameSettings.timeScale;

            Debug.Log($"targetFrameRate: {frameSettings.targetFPS}");
            Application.targetFrameRate = frameSettings.targetFPS;

            Debug.Log($"Default Solver Velocity Iterations: {physicsSettings.defaultSolverVelocityIterations}");
            Physics.defaultContactOffset = physicsSettings.defaultSolverVelocityIterations;
        }
    }

}