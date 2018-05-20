using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptWSFEditor
{
    class CommunSNCF
    {
        public static string logfile = @"C:\e.SNCF\logs\" + Application.ProductName + ".log"; 
        public enum logtype { Information, Success, Warning, Error, Failure };
        public static string HKLMApplication = "HKEY_CURRENT_USER\\SOFTWARE\\" + Application.CompanyName + "\\" + Application.ProductName + "\\Settings";
        public static string HKCUApplicationSubKeyConditions = "SOFTWARE\\" + Application.CompanyName + "\\" + Application.ProductName + "\\Settings\\Conditions";
        public static string HKLMValue1 = "";
        public static string scriptFile = "";
        public static string methodeFile = "";
        public static string jobid = "";
        public static string finalDestination = "";
        public static List<string> listMethode = new List<string>();
        public static List<string> listVariable = new List<string>();
        public static List<string> NeedMethode = new List<string>();
        public static List<string> NeedVariable = new List<string>();

        public static bool formInformationValid = false;

        public static string pbInformation = null;
        public static decimal pbAvancement = -1;
        public static string pbMethode = null;
        public static string errorMsg = "";
        public static string methodeChoiceForCondition = "";
        public static bool conditionAddBefore = true;

        public static bool IsNewCondition { get; internal set; }

        //public static string buildDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime.ToString("yyyyMMdd");
        //public static string build = Application.ProductVersion.TrimEnd('0') + buildDate;

        public static void Log(int iLogType, string logMessage)
        {
            try
            {
                using (StreamWriter w = File.AppendText(logfile))
                {
                    w.WriteLine(DateTime.Now.ToString("") + ";" + Enum.GetName(typeof(logtype), iLogType) + ";" + logMessage);
                }
            } catch(Exception ex)
            {
                ex.GetType();
            }
        }

        internal static string[] ReadRegAllSubkeys(string subkey)
        {
            List<string> result = null;
            try
            {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey(subkey);
                result = rk.GetValueNames().ToList<string>();
                return result.ToArray();
            }
            catch (Exception ex)
            {
                Log(3, ex.Message);
                ex.GetType();
                return null;
            }
        }

        public static  void Log(int iLogType, string logMessage, string txtResult)
        {
            Thread.Sleep(50);
            using (StreamWriter w = File.AppendText(logfile))
            {
                w.WriteLine(DateTime.Now.ToString("") + ";" + Enum.GetName(typeof(logtype), iLogType) + ";" + logMessage);
            }
            using (StreamWriter w = File.AppendText(txtResult))
            {
                w.WriteLine(logMessage);
            }
        }

        public static void StartSplashScreen()
        {
            try
            {
                Application.Run(new FormSplashScreen());
            } catch (Exception ex)
            {
                ex.GetType();
            }
        }

        public static bool IsWindows10()
        {
            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            string productName = (string)reg.GetValue("ProductName");

            return productName.StartsWith("Windows 10");
        }

        public static bool IsWindows7()
        {
            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            string productName = (string)reg.GetValue("ProductName");

            return productName.StartsWith("Windows 7");
        }

        public static string ReadRegParam(string key)
        {
            string result = null;
            try
            {
                result = (string)Registry.GetValue(HKLMApplication, key, "");
                return result;
            }
            catch (Exception ex)
            {
                Log(3, ex.Message);
                ex.GetType();
                return null;
            }
        }

        public static string ReadRegParam(string keyname, string valuename)
        {
            string result = null;
            try
            {
                result = (string)Registry.GetValue(keyname, valuename, "");
                return result;
            }
            catch (Exception ex)
            {
                Log(3, ex.Message);
                ex.GetType();
                return null;
            }
        }

        public static List<string> ReadRegAllSubkeys()
        {
            List<string> result = null;
            try
            {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey(HKCUApplicationSubKeyConditions);
                result = rk.GetValueNames().ToList<string>();
                return result;
            }
            catch (Exception ex)
            {
                Log(3, ex.Message);
                ex.GetType();
                return null;
            }
        }

        public static void SaveRegParam(string key, string value)
        {
            Registry.SetValue(HKLMApplication, key, value, RegistryValueKind.String);
        }

        public static void SaveRegParam(string path,string key, string value)
        {
            Registry.SetValue(path, key, value, RegistryValueKind.String);
        }

        public static long GetTotalFreeSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                {
                    return (drive.TotalFreeSpace / (1024 * 1024 * 1024));
                }
            }
            return -1;
        }

        public static string[] ConvertListBoxItemToArrayString(ListBox lb)
        {
            List<string> result = new List<string>();
            foreach(string item in lb.Items)
            {
                result.Add(item);
            }
            return result.ToArray();
        }

        public static string RemoveLastChar(string s, int x)
        {
            return s.Substring(s.Length - x);
        }

        public static string OtherLetterPartition(string logicalDiskId)
        {
            var deviceId = string.Empty;
            string otherLetter = null;

            var query = "ASSOCIATORS OF {Win32_LogicalDisk.DeviceID='" + logicalDiskId + "'} WHERE AssocClass = Win32_LogicalDiskToPartition";
            var queryResults = new ManagementObjectSearcher(query);
            var partitions = queryResults.Get();

            foreach (var partition in partitions)
            {
                query = "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" + partition["DeviceID"] + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition";
                queryResults = new ManagementObjectSearcher(query);
                var drives = queryResults.Get();


                foreach (var drive in drives)
                {
                    var letter = drive["Caption"];
                    deviceId = drive["DeviceID"].ToString();
                }
            }

            foreach (ManagementObject partition in new ManagementObjectSearcher(
                "ASSOCIATORS OF {Win32_DiskDrive.DeviceID='" + deviceId
                + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get())
            {
                foreach (ManagementObject disk in new ManagementObjectSearcher(
                            "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='"
                                + partition["DeviceID"]
                                + "'} WHERE AssocClass = Win32_LogicalDiskToPartition").Get())
                {
                    var letter = disk["Name"];
                    if (letter.ToString() != logicalDiskId)
                    {
                        otherLetter = letter.ToString();
                    }
                }
            }

            return otherLetter;
        }

        public static void CompareTorunFromAnotherAndClean(List<string> torun, List<string> other, List<string> ls)
        {
            ls.Clear();
            var FilteredList = torun.Intersect(other);
            foreach(string item in FilteredList)
            {
                Application.DoEvents();
                ls.Add(item);
            }
        }

        public static List<string> GetListBoxItemsToList(ListBox lb)
        {
            List<string> result = new List<string>();
            foreach (string item in lb.Items)
                result.Add(item);
            return result;
        }

        public static int GetLineIndexContains(RichTextBox rtbx, string search)
        {
            int index = 0;
            foreach(string line in rtbx.Lines)
            {
                if (line.Contains(search))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public static int GetLineIndexContains(List<string> lines, string s1, string s2)
        {
            int index = 0;
            foreach (string line in lines)
            {
                if (line.Contains(s1) && line.Contains(s2))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }


        public static int GetLineIndexContains(List<string> lines, string search)
        {
            int index = 0;
            foreach (string line in lines)
            {
                if (line.Contains(search))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }


        public static string ReadFirstLineFile(string file)
        {
            string result = "";
            result = File.ReadLines(file).First();
            return result;
        }

        public static async Task<string> MountISO(string fIso)
        {
            string result = null;
            if (IsWindows10())
            {
                try
                {
                    string scriptFilePath = @"C:\psScript.ps1";

                    if (File.Exists(scriptFilePath))
                    {
                        // Note: if the file doesn't exist, no exception is thrown    
                        File.Delete(scriptFilePath); // Delete the script, if it exists    
                    }

                    // Create the script to resize F and make U    
                    File.AppendAllText(scriptFilePath,
                    string.Format(
                        "$mountResult = Mount-DiskImage -ImagePath \"" + fIso + "\" -PassThru\r\n" +
                        "$driveLetter = ($mountResult | Get-Volume).DriveLetter\r\n" +
                        "Write-Host $driveLetter"
                        )
                    ); // And exit    

                    int exitcode = 0;
                    result = await Task.Run(() =>
                    {
                        return ExecutePSCommand("Powershell.exe" + " -executionpolicy bypass -File \"" + scriptFilePath + "\"", ref exitcode);
                    });
                    File.Delete(scriptFilePath); // Delete the script file    
                    if (exitcode > 0)
                    {
                        result = null;
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    Log(3, ex.Message);
                    ex.GetType();
                    return null;

                }
            }
            return result;
        }
   
        public static List<string> ReadFileFromTo(string file,string from, string to)
        {
            List<string> result = new List<string>();
            bool start = false;
            using (StreamReader rdr = new StreamReader(file))
            {
                string line;
                while ((line = rdr.ReadLine()) != null)
                {
                    if (line.ToLower().Contains(from.ToLower()) || start)
                    {
                        start = true;
                        result.Add(line);
                    }
                    if(line.ToLower() == to.ToLower() && start)
                    {
                        start = false;
                        return result;
                    }
                }
            }
            return result;
        }

        public static async void UnMountISO(string fIso)
        {
            string result = null;
            if (IsWindows10())
            {
                try
                {
                    string scriptFilePath = @"C:\psScript.ps1";

                    if (File.Exists(scriptFilePath))
                    {
                        // Note: if the file doesn't exist, no exception is thrown    
                        File.Delete(scriptFilePath); // Delete the script, if it exists    
                    }

                    // Create the script to resize F and make U    
                    File.AppendAllText(scriptFilePath,
                    string.Format(
                        "$mountResult = Dismount-DiskImage -ImagePath \"" + fIso + "\" \r\n"
                        )
                    ); // And exit    

                    int exitcode = 0;
                    result = await Task.Run(() =>
                    {
                        return ExecuteCmdCommand("Powershell.exe" + " -executionpolicy bypass -File \"" + scriptFilePath + "\"", ref exitcode);
                    });
                    File.Delete(scriptFilePath); // Delete the script file    
                    if (exitcode > 0)
                    {
                        Log(2, "UNmountISO result : " + "[" + exitcode + "]" + result);
                        result = null;
                    }
                    Log(0, "UNmountISO result : " + "[" + exitcode + "]" + result);
                }
                catch (Exception ex)
                {
                    Log(3, ex.Message);
                    ex.GetType();

                }
            }
        }

        public static int GetIndexOfDrive(string drive)
        {
            drive = drive.Replace(":", "").Replace(@"\", "");

            // execute DiskPart programatically
            Process process = new Process();
            process.StartInfo.FileName = "diskpart.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            process.StandardInput.WriteLine("list disk");
            process.StandardInput.WriteLine("exit");
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            // extract information from output
            string table = output.Split(new string[] { "DISKPART>" }, StringSplitOptions.None)[1];
            var rows = table.Split(new string[] { "\n" }, StringSplitOptions.None);
            for (int i = 3; i < rows.Length; i++)
            {
                if (rows[i].Contains("Volume"))
                {
                    int index = Int32.Parse(rows[i].Split(new string[] { " " }, StringSplitOptions.None)[3]);
                    string label = rows[i].Split(new string[] { " " }, StringSplitOptions.None)[8];

                    if (label.Equals(drive))
                    {
                        return index;
                    }
                }
            }

            return -1;
        }

        internal static string ConvertArrayToString(string[] array, string separator)
        {
            string result = string.Join(separator, array);
            return result;
        }

        public static int DiskPart(string diskpartFIleContent, string drive,int sizeFat32)
        {
            //Exemple :
            //diskpartFIleContent = "SELECT DISK " + index + "\r\n" + // Select the first disk drive    
            //            "CLEAN\r\n" + // Select the drive    
            //            "CREATE PARTITION PRIMARY SIZE=" + size + "\r\n" + // Shrink to half the original size    
            //            "ASSIGN\r\n" + // Make the drive partition    
            //            "ACTIVE\r\n" + // Assign it's letter    
            //            "FORMAT FS=fat32 QUICK LABEL=\"BOOT\"\r\n" + // Format it   
            //            "CREATE PARTITION PRIMARY\r\n" +
            //            "ASSIGN\r\n" +
            //            "FORMAT FS=ntfs QUICK LABEL=\"DEPLOY\"\r\n" +
            //            "EXIT";
            int result = 0;
            try
            {
                string scriptFilePath = @"C:\dpScript.txt";

                if (File.Exists(scriptFilePath))
                {
                    // Note: if the file doesn't exist, no exception is thrown    
                    File.Delete(scriptFilePath); // Delete the script, if it exists    
                }

                // Create the script to resize F and make U    
                string size = (sizeFat32 * 1024).ToString();

                if (sizeFat32 > GetTotalSize(drive + "\\"))
                {
                    File.AppendAllText(scriptFilePath,
                    string.Format(diskpartFIleContent)); // And exit    

                    int exitcode = 0;
                    string resultSen = ExecuteCmdCommand("DiskPart.exe" + " /s \"" + scriptFilePath + "\"", ref exitcode);
                    File.Delete(scriptFilePath); // Delete the script file    
                    if (exitcode > 0)
                    {
                        result = exitcode;
                    }
                }
                else
                {
                    MessageBox.Show("La taille du périphérique USB n'est pas suffisante.", "Périphérique non compatible", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                Log(3, ex.Message);
                result = 1;
                ex.GetType();
            }
            return result;
        }

        public static int GetTotalSize(string drive)
        {
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                if (d.IsReady && d.Name == drive)
                {
                    return (int)(d.TotalSize / (1024 * 1024 * 1024));
                }
            }
            return -1;
        }

        public static void AddLineAppendFile(string file, string line)
        {
            File.AppendAllText(file, line + Environment.NewLine,System.Text.Encoding.Unicode);
        }

        public static string ExecuteCmdCommand(string Command, ref int ExitCode)
        {
            ProcessStartInfo ProcessInfo;
            Process Process = new Process();
            string myString = string.Empty;
            ProcessInfo = new ProcessStartInfo("cmd.exe", "/C " + Command);

            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.WindowStyle = ProcessWindowStyle.Hidden;
            ProcessInfo.UseShellExecute = false;
            ProcessInfo.RedirectStandardOutput = true;
            Process.StartInfo = ProcessInfo;
            Process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Application.DoEvents();
            Process = Process.Start(ProcessInfo);
            Process.WaitForExit();
            Application.DoEvents();
            StreamReader myStreamReader = Process.StandardOutput;
            string line = myStreamReader.ReadLine();
            myString = myStreamReader.ReadToEnd();

            ExitCode = Process.ExitCode;
            Process.Close();

            return myString;
        }

        public static string ExecutePSCommand(string Command, ref int ExitCode)
        {
            ProcessStartInfo ProcessInfo;
            Process Process = new Process();
            string myString = string.Empty;
            ProcessInfo = new ProcessStartInfo("cmd.exe", "/C " + Command);

            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.WindowStyle = ProcessWindowStyle.Hidden;
            ProcessInfo.UseShellExecute = false;
            ProcessInfo.RedirectStandardOutput = true;
            Process.StartInfo = ProcessInfo;
            Process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Application.DoEvents();
            Process = Process.Start(ProcessInfo);
            Process.WaitForExit();
            Application.DoEvents();
            StreamReader myStreamReader = Process.StandardOutput;
            myString = myStreamReader.ReadLine();
            myStreamReader.ReadToEnd();

            ExitCode = Process.ExitCode;
            Process.Close();

            return myString;
        }

        public static long GetDirectorySize(string fullDirectoryPath)
        {
            long startDirectorySize = 0;
            if (!Directory.Exists(fullDirectoryPath))
                return startDirectorySize; //Return 0 while Directory does not exist.

            var currentDirectory = new DirectoryInfo(fullDirectoryPath);
            //Add size of files in the Current Directory to main size.
            currentDirectory.GetFiles().ToList().ForEach(f => startDirectorySize += f.Length);

            //Loop on Sub Direcotries in the Current Directory and Calculate it's files size.
            currentDirectory.GetDirectories().ToList()
                .ForEach(d => startDirectorySize += GetDirectorySize(d.FullName));

            return startDirectorySize / (1024 * 1024 * 1024);  //Return full Size of this Directory.
        }

        public static async Task<string> FormatPartition(string destDirName)
        {
            string result = null;
            try
            {
                string scriptFilePath = @"C:\psScript.ps1";

                if (File.Exists(scriptFilePath))
                {
                    // Note: if the file doesn't exist, no exception is thrown    
                    File.Delete(scriptFilePath); // Delete the script, if it exists    
                }

                // Create the script to resize F and make U    
                File.AppendAllText(scriptFilePath,
                string.Format(
                    "Format-Volume -DriveLetter " + destDirName.First().ToString() + " -FileSystem FAT32 -NewFileSystemLabel BOOT -confirm:$false"
                    )
                ); // And exit    

                int exitcode = 0;
                result = await Task.Run(() =>
                {
                    return ExecutePSCommand("Powershell.exe" + " -executionpolicy bypass -File \"" + scriptFilePath + "\"", ref exitcode);
                });
                File.Delete(scriptFilePath); // Delete the script file    
                if (exitcode > 0)
                {
                    result = null;
                }

                return result;
            }
            catch (Exception ex)
            {
                Log(3, ex.Message);
                ex.GetType();
                return null;
            }
        }

        public static string GetWindowsVersionBuild()
        {
            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
            string revision = (string)reg.GetValue("UBR").ToString();
            string currentbuild = (string)reg.GetValue("CurrentBuild").ToString();
            return currentbuild + "." + revision;
        }

        public static int FormatVersionWithoutDot(string version)
        {
            try
            {
                string[] aDigit = version.Split('.');
                string result = "";
                foreach (string digit in aDigit)
                {
                    string value = digit;
                    if (Convert.ToInt32(value) < 10 && value.Length < 2)
                    {
                        value = "0" + value;
                    }
                    result += value;
                }
                if (result != "")
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return Convert.ToInt32(version.Replace(".", ""));
                }
            }
            catch (Exception ex)
            {
                return Convert.ToInt32(version.Replace(".", ""));
                throw new Exception(ex.Message);
            }
        }

        public static void AdjustWidthComboBox_DropDown(object sender, System.EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (string s in ((ComboBox)sender).Items)
            {
                newWidth = (int)g.MeasureString(s, font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width + 5;
        }

        public static void OpenFolderInExplorer(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                try
                {
                    ProcessStartInfo l_psi = new ProcessStartInfo();
                    l_psi.FileName = "explorer";
                    l_psi.Arguments = string.Format("/root,{0}", folderPath);
                    l_psi.UseShellExecute = true;

                    Process l_newProcess = new Process();
                    l_newProcess.StartInfo = l_psi;
                    Log(0, "Ouverture de l'explorateur Windows dans le dossier : " + folderPath);
                    l_newProcess.Start();
                }
                catch (Exception ex)
                {
                    Log(3, "Echec d'ouverture de l'explorateur Windows dans le dossier : " + folderPath);
                    throw new Exception("Impossible d'ouvrir l'explorateur WIndows.", ex);
                }
            }
            else
            {
                Log(3, "Dossier : " + folderPath + " est introuvable");
                MessageBox.Show("Le dossier " + folderPath + " est introuvable.", "Dossier non trouvé", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public static void OpenFileWithDefaultProgram(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    Log(0, "Ouverture du fichier :" + filePath);
                    Process.Start(filePath);
                }
                catch (Exception ex)
                {
                    Log(3, "Echec d'ouverture du fichier :" + filePath);
                    throw new Exception("Impossible d'ouvrir le fichier avec son application par défaut.", ex);
                }
            }
            else
            {
                Log(3, "Fichier :" + filePath + " est introuvable");
                MessageBox.Show("Le fichier " + filePath + " est introuvable.", "Fichier non trouvé", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        public static void OpenFileWithNotepad(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    Log(0, "Ouverture du fichier avec le bloc-note :" + filePath);
                    Process.Start("notepad.exe", filePath);
                }
                catch (Exception ex)
                {
                    Log(3, "Echec d'ouverture du fichier :" + filePath);
                    throw new Exception("Impossible d'ouvrir le fichier avec son application par défaut.", ex);
                }
            }
            else
            {
                Log(3, "Fichier :" + filePath + " est introuvable");
                MessageBox.Show("Le fichier " + filePath + " est introuvable.", "Fichier non trouvé", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public static string FindFileByExtension(string path, string extension)
        {
            string result = null;
            if (Directory.Exists(path))
            {
                string[] fileEntries = Directory.GetFiles(path);
                foreach (string fileName in fileEntries)
                {
                    FileInfo fi = new FileInfo(fileName);
                    if (fi.Extension.ToLower() == "." + extension)
                    {
                        Log(0, "Fichier NFO trouvé : " + fileName);
                        result = fileName;
                    }
                }
            }
            else
            {
                MessageBox.Show("Dossier" + path + " introuvable.", "Dossier non trouvé", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log(2, "Dossier non trouvé : " + path);
            }
            if (result == null)
            {
                Log(2, "Fichier " + extension.ToUpper() + " non trouvé dans le dossier: " + path);
            }
            return result;
        }

        public static string FindFileByNameContains(string path, string partialname)
        {
            string result = null;
            try
            {
                if (Directory.Exists(path))
                {
                    string[] fileEntries = Directory.GetFiles(path);
                    foreach (string fileName in fileEntries)
                    {
                        FileInfo fi = new FileInfo(fileName);
                        if (fi.Name.ToLower().Contains(partialname.ToLower()))
                        {
                            Log(0, "Fichier trouvé : " + fileName);
                            result = fileName;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Dossier" + path + " introuvable.", "Dossier non trouvé", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Log(2, "Dossier non trouvé : " + path);
                }
                if (result == null)
                {
                    Log(2, "Fichier contenant " + partialname.ToUpper() + " est non trouvé dans le dossier: " + path);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public IEnumerable<string> FindInFileFromTo(string file, string search, string limit)
        {
            if (file != null)
            {
                return File.ReadLines(file).SkipWhile(line => !line.Contains(search)).Skip(3).TakeWhile(line => !line.Contains(limit));
            }
            else
            {
                Log(3, "La recherche dans le fichier " + file + " de " + search + " n'a pas aboutie");
                return null;
            }
        }

        public static bool CompareFolders(string pathA, string pathB, string except)
        {
            string fileDiffer = "";
            bool result = false;
            System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(pathA);
            System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(pathB);
            try
            {
                Log(0, "Début comparaison des dossiers : " + pathA + " et " + pathB);
                IEnumerable<FileInfo> list1 = dir1.GetFiles("*.*", SearchOption.AllDirectories);
                IEnumerable<FileInfo> list2 = dir2.GetFiles("*.*", SearchOption.AllDirectories);

                FileCompare myFileCompare = new FileCompare();

                var queryNotCommonFiles1 = list1.Except(list2, myFileCompare);
                var queryNotCommonFiles2 = list2.Except(list1, myFileCompare);

                IEnumerable<FileInfo> listFilter1 = Enumerable.Empty<FileInfo>();
                IEnumerable<FileInfo> listFilter2 = Enumerable.Empty<FileInfo>();


                if (queryNotCommonFiles1.Count() > 0)
                {
                    Log(0, "");
                    Log(0, "Les fichiers suivant sont  dans le dossier entrant mais pas dans le dossier sortant");
                    fileDiffer = "Les fichiers suivant sont  dans le dossier entrant mais pas dans le dossier sortant.\r\n";
                    foreach (var v in queryNotCommonFiles1)
                    {
                        if (except == "")
                        {
                            Log(0, v.FullName);
                            //(Autre WSF) a améliorer not WSF
                            if (!v.Name.Contains("NFO") && !v.Name.Contains("diskpart") && !v.Name.Contains("zip") && !v.Name.Contains("driverslist") && !v.Name.Contains("export_nfo") && !v.Name.Contains("hotfixes") && !v.Name.Contains("vide") && !v.Name.Contains("systeminfo") && !v.Name.Contains("InfosRecette.log") && !v.Name.Contains("wmic_product"))
                            {
                                fileDiffer += v.FullName + "\r\n";
                                listFilter1 = listFilter1.Append(v);
                            }
                        }
                        else
                        {
                            if (!v.FullName.Contains(except))
                            {
                                Log(0, v.FullName);
                                //(Autre WSF) a améliorer not WSF
                                if (!v.Name.Contains("NFO") && !v.Name.Contains("diskpart") && !v.Name.Contains("zip") && !v.Name.Contains("driverslist") && !v.Name.Contains("export_nfo") && !v.Name.Contains("hotfixes") && !v.Name.Contains("vide") && !v.Name.Contains("systeminfo") && !v.Name.Contains("InfosRecette.log") && !v.Name.Contains("wmic_product"))
                                {
                                    fileDiffer += v.FullName + "\r\n";
                                    listFilter1 = listFilter1.Append(v);
                                }
                            }
                        }
                    }
                }
                if (queryNotCommonFiles2.Count() > 0)
                {
                    Log(0, "");
                    Log(0, "Les fichiers suivant sont dans le dossier sortant mais pas dans le dossier entrant");
                    fileDiffer += "\r\n";
                    fileDiffer += "Les fichiers suivant sont dans le dossier sortant mais pas dans le dossier entrant.\r\n";
                    foreach (var v in queryNotCommonFiles2)
                    {
                        if (except == "")
                        {
                            Log(0, v.FullName);
                            //(Autre WSF) a améliorer not WSF
                            if (!v.Name.Contains("NFO") && !v.Name.Contains("diskpart") && !v.Name.Contains("zip") && !v.Name.Contains("driverslist") && !v.Name.Contains("export_nfo") && !v.Name.Contains("hotfixes") && !v.Name.Contains("vide") && !v.Name.Contains("systeminfo") && !v.Name.Contains("InfosRecette.log") && !v.Name.Contains("wmic_product"))
                            {
                                fileDiffer += v.FullName + "\r\n";
                                listFilter2 = listFilter2.Append(v);
                            }
                        }
                        else
                        {
                            if (!v.FullName.Contains(except))
                            {
                                Log(0, v.FullName);
                                //(Autre WSF) a améliorer not WSF
                                if (!v.Name.Contains("NFO") && !v.Name.Contains("diskpart") && !v.Name.Contains("zip") && !v.Name.Contains("driverslist") && !v.Name.Contains("export_nfo") && !v.Name.Contains("hotfixes") && !v.Name.Contains("vide") && !v.Name.Contains("systeminfo") && !v.Name.Contains("InfosRecette.log") && !v.Name.Contains("wmic_product"))
                                {
                                    fileDiffer += v.FullName + "\r\n";
                                    listFilter2 = listFilter2.Append(v);
                                }
                            }
                        }
                    }
                }

                var queryFilterCommonFiles = Enumerable.Empty<FileInfo>();
                var queryCommonFiles = list1.Intersect(list2, myFileCompare);
                if (queryNotCommonFiles1.Count() > queryNotCommonFiles2.Count())
                {
                    queryFilterCommonFiles = listFilter1.Intersect(listFilter2, myFileCompare);
                }
                else
                {
                    queryFilterCommonFiles = listFilter2.Intersect(listFilter1, myFileCompare);
                }

                if (queryCommonFiles.Count() > 0)
                {
                    Log(0, "");
                    Log(0, "Les fichiers suivant sont communs au deux dossiers : ");
                    foreach (var v in queryCommonFiles)
                    {
                        Log(0, v.Name);
                    }
                }

                if (queryFilterCommonFiles.Count() > 0)
                {
                    if (listFilter1.Count() == listFilter2.Count())
                    {
                        result = true;
                    }
                    else if (listFilter1.Count() > listFilter2.Count())
                    {
                        Log(3, "Il y a plus de fichiers dans le dossier, " + pathA);
                        fileDiffer += "\r\nIl y a plus de fichiers dans le dossier, " + pathA + "\r\n";
                        result = false;
                    }
                    else if (listFilter1.Count() < listFilter2.Count())
                    {
                        Log(3, "Il y a plus de fichiers dans le dossier, " + pathB);
                        fileDiffer += "\r\nIl y a plus de fichiers dans le dossier, " + pathB + "\r\n";
                        result = false;
                    }
                    if (listFilter1.Count() == 0 && listFilter2.Count() == 0)
                    {
                        Log(0, "Il ne semble pas y avoir d'erreur, à vous de juger...");
                        result = true;
                    }
                    else
                    {
                        Log(3, "Il y a une différence entre les deux dossiers.");
                        result = false;
                    }
                }
                else
                {
                    if (listFilter1.Count() == 0 && listFilter2.Count() == 0)
                    {
                        Log(0, "Il ne semble pas y avoir d'erreur, à vous de juger...");
                        result = true;
                    }
                    else
                    {
                        Log(3, "Il y a une différence entre les deux dossiers.");
                        result = false;
                    }
                }
                Log(0, "Le résultat de la comparaison des 2 dossiers est : " + result.ToString());
                Log(0, "");
                return result;
            }
            catch (Exception ex)
            {
                Log(3, "Comparaison des dossiers impossible, " + ex.Message);
                return result;
                throw new Exception("Compparaison des dossiers impossible.", ex);
            }
        }

        public static string[] FileToArray(string path)
        {
            var result = new List<string>();
            string[] fileLines = File.ReadAllLines(path);
            foreach (string line in fileLines)
            {
                result.Add(line);
            }
            return result.ToArray();
        }

        public static string VersionningFile(string fullfilename)
        {
            FileInfo filename = new FileInfo(fullfilename);
            string sfn = filename.Name.Remove(filename.Name.Length - 4);
            string nsfn = sfn;
            string fn = fullfilename;
            string dir = filename.DirectoryName;
            int count = 0;

            while (File.Exists(fn))
            {
                count++;
                nsfn = sfn + "_(version_" + count.ToString() + ")" + filename.Extension;
                fn = Path.Combine(dir, nsfn);
            }
            return fn;
        }

        public static bool CompareVersion(string fileA, string fileB)
        {
            FileCompare thiscompare = new FileCompare();
            bool result = thiscompare.CompareFile(fileA, fileB);
            return result;
        }

        public static string ReplaceBlank(string text)
        {
            string[] words = text.Split(' ');
            return String.Join(".", words);
        }

        public static void FillComboBoxFromDirectory(ComboBox cb, string directory,bool blank)
        {
            cb.Items.Clear();
            if (Directory.Exists(directory))
            {
                string[] files = Directory.GetFiles(directory);
                if (blank)
                    cb.Items.Add("");
                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    cb.Items.Add(fi.Name);
                }
            }
        }

        public static void FillListBoxFromDirectory(ListBox lb, string directory)
        {
            lb.Items.Clear();
            if (Directory.Exists(directory))
            {
                string[] files = Directory.GetFiles(directory);
                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    lb.Items.Add(fi.Name);
                }
            }
        }

        public static string GetFirstSelectItemInListBox(ListBox lb)
        {
            foreach (var item in lb.SelectedItems)
            {
               return item.ToString();
            }
            return null;
        }

        public static string[] GetAllSelectItemInListBox(ListBox lb)
        {
            List<string> result = Array.Empty<string>().ToList <string>();
            foreach (string item in lb.SelectedItems)
            {
                result.Add(item);
            }
            return result.ToArray();
        }

        public static void AddListBoxSelectedToListBox(string selected,ListBox lb)
        {
            lb.Items.Add(selected);
        }

        public static int CountFilesInDirectory(string directory, string pattern)
        {
            int result = 0;
            result = Directory.GetFiles(directory, pattern).Count();
            return result;
        }

        public static string ReadLinesRemoveFromTo(TextBox tb, int idx, int len, int from, string to)
        {
            string result = tb.Lines[idx].Remove(from, len).Replace(to, "");
            return result.Substring(0, result.Length - 2);
        }

        public static string FindStringInFirstLineAfterString(string file, string lastCharToFind)
        {
            string result = "";
            string tmp = File.ReadLines(file).First();
            result = tmp.Substring(tmp.LastIndexOf(lastCharToFind) + 1, tmp.Length - (tmp.LastIndexOf(lastCharToFind) + 2));
            return result;
        }

        public static string GetFileNameWithoutExtension(string filename)
        {
            string result = filename;
            return Path.GetFileNameWithoutExtension(result); 
        }

        public static List<string> GetFilesInDirectory(string dir)
        {
            List<string> result = new List<string>();

            return result;
        }

        public static void MoveListBoxItem(ListBox lb, int direction)
        {
                // Checking selected item
                if (lb.SelectedItem == null || lb.SelectedIndex < 0)
                    return; // No selected item - nothing to do

                // Calculate new index using move direction
                int newIndex = lb.SelectedIndex + direction;

                // Checking bounds of the range
                if (newIndex < 0 || newIndex >= lb.Items.Count)
                    return; // Index out of range - nothing to do

                object selected = lb.SelectedItem;

                // Removing removable element
                lb.Items.Remove(selected);
                // Insert it in new position
                lb.Items.Insert(newIndex, selected);
                // Restore selection
                lb.SetSelected(newIndex, true);
        }

        internal static void RemoveBlockFromTo(string scriptFile, string methode)
        {
            throw new NotImplementedException();
        }

        public static void MoveUpItemInListBox(ListBox lb)
        {
            MoveListBoxItem(lb, -1);
        }

        public static void MoveDownItemInListBox(ListBox lb)
        {
            MoveListBoxItem(lb, 1);
        }

        public static string GetComboValueSelected(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectValue = (string)comboBox.SelectedItem;
            return selectValue;
        }

        public static int GetComboIndexSelected(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            int selectIndex = (int)comboBox.SelectedIndex;
            return selectIndex;
        }

        public static bool IsWordExistsInFile(string completpathfile, string word,Panel pnl)
        {
            Application.DoEvents();
            pnl.Visible = true;
            bool result = false;
            //the path of the file
            FileStream inFile = new FileStream(completpathfile, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            string record;
            try
            {
                //the program reads the record and displays it on the screen
                record = reader.ReadLine();
                int i = 0;
                while (record != null)
                {
                    Application.DoEvents();
                    Thread.Sleep(10);
                    if (record.Contains(word))
                    {
                        result = true;
                        break;
                    }
                    i++;
                    record = reader.ReadLine();
                    if (i > 250)
                        break;
                }
            }
            finally
            {
                //after the record is done being read, the progam closes
                reader.Close();
                inFile.Close();
            }
            Application.DoEvents();
            pnl.Visible = false;
            return result;
        }

        internal static List<string> ReadFileFromToExtract(string file, string from, string to, string content)
        {
            List<string> result = new List<string>();
            if (file != "")
            {
                bool start = false;
                using (StreamReader rdr = new StreamReader(file))
                {
                    string line;
                    while ((line = rdr.ReadLine()) != null)
                    {
                        if (line.ToLower().Contains(from.ToLower()) || start)
                        {
                            start = true;
                            if (line.ToLower().Contains(content.ToLower()))
                                result.Add(line);
                        }
                        if (line.ToLower() == to.ToLower() && start)
                        {
                            start = false;
                            return result;
                        }
                    }
                }
            }
            return result;
        }
    }

    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Append<T>(
            this IEnumerable<T> source, params T[] tail)
        {
            return source.Concat(tail);
        }
    }

    class FileCompare : IEqualityComparer<FileInfo>
    {
        public FileCompare() { }

        public bool Equals(FileInfo f1, FileInfo f2)
        {
            return (f1.Name == f2.Name);
        }

        public bool CompareFile(string file1, string file2)
        {
            try
            {
                int file1byte;
                int file2byte;
                FileStream fs1;
                FileStream fs2;

                fs1 = new FileStream(file1, FileMode.Open);
                fs2 = new FileStream(file2, FileMode.Open);

                do
                {
                    // Read one byte from each file.
                    file1byte = fs1.ReadByte();
                    file2byte = fs2.ReadByte();
                }
                while ((file1byte == file2byte) && (file1byte != -1));

                // Close the files.
                fs1.Close();
                fs2.Close();

                return ((file1byte - file2byte) == 0);
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("Erreur lors de la comparaison des fichiers, " + ex.Message);
            }
        }

        public List<string> CompareBDDLogFile(string file1, string file2)
        {
            try
            {
                StreamReader fs1;
                StreamReader fs2;
                string line;

                fs1 = new StreamReader(file1);
                fs2 = new StreamReader(file2);

                List<string> af1 = new List<string>();
                List<string> af2 = new List<string>();
                string nline = "";
                int i = 0;
                while ((line = fs1.ReadLine()) != null)
                {
                    if (line.ToLower().Contains("failure"))
                    {
                        if (!line.Contains(@"\\") && !line.Contains("LTITriggerUpgradeFailure.wsf"))
                        {
                            nline = line.Split('>')[0] + ">";
                            af1.Add(nline);
                            i++;
                        }
                    }
                }

                nline = "";
                int x = 0;
                while ((line = fs2.ReadLine()) != null)
                {
                    if (line.ToLower().Contains("failure"))
                    {
                        if (!line.Contains(@"\\") && !line.Contains("LTITriggerUpgradeFailure.wsf"))
                        {
                            nline = line.Split('>')[0] + ">";
                            af2.Add(nline);
                            x++;
                        }
                    }
                }

                // Close the files.
                fs1.Close();
                fs2.Close();

                if (af2.Except(af1).ToList().Count() > 0)
                {
                    return af2.Except(af1).ToList();
                }
                else
                {
                    return new List<string>();
                }
            }
            catch (Exception ex)
            {
                return new List<string>();
                throw new Exception("Erreur lors de la comparaison des fichiers BDD.log, " + ex.Message);
            }
        }

        public int GetHashCode(FileInfo fi)
        {
            string s = string.Format("{0}", fi.Name);
            return s.GetHashCode();
        }
    }
}
