using System.IO;
using System.Threading;

namespace BG3_Save_Backup.Classes
{
    public static class SafeFileHandle {
        public static FileStream WaitForFile(string fullpath) {
            for (int numTries = 0; numTries < 100; numTries++) {
                FileStream fs = null;
                try {
                    fs = new FileStream(fullpath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    return fs;
                } catch (IOException) {
                    fs?.Dispose();
                    Thread.Sleep(250);
                }
            }
            return null;
        }
    }
}
