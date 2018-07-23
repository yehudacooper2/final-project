using System;

 
namespace _00_update_while_action__with_event_

{

    class Program
    {
        static void Main(string[] args)
       {

            string ProjectChoosenOption;
            do
            {
                do
                {
                    Console.WriteLine("please choose one of the following options:\n" +
                        "1.Enter file name to search.\n" +
                        "2.Enter file name to search + parent directory to search in\n" +
                        "3.Exit.");

                    ProjectChoosenOption = (Console.ReadLine());

                } while (ProjectChoosenOption != "1" && ProjectChoosenOption != "2" && ProjectChoosenOption != "3");

                   Console.Write("Enter file name to search:");
                   FileSearcher.chosenFileName = Console.ReadLine();
                // subscribe a function to the event
                   FileSearcher.FilesDbUpDateHandler += () => { FileSearcher.searchId = FileSearcher.DbManagerUpdate(FileSearcher.chosenFileName); };
                // subscribe a function to the event
                   FileSearcher.FileSearcherHandler += (p) => { Console.WriteLine(p); };

                switch (ProjectChoosenOption)
                {
                    case "1":
                        {
                            Console.WriteLine("Start Searching...");
                            FileSearcher.FileSearcherInAllDir(FileSearcher.chosenFileName, "All Directories");
                        }
                        break;
                    case "2":

                        {
                            Console.Write("Enter root directory to search in:");
                            FileSearcher.chosenDirectoryRoot = Console.ReadLine();
                            FileSearcher.FileSearcherInChosenDir(FileSearcher.chosenDirectoryRoot, FileSearcher.chosenFileName, FileSearcher.chosenDirectoryRoot);
                        }
                        break;
                    case "3":
                        { }
                        break;
                }
            } while (ProjectChoosenOption == "1" || ProjectChoosenOption == "2");
        }
    }
}
