using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//using ICSharpCode.SharpZipLib.Zip;

namespace Sys.Tools
{
    /// <summary>
    /// 文件夹处理(删除等)
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 删除指定路径的文件夹及所包含的子文件
        /// </summary>
        /// <param name="folderPath">文件夹全路径</param>
        public static bool DeleteFloder(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath)) return true;

            //判断是否包含当前文件夹，不包含直接返回
            if (!Directory.Exists(folderPath)) return true;

            try
            {
                foreach (string d in Directory.GetFileSystemEntries(folderPath))
                {
                    if (File.Exists(d))
                    {
                        RemoveFileReadonly(d);

                        File.Delete(d);//直接删除其中的文件  
                    }
                    else
                    {
                        DeleteSubFolder(d);//递归删除子文件夹    
                    }
                }

                Directory.Delete(folderPath);
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        // <summary> 
        /// 删除文件夹（及文件夹下所有子文件夹和文件） 
        /// </summary> 
        /// <param name="directoryPath"></param> 
        public static void DeleteSubFolder(string directoryPath)
        {
            foreach (string d in Directory.GetFileSystemEntries(directoryPath))
            {
                if (File.Exists(d))
                {
                    RemoveFileReadonly(d);

                    File.Delete(d);     //删除文件    
                }
                else
                    DeleteSubFolder(d);    //删除文件夹 
            }

            Directory.Delete(directoryPath);    //删除空文件夹 
        }

        // <summary> 
        /// 删除文件夹下的所有文件(包括子文件夹)
        /// </summary> 
        /// <param name="directoryPath"></param> 
        public static void DeleteAllFiles(string directoryPath)
        {
            try
            {
                foreach (string d in Directory.GetFileSystemEntries(directoryPath))
                {
                    if (File.Exists(d))
                    {
                        RemoveFileReadonly(d);

                        File.Delete(d);     //删除文件    
                    }
                    else
                        DeleteSubFolder(d);    //删除文件夹 
                }
            }
            catch (System.Exception ex)
            {

            }
        }


        /// <summary>
        /// 去掉文件的只读属性(只读不能修改，需要先去只读)
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool RemoveFileReadonly(string filePath)
        {
            if (!File.Exists(filePath)) return true;

            if (File.GetAttributes(filePath).ToString().IndexOf("ReadOnly") != -1)
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
            }

            return true;
        }

        /// <summary>
        /// 去文件夹只读
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static bool RemoveFolderReadonly(string directoryPath)
        {
            if (!Directory.Exists(directoryPath)) return true;

            DirectoryInfo dir = new DirectoryInfo(directoryPath);
            //dir.Attributes = FileAttributes.Normal & FileAttributes.Directory;
            dir.Attributes = FileAttributes.Normal;

            return true;
        }


        /// <summary>
        /// 去掉指定文件夹下包含的所有文件夹及文件的只读属性
        /// </summary>
        /// <param name="directoryPath"></param>
        ///  <param name="isSubFolder">是否为子目录</param>
        /// <returns></returns>
        public static bool RemoveAllFilesReadonly(string directoryPath,bool isSubFolder)
        {
            foreach (string d in Directory.GetFileSystemEntries(directoryPath))
            {
                if (File.Exists(d))
                {
                    if (File.GetAttributes(d).ToString().IndexOf("ReadOnly") > -1)
                    {
                        File.SetAttributes(d, FileAttributes.Normal);
                    }

                    continue;
                }

                RemoveAllFilesReadonly(d, true);
            }

            if (isSubFolder) //去掉子文件夹的只读属性
            {
                DirectoryInfo dir = new DirectoryInfo(directoryPath);
                dir.Attributes = FileAttributes.Normal & FileAttributes.Directory;
            }

            return true;
        }

        /// <summary>
        /// 删除指定路径的文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return true;

            //判断是否包含当前文件夹，不包含直接返回
            if (File.Exists(filePath))
            {
                RemoveFileReadonly(filePath);

                File.Delete(filePath);//直接删除其中的文件  
            }

            return true;
        }

        /// <summary>
        /// 拷贝文件，生成新的文件
        /// </summary>
        /// <param name="fromFilePath">需要拷贝的文件路径</param>
        /// <param name="toFolderPath">模板文件夹路径</param>
        /// <param name="newFileName">新的文件名</param>
        /// <returns></returns>
        public static string CopyFile(string fromFilePath,string toFolderPath,string newFileName)
        {
            if (string.IsNullOrEmpty(fromFilePath) || !File.Exists(fromFilePath))
            {
                return "上传的文件路径有误！";
            }

            try
            {
                //如果目标文件夹不存在，则创建    
                if (!Directory.Exists(toFolderPath))
                {
                    Directory.CreateDirectory(toFolderPath);
                }

                string toFilePath = toFolderPath + "\\" + newFileName;
                if (File.Exists(toFilePath))
                {
                    return "目标文件夹中已经存在当前的文件！";
                }

                File.Copy(fromFilePath, toFilePath, false);
            }
            catch (System.Exception ex)
            {
                //return "操作失败！";
            }

            return string.Empty;

        }

        /// <summary>
        /// 拷贝文件，保留原文件名
        /// </summary>
        /// <param name="fromFilePath"></param>
        /// <param name="toFolderPath"></param>
        /// <returns></returns>
        public static string CopyFile(string fromFilePath, string toFolderPath)
        {
            if (string.IsNullOrEmpty(fromFilePath) || !File.Exists(fromFilePath))
            {
                return "文件路径有误！";
            }

            try
            {
                //如果目标文件夹不存在，则创建    
                if (!Directory.Exists(toFolderPath))
                {
                    Directory.CreateDirectory(toFolderPath);
                }

                int startIndex = fromFilePath.LastIndexOf("\\");
                if (startIndex < 0)
                {
                    return "文件路径有误！";
                }

                string fileName = fromFilePath.Substring(startIndex + 1);
                string toFilePath = toFolderPath + "\\" + fileName;

                File.Copy(fromFilePath, toFilePath, true);
            }
            catch (System.Exception ex)
            {
                return "操作失败！";
            }

            return string.Empty;
        }

        /// <summary>
        /// 拷贝文件夹中所有的子文件(包括子文件夹中的文件)
        /// </summary>
        /// <param name="fromFolder"></param>
        /// <param name="toFolder"></param>
        public static void CopyFolder(string fromFolder, string toFolder)
        {
            if (!Directory.Exists(fromFolder)) return;

            if (!Directory.Exists(toFolder))
            {
                Directory.CreateDirectory(toFolder);
            }

            // 遍历所有的文件和目录
            foreach (string file in Directory.GetFileSystemEntries(fromFolder))
            {
                if (Directory.Exists(file)) //文件夹
                {
                    string curdir = toFolder + "\\" + file.Substring(file.LastIndexOf("\\") + 1);
                    if (!Directory.Exists(curdir))
                    {
                        Directory.CreateDirectory(curdir);
                    }

                    CopyFolder(file, curdir);
                }
                else // 否则直接copy文件
                {
                    string filePath = fromFolder + "\\" + file.Substring(file.LastIndexOf("\\") + 1);

                    CopyFile(filePath, toFolder);
                }
            } 
        }

        /// <summary>
        /// 拷贝指定文件列表到指定目录
        /// </summary>
        /// <param name="fromFolder"></param>
        /// <param name="toFolder"></param>
        /// <param name="fileList"></param>
        public static void CopyFileList(string fromFolder, string toFolder,List<string> fileList)
        {
            if (!Directory.Exists(fromFolder)) return;

            if (!Directory.Exists(toFolder))
            {
                Directory.CreateDirectory(toFolder);
            }

            // 遍历所有的文件和目录
            foreach (string file in Directory.GetFileSystemEntries(fromFolder))
            {
                if (Directory.Exists(file)) //文件夹
                {
                    string curdir = toFolder + "\\" + file.Substring(file.LastIndexOf("\\") + 1);
                    if (!Directory.Exists(curdir))
                    {
                        Directory.CreateDirectory(curdir);
                    }

                    CopyFileList(file, curdir, fileList);
                }
                else // 否则直接copy文件
                {
                    string fileName = file.Substring(file.LastIndexOf("\\") + 1);
                    if(fileList.Contains(fileName,StringComparer.OrdinalIgnoreCase))
                    {
                        CopyFile(fromFolder + "\\" + fileName, toFolder);
                    }
                }
            }
        }


        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="toFolderPath"></param>
        /// <returns></returns>
        public static string CreateFolder(string toFolderPath)
        {
            if (string.IsNullOrEmpty(toFolderPath))
            {
                return string.Empty;
            }

            //如果目标文件夹不存在，则创建    
            if (!Directory.Exists(toFolderPath))
            {
                Directory.CreateDirectory(toFolderPath);
            }

            return string.Empty;

        }



        /// <summary>
        /// 判断指定的文件是否存在
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool CheckIsExistFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return false;

            return File.Exists(filePath);
        }

        /// <summary>
        /// 读取文件的内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadFileText(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return string.Empty;

            if (!File.Exists(filePath)) return string.Empty;

            //return File.ReadAllText(filePath);

            StringBuilder result = new StringBuilder();

            StreamReader reader = new StreamReader(filePath, System.Text.Encoding.Default);
            try
            {
                 while (reader.Peek() >= 0) 
                 {
                     result.AppendLine(@reader.ReadLine());
                 }
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                reader.Close();
            }


            return result.ToString();
        }

    }
}
