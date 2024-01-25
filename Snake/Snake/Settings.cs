using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    // Клас налаштувань гри
    class Settings
    {
        // Властивості ширини, висоти і швидкості гри
        public static int width { get; set; }
        public static int height { get; set; }
        public static int speed { get; set; }

        // Рахунок і кількість балів
        public static int score { get; set; }
        public static int points { get; set; }

        // Вказівник чи завершилась гра
        public static bool gameOver { get; set; }

        // Тривалість гри
        public static int duration { get; set; }

        // Напрямок руху змійки
        public static Direction direction { get; set; }

        // Конструктор налаштувань, який встановлює початкові значення
        public Settings()
        {
            duration = 0;
            width = 12;
            height = 12;
            speed = 8;
            score = 0;
            points = 10;
            gameOver = false;
            direction = Direction.Right;
        }
    }
}
