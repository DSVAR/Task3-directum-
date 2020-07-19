using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using task_3.Properties;
using System.Threading;

namespace task_3.scripts
{
    class Settings
    {
       
        static public List<string> WasList = new List<string>();
        static public List<string> WillList = new List<string>();
        static public string Path = Environment.CurrentDirectory+@"\Events\",time, nameChan;
        static public  string mistake = "такого выбора нет";
        static public int l;
        static public DateTime dt;
        static public Thread noc;

        static string comment, timek, name, text, person, answer2;//ответы для записи

        static public void events()
        {
            Console.Clear();
            Console.WriteLine("Запланнированные встречи ");
            show();
            
        }



        static public void show()
        {

            int count=1;
            DirectoryInfo DI = new DirectoryInfo(Path);
            List<string> list = new List<string>();
            string name,answer;
            DateTime dt;

            WasList.Clear();
            WillList.Clear();

            foreach (var files in DI.GetFiles())
            {
                list.Add(new string(files.Name));
            }
           
            for (int i=0; i < list.Count; i++)
            {
                name = list[i];
                name = name.Split('.')[0];

                using (StreamReader reader = new StreamReader(Path + list[i]))
                {
                    time = reader.ReadLine();
                }

                time = time.Remove(0, 5);
                dt = Convert.ToDateTime(time);

                if (DateTime.Now > dt)
                WasList.Add(list[i]);
                
                else WillList.Add(list[i]);
                
            }

            Console.WriteLine("Встречи которые будут");
                for (int y=0;y<WillList.Count;y++)
                {
                    using (StreamReader reader = new StreamReader(Path + WillList[y]))
                    {
                        time = reader.ReadLine();
                    }

                    time = time.Remove(0, 5);
                name = WillList[y];
                name = name.Split('.')[0];

                    Console.WriteLine(count + ". " + name + " "+time);
                    count++;
                }
                count = 1;


            Console.WriteLine("Прошедшие встречи");
                for (int t=0; t<WasList.Count;t++)
                {
                    using (StreamReader reader = new StreamReader(Path + WasList[t]))
                    {
                        time = reader.ReadLine();
                    }

                    time = time.Remove(0, 5); 

                        name = WasList[t];
                        name = name.Split('.')[0];

                Console.WriteLine(count + ". " + WasList[t] + " " + time);
                    count++;
                }

                Wow:

            Console.Write("\r\n"+"редактирировать какие встречи?(будущие или прошлые):");
            answer = Console.ReadLine();

            if(answer=="Будущие" || answer=="будущие")
            {
                future();
            }
                    else
                    {
                        if (answer == "Прошлые" || answer == "прошлые")
                        {
                             past();
                        }
                            else
                            {
                                Console.WriteLine(mistake);
                                    goto Wow;
                            }
                    }
        }




        static public void future()
        {
            nameChan = "future";
            int answerF;
          
        again:
            Console.WriteLine("\r\n Управление будущими встречами");
            Console.Write("Ведите цифру встречу которой хотите управлять:");

            try
            {
                answerF = Convert.ToInt32(Console.ReadLine());

                if (answerF > 0 && answerF <= WillList.Count)
                {
                    l = answerF;
                    l--;
                    name = WillList[l];
                    using (StreamReader reader = new StreamReader(Path + name))
                    {
                        text = reader.ReadToEnd();

                    }

                    timek = text.Remove(0, 5);
                    timek = timek.Split("\r\n")[0];

                    person = text.Substring(text.IndexOf("Person") + 7);
                    person = person.Split("\r\n")[0];

                    comment = text.Substring(text.IndexOf("Comments:") + 9);

                    Console.WriteLine("\r\n Время встречи:" + timek + "\r\n Скем:" + person + "\r\n Комментарий к встрече:" + comment);
        jobs:
                    Console.WriteLine("Что сделать?(Удалить, изменить, поставить оповещения)");
                    answer2 = Console.ReadLine();

                        if (answer2 == "Удалить" || answer2 == "удалить")
                        {
                             delet();
                        }
                            else
                            {
                                if (answer2 == "изменить" || answer2 == "Изменить")
                                {
                                     change();
                                }
                                    else 
                                    {
                                        if (answer2 == "Поставить оповещение" || answer2 == "поставить оповещение"||answer2 == "поставить оповещения"||answer2 == "Поставить оповещения")
                                        {
                                            noc   = new Thread(notice);
                                             noc.Start();
                                        }
                                        else
                                        {
                                            Console.WriteLine(mistake);
                                              goto jobs;
                                        }
                                    }
                            }
                }
                        else
                        {
                            Console.WriteLine(mistake);
                                goto again;
                        }
            }
            catch { Console.WriteLine("Строка имела не верный формат"); goto again; }
        }



        static public void past()
        {
            nameChan = "last";
            Console.WriteLine("Управление прошлыми встречами");
            int answerF;

        again:
            Console.WriteLine("\r\n Управление будущими встречами");
            Console.Write("Ведите цифру встречу которой хотите управлять:");

            try
            {
                answerF = Convert.ToInt32(Console.ReadLine());

                if (answerF > 0 && answerF <= WasList.Count)
                {
                    l = answerF;
                    l--;
                    name = WasList[l];
                    using (StreamReader reader = new StreamReader(Path + name))
                    {
                        text = reader.ReadToEnd();

                    }

                    timek = text.Remove(0, 5);
                    timek = timek.Split("\r\n")[0];

                    person = text.Substring(text.IndexOf("Person") + 7);
                    person = person.Split("\r\n")[0];

                    comment = text.Substring(text.IndexOf("Comments:") + 9);

                    Console.WriteLine("\r\n Время встречи:" + timek + "\r\n Скем:" + person + "\r\n Комментарий к встрече:" + comment);
                jobs:
                    Console.WriteLine("Что сделать?(Удалить)");
                    answer2 = Console.ReadLine();

                    if (answer2 == "Удалить" || answer2 == "удалить")
                    {
                        delet();
                    }
                   
                }
                else
                {
                    Console.WriteLine(mistake);
                    goto again;
                }
            }
            catch { Console.WriteLine("Строка имела не верный формат"); goto again; }


        }

        static public void delet()
        {
            if (nameChan == "future")
            {
                File.Delete(Path + WillList[l]);
              
            }
            else
            {
                File.Delete(Path + WasList[l]);
                Settings.events();

            }

        }

        static public void change()
        {
            string choise;
        change1:
            Console.Write("\r\nИзменить время или коментарий?");
            choise = Console.ReadLine();
            
            if (choise== "время " || choise == "Время ")
            {
                Console.Write("\r\nНовое время:");
                time = Console.ReadLine();
                    try { 
                    dt = Convert.ToDateTime(time);
                    }
                catch
                {
                    Console.WriteLine("неправильный формат");
                    goto change1;
                }
                text = "Date:" + dt + "\r\n" + "Person:" + person + "\r\n" + "Comments:" + comment;

                using (StreamWriter writer = new StreamWriter(Path + person + ".txt"))
                {
                    writer.WriteLine(text);
                }
                Settings.events();
            }
                else
                {
                        if (choise== "коментарий" || choise == "Коментарий")
                        {
                            Console.Write("\r\nНовый коментарий:");
                            comment = Console.ReadLine();
                            text = "Date:" + timek + "\r\n" + "Person:" + person + "\r\n" + "Comments:" + comment;

                            using (StreamWriter writer = new StreamWriter(Path + person + ".txt"))
                            {
                                writer.WriteLine(text);
                            }
                    Settings.events();
                         }
                    else
                    {
                    Console.WriteLine(mistake);
                    goto change1;
                    }
                }
        }

        static public void notice()
        {
            int hours, minutes;
            string name = WillList[l];
                name = name.Split('.')[0];
            DateTime timer=DateTime.Now;

            Console.WriteLine("через сколько напомнить о встрече?");
            Console.Write("Часов:");
            hours=Convert.ToInt32(Console.ReadLine());
            Console.Write("Минут:");
            minutes = Convert.ToInt32(Console.ReadLine());

          timer=  timer.AddHours(+hours);
            timer = timer.AddMinutes(+minutes);


            Console.WriteLine(timer);

            while (true)
            {
                if (timer < DateTime.Now || timer == DateTime.Now)
                { 
                    SoundPlayer sound = new SoundPlayer(Properties.Resources.aku);
                    sound.Play();
                         Console.WriteLine("время пришло для "+name);
                        Console.WriteLine(DateTime.Now);
                    Console.WriteLine(timer);
                        Thread.Sleep(100000);
                        Settings.events();
                }
            }
          

        }
    }
}
