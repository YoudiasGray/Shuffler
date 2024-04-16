using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shuffler
{
    internal class Worker
    {
        private List<string> ReadToLines(string fileName)
        {
            List<string> ret = new List<string>();
            if(File.Exists(fileName))
            {
                ret = File.ReadAllLines(fileName).ToList();
            }
            return ret;
        }
        private void WriteToFile(List<string> lines, string fileName)
        {
            File.WriteAllLines(fileName, lines);
        }
        private UInt32 GetNextRandom(UInt32 max)
        {
            UInt32 ret = 0;
            byte[] randomNumber = RandomNumberGenerator.GetBytes(4);
            UInt32 num = BitConverter.ToUInt32(randomNumber, 0);
            // 随机生成一个UINT32的数字，如果不是能够整除max，继续生成
            while (num >= (UInt32.MaxValue / max * max))
            {
                randomNumber = RandomNumberGenerator.GetBytes(4);
                num = BitConverter.ToUInt32(randomNumber, 0);
            }
            ret = num % max;
            return ret;
        }
        private List<int> GetNewOrder(List<int> input)
        {
            List<int> ret = new List<int>();
            for (int i = input.Count-1; i >= 0; i--)
            {
                int tmp = (int)GetNextRandom((UInt32)(i+1));
                int tmpValue = input[tmp];
                input[tmp] = input[i];
                input[i] = tmpValue;
            }
            return input;
        }

        private List<string> GetWriteContent(List<string> contents, List<int> Orders)
        {
            List<string> ret = new List<string>();
            foreach (int i in Orders)
            {
                ret.Add(contents[i]);
            }
            return ret;
        }

        private void TestShuffe()
        {
            int n = 10;
            List<int> oldOrder = Enumerable.Range(0, n).ToList();
            foreach (int i in oldOrder)
            {
                Console.Write(i);
                Console.Write(" ");
            }
            Console.WriteLine();
            oldOrder = GetNewOrder(oldOrder);
            foreach (int i in oldOrder)
            {
                Console.Write(i);
                Console.Write(" ");
            }
        }

        public void Work(string inputFile, string outFile)
        {
            //TestShuffe();
            //return;

            // 读取input文件到每一行
            // 重新排序
            // 输出到outfile
            List<string> fileContents = ReadToLines(inputFile);
            List<int> lineIndexs = Enumerable.Range(0, fileContents.Count).ToList();
            lineIndexs = GetNewOrder(lineIndexs);
            List<string> writeContents = GetWriteContent(fileContents,lineIndexs);
            WriteToFile(writeContents,outFile);
        }
    }
}
