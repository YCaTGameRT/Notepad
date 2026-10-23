using System.Diagnostics;

namespace Notepad {
    internal class Program {
        static void Main(string[] args) {
            Thread thread1 = new Thread(Notepad);
            thread1.Start();

            Thread thread2 = new Thread(Cmd);
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine("Г: Все задачи выполнены. Нажмите любую клавишу для выхода...");
            Console.ReadKey();

            static void Notepad() {
                Console.WriteLine("1: Запуск потока.");
                try {
                    Console.WriteLine("1: Запуск Notepad.");
                    Process notepadProcess = Process.Start("notepad.exe"); {
                        Console.WriteLine("1: Ожидание 3 секунды...");
                        Thread.Sleep(3000);
                        Console.WriteLine("1: Закрытие Notepad.");
                        if (!notepadProcess.CloseMainWindow()) {
                            notepadProcess.Kill();
                        }
                        notepadProcess.Dispose();
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine($"1: Ошибка: {ex.Message}");
                }
            }
            static void Cmd() {
                Console.WriteLine("2: Запуск потока.");
                try {
                    Console.WriteLine("2: Запуск cmd.exe с командой dir.");
                    ProcessStartInfo startInfo = new ProcessStartInfo {
                        FileName = "cmd.exe",
                        Arguments = "/c dir",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        StandardOutputEncoding = System.Text.Encoding.GetEncoding(866)
                    };
                    using (Process cmdProcess = Process.Start(startInfo)) {
                        if (cmdProcess != null) {
                            using (StreamReader reader = cmdProcess.StandardOutput) {
                                string result = reader.ReadToEnd();
                                Console.WriteLine("2: Результаты команды dir:\n");
                                Console.WriteLine(result);
                            }
                            cmdProcess.WaitForExit();
                        }
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine($"2: Ошибка: {ex.Message}");
                }
            }
        }
    }
}
