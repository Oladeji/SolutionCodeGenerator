using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CleanAppFilesGenerator
{
    public static class HelperClass
    {
        public static string GetNodeNameFromXml(string path, string nodeName)
        {
            using (XmlReader reader = XmlReader.Create(path))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == nodeName)
                    {
                        return reader.ReadElementString();
                    }
                }
            }
            return "";
        }

        public static void EnsureFolderIsCreated(string folderlocation, string folderName)
        {
            if (!Directory.Exists( $"{folderlocation}\\{folderName}"))
            {
                Directory.CreateDirectory($"{folderlocation}\\{folderName}");
            }
        }



    }



}
