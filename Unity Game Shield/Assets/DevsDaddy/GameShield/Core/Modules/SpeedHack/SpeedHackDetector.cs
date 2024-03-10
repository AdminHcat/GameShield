using DevsDaddy.GameShield.Core.Payloads;
using DevsDaddy.Shared.EventFramework;

namespace DevsDaddy.GameShield.Core.Modules.SpeedHack
{
    /// <summary>
    /// SpeedHack Detector Module
    /// </summary>
    public class SpeedHackDetector : IShieldModule
    {
        private Options _currentOptions = new Options();
        private bool _initialized = false;
        private bool _isPaused = false;

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
        /// Toggle Pause for Current Detector
        /// </summary>
        /// <param name="isPaused"></param>
        public void PauseDetector(bool isPaused) {
            if(isPaused == _isPaused) return;
            _isPaused = isPaused;
            EventMessenger.Main.Publish(new SecurityModulePause {
                Module = this,
                IsPaused = _isPaused
            });
        }

        /// <summary>
        /// Check if Detector Paused
        /// </summary>
        /// <returns></returns>
        public bool IsPaused() {
            return _isPaused;
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
                Name = "SpeedHack Detector",
                Description = "The module allows you to track time acceleration attempts to change character speeds in-game, by monitoring the real time flow and frame time.",
                DocumentationLink = "https://github.com/DevsDaddy/GameShield/wiki/Modules-Overview#speedhack-detector"
            };
        }
        
        // Module Options Configuration
        [System.Serializable]
        public class Options : IShieldModuleConfig
        {
            
        }
    }
}