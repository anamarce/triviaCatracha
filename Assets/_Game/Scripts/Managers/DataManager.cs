
using UnityEngine;
using System;
using System.Xml;


#if UNITY_WINRT && !UNITY_EDITOR  
   using LegacySystem.IO;  

#else
    using System.IO;


    public class DataManager : MonoBehaviour  {

        public string UserFilesRootPath;

        void Awake()
        {
            UserFilesRootPath = Application.persistentDataPath;
        }
  

        public static string Encrypt(string textToEncrypt)
        {
            string temp;
#if UNITY_WINRT && !UNITY_EDITOR  
             temp = LegacySystem.IO.Encrypt(textToEncrypt)
        #else
            temp = SecurityClass.Encrypt(textToEncrypt);
#endif
            return temp;
        }
        public static string Decrypt(string textToEncrypt)
        {
            string temp;
#if UNITY_WINRT && !UNITY_EDITOR  
             temp = LegacySystem.IO.Decrypt(textToEncrypt)
        #else
            temp = SecurityClass.Decrypt(textToEncrypt);
#endif
            return temp;
        }
        public  bool DirectoryExists(string currentWorkingPath,string directorio)  // Todo basada en currentWorkingpath
        {  if(Directory.Exists(currentWorkingPath + "/" + directorio)) 
        { return true; } 
        else 
        { return false; } 
        }
        public  void CreateDirectory(string currentWorkingPath,string directorio) 
        { 
		
            if(DirectoryExists(currentWorkingPath,directorio) == false) 
            { 
                Directory.CreateDirectory(currentWorkingPath + "/" + directorio); 
            } 
            else 
            {
                return;
            } 
        }
        public  void DeleteDirectory(string currentWorkingPath, string directorio) 
        { 
            if(DirectoryExists(currentWorkingPath,directorio)) 
            { 
                Directory.Delete(currentWorkingPath + "/" + directorio, true); 
            } 
            else  
            {
                return;
            } 
        }
        public  bool FileExists(string currentWorkingPath,string filePath) 
        { 
            if(File.Exists(currentWorkingPath + "/" + filePath)) 
            { return true; } 
            else 
            { return false;  }
        }
        // ------------------- Creando el archivo ------------------------------------
        public  void CreateFile(string currentWorkingPath,string directorio, string filename, string filetype, 
            string fileData, bool replace=false) 
        { 
            if (replace)
            {
                File.WriteAllText( currentWorkingPath+ "/" + directorio + "/" + filename + "." + filetype, fileData); 
		  
            }
            else
            {
                if(DirectoryExists (currentWorkingPath,directorio)) 
                { 
                    if(FileExists(currentWorkingPath,directorio + "/" + filename + "." + filetype) == false)  // No existe
                    { // Crear el archivo
                        File.WriteAllText( currentWorkingPath+ "/" + directorio + "/" + filename + "." + filetype, fileData); 
                    }
                }
            }
        }
        //--------------------------------------------------------------------
        public  void DeleteFile(string currentWorkingPath,string filePath) 
        { 
            if(FileExists(currentWorkingPath, filePath)) 
            { 
                File.Delete(currentWorkingPath + "/" + filePath); 
            }
            else 
            { 
                return;
            } 
        }
	
     
        //--------------------Leer el contenido Archivo ---------------------------------
        public  string ReadFile(string currentWorkingPath,string directorio, string filename, string filetype) 
        {   
            string fileContents = string.Empty;
		
            if(DirectoryExists(currentWorkingPath,directorio))
            {
                if(FileExists(currentWorkingPath,directorio + "/" + filename + "." + filetype) )
                { // Leer el contenido 
                    fileContents = File.ReadAllText(currentWorkingPath + "/" + 
                                                    directorio + "/" + filename + "." + filetype);
                    return fileContents; 
                }
            }
		
            return fileContents;
        }
	
	
	
	  
	
	
    }


#endif