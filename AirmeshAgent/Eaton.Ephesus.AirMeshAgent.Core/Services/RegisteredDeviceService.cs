using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaton.Ephesus.AirMeshAgent.Core.Services
{
    /// <summary>
    /// Keeps list of devices that have been registered with the Eaton Cloud
    /// </summary>
    public static class RegisteredDeviceService
    {
        // TODO: Move this to better storage, probably won't work well when multiple processes are spun up!

        private const string REGISTERED_DEVICE_FILE = "RegisteredDevices.txt";
        private static readonly List<string> registeredDevices = new List<string>();

        static RegisteredDeviceService()
        {
            if(File.Exists(REGISTERED_DEVICE_FILE))
            {
                registeredDevices.AddRange(File.ReadAllLines(REGISTERED_DEVICE_FILE));
            }
        }

        /// <summary>
        /// Checks if a device had already been registered with cloud
        /// </summary>
        /// <param name="UUID">The UUID of the device to determine if it has already been registered.</param>
        /// <returns>Returns true if device has already been registered</returns>
        public static bool IsDeviceRegistered(string UUID)
        {
            return registeredDevices.Contains(UUID);
        }

        /// <summary>
        /// Marks a device as being registered in the cloud.  Will also persist data to a file
        /// </summary>
        /// <param name="UUID">The Device to mard as registered</param>
        public static void RegisterDevice(string UUID)
        {
            if(!IsDeviceRegistered(UUID))
            {
                registeredDevices.Add(UUID);
                File.WriteAllLines(REGISTERED_DEVICE_FILE, registeredDevices);
            }
        }
    }
}
