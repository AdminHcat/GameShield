using DevsDaddy.GameShield.Core.Payloads;
using DevsDaddy.Shared.EventFramework;

namespace DevsDaddy.GameShield.Core.Modules.Memory
{
    /// <summary>
    /// Memory Protector Module
    /// </summary>
    public class MemoryProtector : IShieldModule
    {
        public Options Config => _currentOptions;
        private Options _currentOptions = new Options();
        private bool _initialized = false;

        /// <summary>
        /// Setup Module
        /// </summary>
        /// <param name="config"></param>
        /// <param name="reinitialize"></param>
        public void SetupModule(IShieldModuleConfig config = null, bool reinitialize = false) {
            // Change Configuration
            _currentOptions = (Options)config ?? new Options();
            EventMessenger.Main.Publish(new SecurityModuleConfigChanged {
                Module = this,
                Config = _currentOptions
            });
            
            // Initialize Module
            if (!_initialized && !reinitialize)
                Initialize();
        }

        /// <summary>
        /// Disconnect Module
        /// </summary>
        public void Disconnect() {
            
            // Fire Disconnected Complete
            EventMessenger.Main.Publish(new SecurityModuleDisconnected {
                Module = this
            });
        }

        /// <summary>
        /// Initialize Module
        /// </summary>
        private void Initialize() {

            // Fire Initialization Complete
            EventMessenger.Main.Publish(new SecurityModuleInitialized {
                Module = this
            });
        }

        /// <summary>
        /// Get Module Information
        /// </summary>
        /// <returns></returns>
        public ModuleInfo GetModuleInfo() {
            return new ModuleInfo {
                Name = "Memory Protector",
                Description = "This module monitors and sends a notification if a user has attempted to change the value of a protected (SecuredType) types in memory.",
                DocumentationLink = "https://github.com/DevsDaddy/GameShield/wiki/Modules-Overview#memory-protection"
            };
        }
        
        // Module Options Configuration
        [System.Serializable]
        public class Options : IShieldModuleConfig
        {
            public float FloatEpsilon = 0.0001f;
            public float Vector2Epsilon = 0.1f;
            public float Vector3Epsilon = 0.1f;
            public float Vector4Epsilon = 0.1f;
            public float QuaternionEpsilon = 0.1f;
            public float ColorEpsilon = 0.1f;
            public byte Color32Epsilon = 1;
        }
    }
}