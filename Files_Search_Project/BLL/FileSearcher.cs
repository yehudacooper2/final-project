using DAL;
using System;
using System.Collections.Generic;
using System.IO;

//BLL - Business Logic Layer
 namespace _00_update_while_action__with_event_

{

    public static class FileSearcher
    {
        public static event Action  FilesDbUpDateHandler;

        public static event Action<string> FileSearcherHandler;

        public static string chosenFileName;

        public static string chosenDirectoryRoot;

        public static int searchId { get; set; }

        public static List<String> files = new List<String>();

        #region FileSearchInDirRoot

        /// <summary>
        ///     Returns the names of files (including their paths) that match the specified search
        ///     pattern in the specified directory and subdirectories and calling an event which shows the full path of the files
        ///     and adding the results into a list.
        ///     
        /// </summary>
        /// <param name="sDir"></param>
        /// <param name="fileName"></param>
        /// <param name="pathRoot"></param>
        public static void FileSearchInDirRoot(string sDir, string fileName, string pathRoot)
        {

            try

            {

                foreach (string f in Directory.GetFiles(sDir))
                {
                    if (f.Contains(fileName))
                    {
                     // calling an event when finding a suitable file.
                        FileSearcherHandler?.Invoke(Path.GetFullPath(f));

                        files.Add(Path.GetFullPath(f));

                    }
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    FileSearchInDirRoot(d, fileName, pathRoot);
                }
            }

            catch (System.Exception excpt)
            {
                  Console.WriteLine(excpt.Message);
            }

        }
        #endregion

        #region FileSearcherInChosenDir

        /// <summary>
        /// this function is searching throw a specified directory chosen by the user 
        /// and sends the results in the end of the searching to the DbManager functions.
        /// </summary>
        /// <param name="sDir"></param>
        /// <param name="fileName"></param>
        /// <param name="pathRoot"></param>
        public static void FileSearcherInChosenDir(string sDir, string fileName, string pathRoot)
        {
           //calling an event which updates the searchId.
               FilesDbUpDateHandler?.Invoke();

               FileSearchInDirRoot(sDir, fileName, pathRoot);

            foreach (string item in files)
            {
                DbManager.UpdateDbResults(searchId, item,pathRoot);

            }
        }
        #endregion

        #region FileSearcherInAllDir

        /// <summary>
        /// this function is searching throw all the drives in the PC and sends the results in the 
        /// end of the searching to the DbManager functions.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="allDir"></param>
        public static void FileSearcherInAllDir(string fileName,string allDir)
        {
            FilesDbUpDateHandler?.Invoke();

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                FileSearchInDirRoot($"{drive}", fileName,allDir);
            }
            foreach (string item in files)
            {
                DbManager.UpdateDbResults(searchId, item, "allDirectories");

            }
        }
        #endregion

        #region DbManagerUpdate

        /// <summary>
        /// this function sends to the DbManager the file name query and returns its searchId.
        /// </summary>
        /// <param name="searchQuary"></param>
        /// <returns></returns>
        public static int DbManagerUpdate(string searchQuery)
        {
            DbManager.UpdateDbSearchQuery(searchQuery);

            return searchId = DbManager.UpdateDbSearchId();

        }
        #endregion
    }
}
