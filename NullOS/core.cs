//
// Created by aswer 
//
// at 26.10.2024
//
// TODO:
// 1. Change user password (passwd)
// 2. USEEEEEERS!!!!!!!!!!!!!!!
// 3. Base programms
// 4. hot keys
//

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NullOS
{
    internal class core
    {
        public static void core_main()
        {
            Console.WriteLine("----------------");
            Console.WriteLine("Loaded!");
            Console.WriteLine("----------------");

            string user_config_path = "configs/user.json";
            string system_config_path = "configs/system.json";
            string main_file_system = Directory.GetCurrentDirectory() + @"\usr_data\";

            string current_directory = main_file_system;

            string buffer = "";

            string path = "";
            string data = "";

            bool check_config_user = File.Exists(user_config_path);
            bool check_config_system = File.Exists(system_config_path);
            bool check_main_file_system = Directory.Exists(main_file_system);
            if (check_main_file_system == false) { Directory.CreateDirectory(main_file_system); }
            if (check_config_system == false) { Directory.CreateDirectory("configs"); string buffer_device_name = write_system_config(); var info = new {system = "nullOS", version = "0.7", device_name = buffer_device_name, dev = "false" }; write_config(info, system_config_path); }
            if (check_config_user == false) { var info = write_user_config(); write_config(info, user_config_path); }

            bool start = true;
            bool login = true;

            string user = read_user();
            string current_user = user;
            string user_pass = read_user_pass();

            while(login)
            {
                Console.Write("Enter login: ");
                string buffer_login = Console.ReadLine();
                Console.Write("Enter password: ");
                string buffer_password = Console.ReadLine();
                if (buffer_password == user_pass && buffer_login == user)
                {
                    login = false;
                    Console.Write("\n");
                }
                else { Console.WriteLine("Login or password incorrect. "); }

            } //login

            while (start)
            {
                user = read_user();
                string device_name = read_device_name();
                string dev = read_dev();
                read_device_name();
                //Console.WriteLine($"Current directory: {current_directory}");
                Console.ForegroundColor = ConsoleColor.Green; Console.Write(current_user); Console.ForegroundColor = ConsoleColor.White; Console.Write("@"); Console.ForegroundColor = ConsoleColor.Cyan; Console.Write(device_name); Console.ForegroundColor = ConsoleColor.White; Console.Write("$ ");
                string command = Console.ReadLine();

                switch (command)
                {
                    default:
                        Console.WriteLine($"Command {command} not exist.");
                        break; //DONE

                    case @"":
                        start = false;
                        break; // DONE? || HOTKEY: "\u0004" - "CTRL + D" || ANALOG: "exit" COMMAND

                    case "help":
                        Console.WriteLine(@"
Total commands: 12
Silent commands: 2

1. exit - exit this system
2. uname - system name
3. ver - print info
4. reset - reset system
5. touch - create file
6. cat - read file
7. echo - write file
8. ls - list files and directories
9. su - change user
10. help - this list
");
                        break; //ATTENTION!

                    case "":
                        break; //DONE

                    case "exit":
                        start = false;
                        break; //DONE

                    case "dev":
                        if (current_user == "root")
                        {
                            string buffer_os_device_name = read_system();
                            string buffer_os_ver = read_version();
                            string buffer_device_name = read_device_name();
                            var info = new { system = buffer_os_device_name, version = buffer_os_ver, device_name = buffer_device_name, dev = "true" };
                            write_config(info, system_config_path);
                            Console.WriteLine("Success.");
                            break;
                        }
                        else { Console.Write("["); Console.ForegroundColor = ConsoleColor.Red; Console.Write("ERROR"); Console.ForegroundColor = ConsoleColor.White; Console.Write("] Operation not permitted.\n"); }
                        break; //DONE?

                    case "uname":
                        Console.WriteLine("nullOS C#");
                        break; //+-

                    case "ver":
                        ver(current_user);
                        break; //okaaay..

                    case "edit":
                        if (dev == "true")
                        {
                            Console.Write("Enter username: ");
                            string buffer_user = Console.ReadLine();
                            if (buffer_user == "root")
                            {
                                Console.WriteLine("Invalid value.");
                                break;
                            }
                            var username = new {user =  buffer_user};
                            write_config(username, user_config_path);
                            Console.Write("Enter device name: ");
                            string buffer_device_name = Console.ReadLine();
                            Console.Write("Enter OS version: ");
                            string buffer_os_ver = Console.ReadLine();
                            Console.Write("Enter system name: ");
                            string buffer_os_device_name = Console.ReadLine();
                            Console.Write("Enter developer option (true/false): ");
                            string buffer_dev = Console.ReadLine();
                            var info = new { system = buffer_os_device_name, version = buffer_os_ver, device_name = buffer_device_name, dev = buffer_dev };
                            write_config(info, system_config_path);
                        }
                        else { Console.Write("["); Console.ForegroundColor = ConsoleColor.Red; Console.Write("ERROR"); Console.ForegroundColor = ConsoleColor.White; Console.Write("] Operation not permitted.\n"); }
                        break; //idk
                        
                    case "reset":
                        if (current_user == "root")
                        {
                            Console.Write("Reset system? (y/n) ");
                            string buff = Console.ReadLine();
                            if (buff == "y")
                            {
                                Console.WriteLine("SYSTEM HAS BEEN RESETED!!!\nPlease, Boot system again.");
                                Thread.Sleep(1000);
                                start = false;
                                Directory.Delete("configs", true);
                                Console.WriteLine("Success");
                                Thread.Sleep(750);
                            }
                            else { Console.WriteLine("Canceled."); }
                        }
                        else { Console.Write("["); Console.ForegroundColor = ConsoleColor.Red; Console.Write("ERROR"); Console.ForegroundColor = ConsoleColor.White; Console.Write("] Operation not permitted.\n"); }
                        break; //DONE

                    case "touch":
                        try
                        {
                            Console.Write("Enter path to file: ");
                            path = Console.ReadLine();
                            create_file(current_directory + path);
                        }
                        catch (Exception ex)  { Console.WriteLine("Unknown err"); }
                        break; //DONE || FILESYSTEM

                    case "cat":
                        try
                        {
                            Console.Write("Enter path to file: ");
                            path = Console.ReadLine();
                            Console.WriteLine(read_file(current_directory + path));
                        }
                        catch (Exception e) { Console.WriteLine("Error. The file may not exist or could not be accessed."); }
                        break; //DONE || FILESYSTEM

                    case "echo":
                        try
                        {
                            Console.Write("Enter text: ");
                            data = Console.ReadLine();
                            Console.Write("Enter path to file: ");
                            path = Console.ReadLine();
                            write_file(current_directory + path, data);
                        }
                        catch (Exception e) { Console.WriteLine("Unknown err"); }
                        break; //DONE || FILESYSTEM

                    case "ls":
                        //Console.WriteLine(files_list());
                        Console.Write("enter path or just press enter: ");
                        string buff_ls = Console.ReadLine();
                        if (buff_ls != "")
                        {
                            files_list(current_directory + buff_ls);
                        }
                        else
                        {
                            files_list(current_directory);
                        }
                        break; //DONE

                    //case "cd":
                        Console.Write("Enter directory name: ");
                        buffer = Console.ReadLine();
                        current_directory += @"\" + buffer;
                        break; // ls DONE || cd off || FILESYSTEM

                    case "su":
                        Console.Write("Enter username: ");
                        string user_buffer = Console.ReadLine();
                        if (user_buffer == "root")
                        {
                            Console.Write("Enter password: ");
                            string pass = Console.ReadLine();
                            if (pass == "Da6RumTf") { current_user = "root";  Console.Write("Success\n"); }
                        }
                        else if (user_buffer == user)
                        {
                            current_user = user; Console.WriteLine("Success");
                        }
                        else
                        {
                            Console.WriteLine("username or password is not correct.");
                        }
                        break; //not bad 

                    case "rm":
                        try
                        {
                            Console.Write("Enter path: ");
                            path = Console.ReadLine();
                            delete_file(current_directory + path);
                        }
                        catch (Exception e) { Console.WriteLine("Unknown err"); }
                        break; //DONE || FILESYSTEM

                    case "mkdir":
                        try
                        {
                            Console.Write("Enter path: ");
                            path = Console.ReadLine();
                            create_directory(current_directory + path);
                        }
                        catch (Exception e) { Console.WriteLine("Unknown err"); }
                        break; //DONE || FILESYSTEM

                    case "rf":
                        try
                        {
                            Console.Write("Enter path to directory: ");
                            path = Console.ReadLine();
                            delete_directory(current_directory + path);
                        }
                        catch (Exception e) { Console.WriteLine("Unknown err"); }
                        break; //DONE || FILESYSTEM
                }
            } //commands
        }

        //methods and links
        public static string read_user()
        {
            string jsonString = File.ReadAllText("configs/user.json");
            Config config = JsonSerializer.Deserialize<Config>(jsonString);
            string buffer_user = config.user;
            return buffer_user;

        }
        public static string read_user_pass() {
            string jsonString = File.ReadAllText("configs/user.json");
            Config config = JsonSerializer.Deserialize<Config>(jsonString);
            string buffer_user = config.pass;
            return buffer_user;
        }
        public static string read_device_name()
        {
            string jsonString = File.ReadAllText("configs/system.json");
            Config config = JsonSerializer.Deserialize<Config>(jsonString);
            string buffer_device_name = config.device_name;
            return buffer_device_name;

        }
        public static string read_dev()
        {
            string jsonString = File.ReadAllText("configs/system.json");
            Config config = JsonSerializer.Deserialize<Config>(jsonString);
            string buffer_system = config.dev;
            return buffer_system;
        }
        public static string read_system()
        {
            string jsonString = File.ReadAllText("configs/system.json");
            Config config = JsonSerializer.Deserialize<Config>(jsonString);
            string buffer_system = config.system;
            return buffer_system;

        }
        public static string read_version()
        {
            string jsonString = File.ReadAllText("configs/system.json");
            Config config = JsonSerializer.Deserialize<Config>(jsonString);
            string buffer_version = config.version;
            return buffer_version;

        }
        public static object write_config(object info, string path)
        {
            string jsonString = JsonSerializer.Serialize(info);
            Console.WriteLine($"DEBUG: {jsonString}"); //DEBUG
            File.WriteAllText(path, jsonString);
            return info;
        }
        public static object write_user_config()
        {
            bool success = true;
            object info = new {};
            while (success)
            {
                Console.Write("Enter username: ");
                string buffer_user = Console.ReadLine();
                if (buffer_user == "root")
                {
                    Console.WriteLine("Invalid value.");
                    //return null;
                }
                else
                {
                    Console.Write("Enter password: ");
                    string pass = Console.ReadLine();
                    info = new { user = buffer_user, pass = pass };
                    success = false;
                    //return info;
                }
            }
            return info;

        }
        public static string write_system_config()
        {
            Console.Write("Enter device name: ");
            string buffer_device_name = Console.ReadLine();
            return buffer_device_name;
        }
        public static void ver(string current_user)
        {
            string user = read_user(); string device_name = read_device_name(); string system = read_system(); string version = read_version();
            Console.WriteLine(@$"

        <-. (`-')_      {current_user}@{device_name}
       \( OO) )         OS: {system}
    ,--./ ,--/          Ver: {version}
    |   \ |  |          
    |  . '|  |)         
    |  |\    |          
    |  | \   |          
    `--'  `--' 
");
        }
        public static string read_file(string path)
        {
            return File.ReadAllText(path);
        }
        public static void write_file(string path, string data)
        {
            File.WriteAllText(path, data);
        }
        public static void create_file(string path)
        {
            File.Create(path);
        }
        public static void delete_file (string path)
        {
            File.Delete(path);
        }
        public static void create_directory (string path)
        {
            Directory.CreateDirectory(path);
        }
        public static void delete_directory (string path)
        {
            Directory.Delete(path);
        }
        public static bool exists(string path)
        {
            return File.Exists(path);
        }
        public static void files_list(string current_directory)
        {
            //return Directory.GetFiles(Directory.GetCurrentDirectory());
            //Console.WriteLine(Directory.GetDirectories(Directory.GetCurrentDirectory()));
            //Console.WriteLine(Directory.GetFiles(Directory.GetCurrentDirectory()));

            // если папка существует
            if (Directory.Exists(current_directory))
            {
                //Console.WriteLine("Подкаталоги:");
                string[] dirs = Directory.GetDirectories(current_directory);
                foreach (string s in dirs)
                {
                    Console.WriteLine("<DIR>    " + s);
                }
                //Console.WriteLine();
                //Console.WriteLine("Файлы:");
                string[] files = Directory.GetFiles(current_directory);
                foreach (string s in files)
                {
                    Console.WriteLine("<FILE>   " + s);
                }
            }
        }
    }

    //config (kak banal'no blen)
    internal class Config
    {
        public string? user { get; set; }
        public string? device_name { get; set; }
        public string? system { get; set; }
        public string? version { get; set; }
        public string? dev { get; set; }
        public string pass { get; set; }
    }
}