using PgDump;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Windows;
using WpfApp3.EF.TableClasses;

namespace WpfApp3;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        //this.Exit += AppExit;
    }
    private void AppExit(object sender, ExitEventArgs e)
    {
        PgDump();
    }
    public async void PgDump()
    {
        ConnectionOptions options = new ConnectionOptions("localhost", 5432, "postgres", "your_password", "your_database");
        PgClient client = new PgClient(options);
        using MemoryStream memoryStream = new MemoryStream();
        StreamOutputProvider outputProvider = new StreamOutputProvider(memoryStream);
        FileOutputProvider provider = new("Dump.tar");
        await client.DumpAsync(provider, timeout: TimeSpan.FromMinutes(1));
        /*string PDPath = @"C:\ProgramFiles\PostgreSQL\17\bin\pg_dump.exe";
        string db = "postgres";
        string user = "postgres";
        string pass = "ksanox";
        string dump = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Dump.tar");
        string command = $"\"{PDPath}\"-U{user}-d{db}-f{dump}";
        Process process = new Process();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = $"/C{command}";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.CreateNoWindow = true;
        process.Start();
        process.WaitForExit();
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();*/
        
    }
    
    public void PgRestore(Account account)
    {
        
    }
}

