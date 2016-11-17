using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Speech.Synthesis;
using System.Timers;

namespace Boe
{
    public class Game
    {
        private readonly SpeechSynthesizer speech = new SpeechSynthesizer();
        private readonly Stopwatch stopwatch = new Stopwatch();
        private readonly Timer timer;
        private bool active = true;
        private string currentPlayer;
        private long currentScore;

        public Game()
        {
            timer = new Timer {AutoReset = false};
            timer.Elapsed += HandleTimerElapsed;
        }

        public void Play()
        {
            Console.WriteLine("Wat is je naam?");
            currentPlayer = Console.ReadLine();
            Console.WriteLine("We zijn begonnen! Druk zsm op een toets als je BOE hoort");

            timer.Interval = new Random().Next(1000, 10000);
            timer.Start();

            while (active)
            {
            }
        }

        public void HandleTimerElapsed(object sender, ElapsedEventArgs e)
        {
            speech.SpeakAsync("Boe!");
            DisplayElapsedTime();
            WriteHighScore();
            Replay();
        }

        private void DisplayElapsedTime()
        {
            stopwatch.Start();
            Console.ReadKey();
            stopwatch.Stop();
            currentScore = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"{currentPlayer}, jouw score is {currentScore}");
            stopwatch.Reset();
        }

        private void WriteHighScore()
        {
            File.AppendAllLines("highscore.txt", new List<string> {$"{currentPlayer},{currentScore}"});
        }

        private void Replay()
        {
            Console.WriteLine("Nogmaals? y/n");
            var again = Console.ReadKey();
            Console.WriteLine();
            if (again.KeyChar == 'n')
            {
                active = false;
            }
            else
            {
                Play();                
            }
        }

        
    }
}