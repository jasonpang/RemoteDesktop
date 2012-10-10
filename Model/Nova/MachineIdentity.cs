using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace Model.Nova
{
    /// <summary>
    /// Uniquely identifies a machine based on their hardware ID.
    /// </summary>
    public class MachineIdentity
    {
        /// <summary>
        /// The unique hardware ID of the machine.
        /// </summary>
        public String UniqueId { get; set; }

        /// <summary>
        /// Gets the hardware ID of this machine.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentIdentity()
        {
            return GetSha1Hash(GetVolumeSerialId(GetPrimaryDriveLetter()) + GetCpuId(), Encoding.ASCII);
        }

        private static string GetPrimaryDriveLetter()
        {
            string drive = String.Empty;
            //Find first drive
            foreach (var compDrive in DriveInfo.GetDrives())
            {
                if (compDrive.IsReady && compDrive.DriveType == DriveType.Fixed)
                {
                    drive = compDrive.RootDirectory.ToString();
                    break;
                }
            }

            if (drive.EndsWith(":\\"))
            {
                //C:\ -> C
                drive = drive.Substring(0, drive.Length - 2);
            }

            return drive;
        }

        private static string GetVolumeSerialId(string drive)
        {
            var disk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
            disk.Get();

            string volumeSerial = disk["VolumeSerialNumber"].ToString();
            disk.Dispose();

            return volumeSerial;
        }

        private static string GetCpuId()
        {
            string cpuInfo = "";
            var managClass = new ManagementClass("win32_processor");
            var managCollec = managClass.GetInstances();

            foreach (ManagementObject managObj in managCollec)
            {
                if (cpuInfo == "")
                {
                    //Get only the first CPU's ID
                    cpuInfo = managObj.Properties["processorID"].Value.ToString();
                    break;
                }
            }

            return cpuInfo;
        }

        /// <summary>
        /// Calculates SHA1 hash
        /// </summary>
        /// <param name="text">input string</param>
        /// <param name="enc">Character encoding</param>
        /// <returns>SHA1 hash</returns>
        private static string GetSha1Hash(string text, Encoding enc)
        {
            var buffer = enc.GetBytes(text);
            var cryptoTransformSHA1 =
                new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(
                cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");

            return hash;
        }
    }
}