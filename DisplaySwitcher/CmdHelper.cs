using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace DisplaySwitcher
{
    public static class CmdHelper
    {
        public static void Cmd(this string cmd)
        {
            Task.Factory.StartNew(() =>
            {
                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = cmd,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                process.Start();
                if (!process.HasExited)
                {
                    Thread.Sleep(5000);
                    process.Kill();
                }
                
                Thread.Yield();
            });
        }

        public static string FormatScreenChange(this DisplaySwitcherEnum displaySwitcherEnum )
        {
            switch (displaySwitcherEnum)
            {

                case DisplaySwitcherEnum.Extend:
                    return "displayswitch.exe/extend";
                case DisplaySwitcherEnum.External:
                    return "displayswitch.exe/external";
                case DisplaySwitcherEnum.Internal:
                    return "displayswitch.exe/internal";
                default:
                case DisplaySwitcherEnum.Unknown:
                case DisplaySwitcherEnum.Clone:
                    return "displayswitch.exe/clone";
            }
        }
    }
}