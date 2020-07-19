using System;
using System.Collections.Generic;
using System.Text;

namespace task_3.scripts
{
    class Main_Menu
    {
        static public int answer;
        static public void mai()
        {
            Console.Clear();
         
            Begin:
            Console.WriteLine("что бы поставить оповещение надо выбрать цифру 2. встречу и ' поставить оповещения' . \r\n Написать через сколько прозвинит оповещение\r\n через некоторое время заиграет музыка, значит что таймер закончил свою работу.");
            Console.WriteLine("\r\n"+"1.Добавить встречи");
            Console.WriteLine("2.Имеющиеся встречи/настройка встреч");
            Console.Write("Введите цифру:");


            try 
            { 
                answer = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("строка имела не правильную форму");
                goto Begin;
            }


            if (answer > 2 || answer < 1)
            {
                Console.WriteLine("Нету данного варианта");
                goto Begin;
            }


            switch (answer)
            {

                case 1:
                    {
                        Add_Event.events();
                        break;
                    }

                case 2:
                    {
                        Settings.events();
                        break;
                    }

            }


        }


    }
}
