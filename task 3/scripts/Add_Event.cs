using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading;

namespace task_3.scripts
{
    class Add_Event
    {
        static public string Person, Comment,time,path,pathIN,text;
       static public DateTime Date;
        static public bool ex,checks=true;

        static public void events()
        {
            path = Environment.CurrentDirectory + @"\Events\";
            pathIN = Environment.CurrentDirectory;
            Console.Clear();

            //
            Console.WriteLine("Пример ввода имени: Хасанов Артур");
            Console.WriteLine("Пример ввода времени: 18.07.2020 21:50");
            Console.WriteLine("Пример коментария: Встреча в пабе BlackCherry");

            Console.Write("Скем встреча:");
            Person = Console.ReadLine();



        Again:
            Console.Write("\r\n" + "Дата встречи и время:");

                try
                {
                    time = Console.ReadLine();
                    Date = Convert.ToDateTime(time);
                    Check_Time(time);
                    if (checks == false) goto Again;
                }
                catch
                {
                    Console.WriteLine("строка имелла не верный формат. Напишите дату и время как показано с верху");
                    goto Again;
                }


            if (DateTime.Now > Date || DateTime.Now == Date)
            {
                Console.WriteLine("В прошлом нельзя назначать встречи!!");
                goto Again;
            }

           
            var times = Date - DateTime.Now;
       
                if (times.Minutes < 59 && times.Hours==0)
                {
                    Console.WriteLine("Встречу планируют хотя-бы за час, но не меньше");
                        goto Again;
                }

          


            Console.Write("\r\n" + "Коментарий к встрече:");
              Comment = Console.ReadLine();


            text = "Date:" + Date + "\r\n" + "Person:" + Person + "\r\n" + "Comments:" + Comment;

            write();


            Console.WriteLine("Удачно все записано");
            Thread.Sleep(1000);
            Console.WriteLine("Через 2 секунды вернется главное меню");
            Thread.Sleep(1000);
            Console.WriteLine("1 СЕКУНДА");
            Thread.Sleep(1000);
            Console.WriteLine("ВОЗВРАТ");
            Thread.Sleep(1000);
            Main_Menu.mai();
        }

       static public void write()
        {
              
                ex = Directory.Exists(path);
                DirectoryInfo DI = new DirectoryInfo(pathIN);


            if (ex == false)
            {
                string nameFolder = "Events";

                DI.CreateSubdirectory(nameFolder);
                using (StreamWriter writer = new StreamWriter(path + Person + ".txt"))
                {
                    writer.WriteLine(text);
                }
            }

            else
            { 
                using (StreamWriter writer = new StreamWriter(path+Person+".txt"))
                {
                    writer.WriteLine(text);
                }
            }
        }



        static public void Check_Time(string timer)
        {
            checks = true;
            string otherTIME;
            DirectoryInfo DI = new DirectoryInfo(path);
            List<string> list = new List<string>();

             foreach(var files in DI.GetFiles())
             {
                list.Add(new string(files.Name));
             }
           


             for(int t=0; t < list.Count; t++)
             {
                using (StreamReader reader = new StreamReader(path+list[t]))
                {
                    otherTIME = reader.ReadLine();
                }

                otherTIME = otherTIME.Remove(0, 5);

                if (otherTIME == timer) 
                {
                    Console.WriteLine("На это время уже есть запланированная встреча с" +list[t]) ;
                    checks = false;
                }
              
                
             }

            
        }
    }
}
