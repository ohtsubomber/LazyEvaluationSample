using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyEvaluationSample
{
    class Program
    {
        /// <summary>
        /// TextFile1.txtに１と２が含まれているので実際にはTextFile2.txtは読み込まれない。
        /// もしdataに4が含まれている場合はTextFile2.txtも読み込まれる
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var data = new List<string>() { "1", "3" };
            
            foreach(var str in GetLazyEvaluationList().Where(val=>data.Contains(val)))
            {
                data.Remove(str);
                //do task
                SomeTask(str);

                if (data.Count == 0)
                {
                    break;
                }
            }
            Console.WriteLine("completed");
            Console.ReadLine();
        }

        static void SomeTask(string str)
        {
            Console.WriteLine($"do some task:{str}");
        }

        public static IEnumerable<string> GetLazyEvaluationList()
        {
            var files = new string[] {"TextFile1.txt","TextFile2.txt"};
            foreach(var file in files)
            {
                Console.WriteLine($"open file {file}");
                using(var sr = new StreamReader(file))
                {
                    var str = string.Empty;
                    while ((str=sr.ReadLine())!=null)
                    {
                        yield return str;
                    }
                }
            }
        }
    }
}
