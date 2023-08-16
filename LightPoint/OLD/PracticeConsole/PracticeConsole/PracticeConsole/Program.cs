using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticeConsole
{
    public delegate string MyDel(string str);
    public class Program
    {
        public static void Main (string[] args)
        {
            //Console.WriteLine("Enter the video Title:");
            //var videoTitle = Console.ReadLine();

            //Video newVideo = new Video { Title = videoTitle };

            //var VEncoder = new VideoEncoder(); //provider
            //var mailService = new MailingService(); //subscriber
            //var messageService = new MessageService(); //subscriber

            //VEncoder.VideoEncodedEvent += mailService.OnVideoEncoded;  
            //VEncoder.VideoEncodedEvent += messageService.OnVideoEncoded;

            //VEncoder.Encode(newVideo);





            //List<string> list = new List<string>() { "Bilal", "Zeeshan", "Ali", "Mustajab"};
            //string stringList;
            //Console.WriteLine(stringList = String.Join(",", list));


            //for (int i=0; i <= 50; i+2)
            //{
            //    Console.WriteLine($"the number is : {i}");
            //}

            for(int i=0; i<=20; i += 2)
            {
                //Console.WriteLine($"2 * {i} = {i*2}");
                Console.WriteLine($"the number is : {i}");
            }


        }

    }
}
