namespace Shuffler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 2 )
            {
                Console.WriteLine("usage: XX.exe file1 file2");
                return;
            }
            Worker worker = new Worker();
            string file1 = args[0];
            string file2 = args[1];

            worker.Work(file1,file2);

        }
    }
}
