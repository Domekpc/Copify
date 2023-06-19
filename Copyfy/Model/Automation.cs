using Copyfy.View;
using Copyfy.View.Control;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copyfy.Model
{
    class Automation
    {
        private Storage<CompareControl> compareControls;
        private readonly string comparePath = "Automation\\ComparePaths.cpy";
        private readonly bool automated = true;

        private LogFile log = new LogFile();
        // In the constructor, we load comparison controls from a file and perform comparisons.
        public Automation()
        {
            compareControls = new Storage<CompareControl>();
            compareControls.LoadFromFileSeparately(comparePath, path => { return new CompareControl(path[0], path[1]);}, automated);

            Compare();
        }
        private void Compare()
        {
            foreach (var control in compareControls.items)
            {
                try
                {
                    // Check if source and target directories exist.
                    if (!new DirectoryInfo(control.sourcePath).Exists)
                    {
                        throw new DirectoryNotFoundException($"{control.sourcePath} does not exist!");
                    }

                    if (!new DirectoryInfo(control.targetPath).Exists)
                    {
                        throw new DirectoryNotFoundException($"{control.targetPath} does not exist!");
                    }

                    // Get all subdirectories from source and target directories.
                    string[] sourcePathFolders = Directory.GetDirectories(control.sourcePath);
                    string[] targetPathFolders = Directory.GetDirectories(control.targetPath);

                    // Get the directories that are in the target but not in the source.()
                    List<string> difference = targetPathFolders.Except(sourcePathFolders).ToList();
                    
                    if (difference.Count > 0)
                    {
                        List<DirControl> folders = new List<DirControl>();// create DirControl from difference list, beacuse DirControl has the necessary methods

                        foreach (string folder in difference)
                        {
                            folders.Add(new DirControl(folder));
                        }

                        LoadingScreen copy = new LoadingScreen(folders, control.sourcePath, automated);// start to copy
                    }
                }
                catch (Exception ex)
                {
                    log.Write(ex.Message);
                }
            }
        }
    }
}
